using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses.Xna.Data;
using System;
using System.Collections.Generic;
using MagickaPUP.XnaClasses.Xnb.Data;
using MagickaPUP.MagickaClasses.Liquids;
using MagickaPUP.MagickaClasses.Map;
using MagickaPUP.Utility.Exceptions;
using MagickaPUP.Utility.Compression;
using System.IO;
using MagickaPUP.XnaClasses.Xna;

namespace MagickaPUP.XnaClasses.Xnb
{
    // TODO : Fix up this large as fuck wall of text of a comment...
    // TODO : Implement the following proposal and clean up the fucking comments and move all of the logic to the PUP classes, etc etc...

    // NOTE : After implementing the XNB file decompression support, the XNB file class itself simply contains the reading and writing for the contents of the data
    // stored within the XNB file itself.
    // This means that the XNB header, platform and flag bytes are all written and handled entirely externally in the PUP classes.
    // This has been done like this so as to make it easier to pass around the stream to read the binary data from, which also made it far easier to encapsulate
    // the process of selecting the final memory stream and freeing the compressed one when performing in memory decompression.
    // This process partially mimicks the way Monogame's ContentManager abstracted away the ContentReader stream selection for ContentReader construction when opening
    // an XNB file.

    // In short: The XnbFile class contains the decompressed and relevant data of the XNB file.
    
    // TODO : Maybe should rename this class to something else so that there is no confusion regarding where the XNB header bytes are read / written
    // and how the compression / decompression is performed (as well as where is it that it takes place within the code).
    // All of these are concerns for future readers who might want to understand the code tho.
    public class XnbFileData // Maybe we should rename this class to just XnbData and that's it? and the other XnaData static class should maybe just be XnaConstants or whatever so as to prevent confusion? Altough XnbFileData is pretty self explanatory and makes sense, so it's not necessarily a bad name...
    {
        #region Variables - Public

        public ContentTypeReader[] ContentTypeReaders { get; set; }
        public XnaObject PrimaryObject { get; set; }
        public XnaObject[] SharedResources { get; set; }

        #endregion

        #region Constructor

        public XnbFileData()
        {
            this.ContentTypeReaders = new ContentTypeReader[0];
            this.PrimaryObject = new XnaObject();
            this.SharedResources = new XnaObject[0];
        }

        public XnbFileData(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading XNB Data...");

            ReadContentTypeReaders(reader, logger);
            ReadSharedResourceCount(reader, logger);
            ReadPrimaryObject(reader, logger);
            ReadSharedResources(reader, logger);

            logger?.Log(1, "Finished Reading XNB Data!");
        }

        #endregion

        #region PublicMethods

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing XNB Data...");

            using (var memoryStream = new MemoryStream())
            using (var writerStream = new MBinaryWriter(memoryStream))
            {
                WritePrimaryObject(writerStream, logger);
                WriteSharedResources(writerStream, logger);

                WriteContentTypeReaders(writer, writerStream, logger);
                WriteSharedResourceCount(writer, logger);
                writer.Write(memoryStream.ToArray());
            }

            logger?.Log(1, "Finished Writing XNB Data!");
        }

        #endregion

        #region PrivateMethods - Read

        private void ReadContentTypeReaders(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Content Type Readers...");

            // Get the amount of type readers and iterate through all of them.
            int typeReaderCount = reader.Read7BitEncodedInt();
            logger?.Log(1, $"Content Type Reader Count : {typeReaderCount}");
            this.ContentTypeReaders = new ContentTypeReader[typeReaderCount];
            for (int i = 0; i < typeReaderCount; ++i)
                this.ContentTypeReaders[i] = new ContentTypeReader(reader, logger);

            // Add the readers to the current context reader too so that we can use them later on with the correct indices.
            reader.ContentTypeReaderStorage.AddReaders(this.ContentTypeReaders);
        }
        
        private void ReadSharedResourceCount(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Shared Resource Count...");

            // Get number of Shared Resources.
            int sharedResourceCount = reader.Read7BitEncodedInt();
            this.SharedResources = new XnaObject[sharedResourceCount];

            logger?.Log(1, $"Shared Resource Count : {sharedResourceCount}");
        }

        private void ReadPrimaryObject(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Primary Object...");
            this.PrimaryObject = XnaUtility.ReadObject<XnaObject>(reader, logger);
            logger?.Log(1, "Finished Reading Primary Object!");
        }

        private void ReadSharedResources(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Shared Resources...");
            
            for (int i = 0; i < this.SharedResources.Length; ++i)
            {
                logger.Log(1, $"Reading Shared Resource {(i + 1)} / {this.SharedResources.Length}...");
                var sharedResource = XnaUtility.ReadObject<XnaObject>(reader, logger);
                this.SharedResources[i] = sharedResource;
            }
            
            logger?.Log(1, "Finished reading Shared Resources!");
        }

        #endregion

        #region PrivateMethods - Write

        private void WriteContentTypeReaders(MBinaryWriter writer, MBinaryWriter writer2, DebugLogger logger = null)
        {
            logger?.Log(1, "Fetching Content Type Readers...");
            // Add the content type readers to the context writer's list of readers so that they can be used later on.
            writer.ContentTypeReaderStorage.AddReaders(this.ContentTypeReaders);
            writer.ContentTypeReaderStorage.AddReaders(writer2.ContentTypeReaderStorage.ContentTypeReaders);
            
            logger?.Log(1, $"Content Type Readers found : {writer.ContentTypeReaderStorage.Count}");

            // The obtained object defines its own content type reader list.
            // Write the content type readers defined by the input file.
            // Note that if a required content type reader is not present on this list, it will be added prior to writing the content type readers.
            // That step is performed automatically during the XNB data read process.
            // Each time an XnaObject.ReadObject() call is performed, the requested content type writer is added if it is not present.
            logger?.Log(1, "Writing Content Type Readers...");
            writer.Write7BitEncodedInt(writer.ContentTypeReaderStorage.Count);
            foreach (var reader in writer.ContentTypeReaderStorage.ContentTypeReaders)
                reader.Write(writer, logger);
        }

        private void WriteSharedResourceCount(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Shared Resource Count...");

            int count = this.SharedResources.Length;

            // We always append a null object as a shared resource if the number of shared resources is 0 and the object type is a level model.
            // This makes the game less likely to crash when dealing with a map, for some fucking reason...
            if (ShouldAppendNullObject())
                ++count;

            writer.Write7BitEncodedInt(count);
            logger?.Log(1, $" - Shared Resource Count : {count}");
        }

        private void WritePrimaryObject(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Primary Object...");
            XnaUtility.WriteObject(this.PrimaryObject, writer, logger); // First we write the 7 bit encoded integer for the content reader index, then we write the object itself.
        }

        private void WriteSharedResources(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Shared Resources...");

            if (ShouldAppendNullObject())
            {
                XnaUtility.WriteObject<object>(null, writer, logger);
            }
            else
            {
                for (int i = 0; i < this.SharedResources.Length; ++i)
                {
                    XnaUtility.WriteObject(this.SharedResources[i], writer, logger);
                }
            }
        }

        private bool ShouldAppendNullObject()
        {
            bool hasAnyResources = this.SharedResources.Length > 0;
            bool shouldAppendNullObject = this.PrimaryObject.ShouldAppendNullObject();
            return !hasAnyResources && shouldAppendNullObject;
        }

        #endregion
    }
}
