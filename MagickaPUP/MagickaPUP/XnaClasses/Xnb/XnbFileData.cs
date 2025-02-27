using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses.Xna.Data;
using System;
using System.Collections.Generic;
using MagickaPUP.XnaClasses.Xnb.Data;
using MagickaPUP.MagickaClasses.Liquids;
using MagickaPUP.MagickaClasses.Map;
using MagickaPUP.Utility.Exceptions;
using MagickaPUP.Utility.Compression;

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
    public class XnbFileData
    {
        #region Variables - Private

        // TODO : Get rid of this shit. Local storage doesn't even make sense tbh...
        private byte[] xnbMagic; // The "XNB Magic" are the initial bytes used at the start of the file that help identify it as a valid XNB file. All XNB files must start with the bytes corresponding to the ASCII encoded chars "XNB"
        private byte platform;
        private bool isCompressed;

        #endregion

        #region Variables - Public

        public ContentTypeReader[] ContentTypeReaders { get; set; }
        public XnaObject PrimaryObject { get; set; }
        public XnaObject[] SharedResources { get; set; }

        #endregion

        #region Constructor

        public XnbFileData()
        {
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

            WriteContentTypeReaders(writer, logger);
            WriteSharedResourceCount(writer, logger);
            WritePrimaryObject(writer, logger);
            WriteSharedResources(writer, logger);

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
                this.ContentTypeReaders[i] = ContentTypeReader.Read(reader, logger);

            // Add the readers to the current context reader too so that we can use them later on with the correct indices.
            reader.ContentTypeReaders.AddReaders(this.ContentTypeReaders);
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
            this.PrimaryObject = XnaObject.ReadObject<XnaObject>(reader, logger);
            logger?.Log(1, "Finished Reading Primary Object!");
        }

        private void ReadSharedResources(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Shared Resources...");
            
            for (int i = 0; i < this.SharedResources.Length; ++i)
            {
                logger.Log(1, $"Reading Shared Resource {(i + 1)} / {this.SharedResources.Length}...");
                var sharedResource = XnaObject.ReadObject<XnaObject>(reader, logger);
                this.SharedResources[i] = sharedResource;
            }
            
            logger?.Log(1, "Finished reading Shared Resources!");
        }

        #endregion

        #region PrivateMethods - Write

        // TODO : Modify this code to get the content type readers from the context var instead. We'll add them somewhere when reading the object.
        // We can still use the required content readers getter method, the point is that I want to be a bit more consistent with the idea of what I want to end up doing in the future when I modify the read side of the code...
        // The point of these modifications is that we will eventually be capable of writing the correct writer indices without hardcoding a list of all of the known readers... but only once I finally get around finishing the implementation of the rest of this fucking code!!!
        // In short, this impl breaks everything and I must rewrite a ton of shit to get back to a working product...
        private void WriteContentTypeReaders(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Fetching Content Type Readers...");
            // Add the content type readers to the context writer's list of readers so that they can be used later on.
            writer.ContentTypeReaders.AddReaders(this.ContentTypeReaders);
            
            logger?.Log(1, $"Content Type Readers found : {writer.ContentTypeReaders.Count}");
            if (writer.ContentTypeReaders.Count > 0)
            {
                logger?.Log(1, "Writing Content Type Readers...");

                // If the obtained object defines its own content type reader list, then we write those...
                writer.Write7BitEncodedInt(writer.ContentTypeReaders.Count);
                foreach (var reader in writer.ContentTypeReaders.ContentTypeReaders)
                    reader.WriteInstance(writer, logger);
            }
            else
            {
                logger?.Log(1, "Defaulting to writing all known content type readers...");
                
                // If the obtained object does not define its own content type reader list, then we write them all just in case...
                writer.Write7BitEncodedInt(XnaInfo.ContentTypeReaders.Length);
                foreach (var reader in XnaInfo.ContentTypeReaders)
                    reader.WriteInstance(writer, logger);
            }
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
            XnaObject.WriteObject(this.PrimaryObject, writer, logger); // First we write the 7 bit encoded integer for the content reader index, then we write the object itself.
        }

        private void WriteSharedResources(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Shared Resources...");

            if (ShouldAppendNullObject())
            {
                XnaObject.WriteEmptyObject(writer, logger);
            }
            else
            {
                for (int i = 0; i < this.SharedResources.Length; ++i)
                {
                    XnaObject.WriteObject(this.SharedResources[i], writer, logger);
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
