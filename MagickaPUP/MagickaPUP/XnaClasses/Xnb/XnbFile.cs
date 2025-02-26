using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses.Xna.Data;
using System;
using System.Collections.Generic;
using MagickaPUP.XnaClasses.Xnb.Data;
using MagickaPUP.MagickaClasses.Liquids;
using MagickaPUP.MagickaClasses.Map;
using MagickaPUP.Utility.Exceptions;

namespace MagickaPUP.XnaClasses.Xnb
{
    public class XnbFile
    {
        #region Variables - Private

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

        public XnbFile()
        {
            this.PrimaryObject = new XnaObject();
            this.SharedResources = new XnaObject[0];
        }

        public XnbFile(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading XNB File...");

            ReadHeader(reader, logger);
            ReadFileSizes(reader, logger);
            ReadContentTypeReaders(reader, logger);
            ReadSharedResourceCount(reader, logger);
            ReadPrimaryObject(reader, logger);
            ReadSharedResources(reader, logger);

            logger?.Log(1, "Finished reading XNB file!");
        }

        #endregion

        #region PublicMethods

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing XNB File...");

            WriteHeader(writer, logger);
            WriteFileSizes(writer, logger);
            WriteContentTypeReaders(writer, logger);
            WriteSharedResourceCount(writer, logger);
            WritePrimaryObject(writer, logger);
            WriteSharedResources(writer, logger);
            
            WritePaddingBytes(writer, logger);

            logger?.Log(1, "Finished writing XNB file!");
        }

        #endregion

        #region PrivateMethods - Read

        private void ReadHeader(MBinaryReader reader, DebugLogger logger = null)
        {
            #region Comment - Char vs Byte

            // NOTE : All char reads ('X', 'N', 'B', and 'w') are performed with ReadByte() rather than ReadChar() since I want to ensure that future text encodings cannot fuck shit up for whatever reason.
            // The future is most likely UTF8 which is ASCII compatbile, so ReadChar() for those bytes should theoretically work just fine, but in C# guaranteed to maintain char as a single byte for future versions?
            // This isn't C, so I'd rather not risk it!
            
            // As a side note, as of today, the C# standard seems to say that char is 2 bytes in memory, but ReadChar() will read 1 single byte if the detected char corresponds to an ASCII compatible byte in the system encoding...
            // So yeah, all the more reason to NOT use char type in this case in C#...

            // In short: replaced all ReadChar() calls with ReadByte() calls to ensure that users with a different system encoding don't break the program.

            #endregion

            logger?.Log(1, "Reading XNB Header...");

            // Validate the input data to check if it is a valid XNB file
            logger?.Log(1, "Validating XNB File...");
            this.xnbMagic[0] = reader.ReadByte();
            this.xnbMagic[1] = reader.ReadByte();
            this.xnbMagic[2] = reader.ReadByte();
            string headerString = $"{(char)this.xnbMagic[0]}{(char)this.xnbMagic[1]}{(char)this.xnbMagic[2]}";
            if (!(this.xnbMagic[0] == (byte)'X' && this.xnbMagic[1] == (byte)'N' && this.xnbMagic[2] == (byte)'B'))
            {
                logger?.Log(1, $"Header \"{headerString}\" is not valid!");
                throw new MagickaReadExceptionPermissive();
            }
            logger?.Log(1, $"Header \"{headerString}\" is valid!");

            // Perform platform validation.
            // Check if the platform is Windows. (No other platforms are supported in Magicka, so it really can't be anything else...)
            // TODO : Maybe modify this code to be flexible and allow the platform byte to be anything when reading?
            this.platform = reader.ReadByte();
            if (platform != (byte)'w')
            {
                logger?.Log(1, $"Platform \"{platform}\" is not valid.");
                throw new MagickaReadExceptionPermissive();
            }
            logger?.Log(1, $"Platform \"{platform}\" is valid (Windows)");

            #region Comment - Flags
            
            // NOTE : Within Magicka's compiled code, the compiled XNA assembly was actually optimized to disregard a lot of information from the 2 following bytes.
            // They are just read as a single u16, and if it's equal to 4, then the version is XNA 3.1 (which is the correct one for Magicka) and flags is set to false (all 0s).
            // If the u16 has the value 32772, then the byte that corresponds to the version has value 4 (so again, correct XNA version) and the XNB flags byte is set to 0x80, which is LZX compression.
            // Within Magicka's code, flags is just a single bool which determines whether the file is compressed or not.
            // In short, the code is weirdly optimized and discards a lot of information that would lead to a potentially valid XNB file, but in the case of MagickaPUP's
            // implementation, I want it to be as correct as possible and as transparent as possible, which is why I derived a working XNB 3.1 working reader implementation
            // from both reverse engineering Magicka's code, seeing the optimized and decompiled assemblies of XNA 3.1, and by guessing a lot of changes from what little I could find of the XNA 4.0 official spec.
            // If only the official XNA 3.1 spec was still available somewhere, none of this would be an issue...
            
            #endregion

            // Validate version number.
            // Gets the version number and validates that it is an XNB file for XNA 3.1, even tho it does not matter that much in this case.
            byte xnbVersion = reader.ReadByte();
            logger?.Log(1, $"XNA Version : {{ byte = {xnbVersion}, version = {XnaVersion.XnaVersionString(((XnaVersion.XnaVersionByte)xnbVersion))} }}");
            if (xnbVersion != (byte)XnaVersion.XnaVersionByte.Version_3_1)
            {
                logger?.Log(1, "The XNA version is not supported by Magicka!");
                throw new MagickaReadExceptionPermissive();
            }

            // Get XNB Flags to check for compression.
            // Compression type should always be uncompressed to be able to read the data within the file.
            // Expect this vlaue to be 0x00. If it's 0x80 or anything else, bail out.
            byte xnbFlags = reader.ReadByte();
            bool hiDefProfile = (xnbFlags & (byte)XnbFlags.HiDefProfile) == (byte)XnbFlags.HiDefProfile;
            bool isCompressedLz4 = (xnbFlags & (byte)XnbFlags.Lz4Compressed) == (byte)XnbFlags.Lz4Compressed;
            bool isCompressedLzx = (xnbFlags & (byte)XnbFlags.LzxCompressed) == (byte)XnbFlags.LzxCompressed;
            logger?.Log(1, $"XNB Flags : (byte = {xnbFlags})");
            logger?.Log(1, $" - HD Profile          : {hiDefProfile}");
            logger?.Log(1, $" - Compressed with Lz4 : {isCompressedLz4}");
            logger?.Log(1, $" - Compressed with Lzx : {isCompressedLzx}");

            // Handle file compression
            if (isCompressedLz4)
            {
                logger?.Log(1, "Cannot read XNB files compressed with LZ4 compression!");
                throw new MagickaReadExceptionPermissive();
            }

            if (isCompressedLzx)
            {
                logger?.Log(1, "Cannot read XNB files compressed with LZX compression!");
                throw new MagickaReadExceptionPermissive();
            }

            // TODO : Implement decompression support in the future!

            // TODO : Implement decompression step here, at least for the LZX compression algorithm.

            // NOTE : Any data that is located AFTER the decompression flag is susceptible to being compressed. Only the initial part of the header is always the same
            // size in bytes both in compressed and non compressed files.
            // That is why we perform the decompression step (for the implemented compression algorithms that we support...) before continuing with the rest of the reading.
        }

        private void ReadFileSizes(MBinaryReader reader, DebugLogger logger = null)
        {
            // TODO : Maybe move the size reading code to the header reading since the amount of size vars read depends on the compression?
            logger?.Log(1, "Reading File Sizes...");

            // TODO : Fix this, the size reading is actually wrong. When uncompressed, the file stores a single i32 which stores the whole file length, which usually is the same as an u16 and the other 2 bytes entirely zeroed out because the max file size seems to be the u16 limit.
            // When compressed, the file contains first an i32 with the decompressed size, and then an i32 with the compressed size.
            // The 4.0 spec is different, which is why this worked when downsizing the var size from 2 i32s to 2 u16s, but the logic is actually not correct and it doesn't do what the code says it does...
            // in short: FIXME!!!

            // Get file sizes for compressed and uncompressed sizes.
            // NOTE : They can actually be whatever you want, it doesn't really matter since Magicka doesn't use these values actually...
            // NOTE : These values should be ushort but I'm keeping them as uint as originally, the XNB file reference I was following was for XNA 4.0 and those use u32 for the size variables. In short, I'm just keeping it like this to remember and for furutre feature support etc etc... could really just be changed to ushort without any problems tbh. Actually, the writer does use ushorts so yeah lol...
            uint sizeCompressed = reader.ReadUInt16(); // Compressed size of the data
            uint sizeDecompressed = reader.ReadUInt16(); // Decompressed / Uncompressed size of the data
            logger?.Log(1, $"File Size Compressed   : {sizeCompressed}");
            logger?.Log(1, $"File Size Decompressed : {sizeDecompressed}");
        }
        
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

        private void WriteHeader(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing XNB Header...");

            // Write the XNB file header
            byte[] bytes = {
                (byte)'X', (byte)'N', (byte)'B', // XNB identifier
                (byte)'w', // Platform Windows
                (byte)XnaVersion.XnaVersionByte.Version_3_1, // XNA Version 3.1
                (byte)XnbFlags.None // No compression
            };
            writer.Write(bytes);

            // TODO : Maybe implement in the future a compression system so that we can export compressed files if the user wants to do so?
        }

        private void WriteFileSizes(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing File Sizes...");

            // Write the File Sizes for compressed and uncompressed XNB file states.
            // These sizes are placeholders. For now, we just write the max possible size and call it a day, because they don't really matter...
            // The game doesn't use these values for anything anyway, so they can be as large as we want. Easier to just hardcode them to be the max possible size.
            ushort sizeCompressed = 65535;
            ushort sizeDecompressed = 65535;
            writer.Write(sizeCompressed);
            writer.Write(sizeDecompressed);

            // TODO : In the future, if the file sizes are properly implemented rather than always going with the max value of an u16, we should probably print them to the console with debug logs.
        }

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

        private void WritePaddingBytes(MBinaryWriter writer, DebugLogger logger = null, int numBytes = 64, byte value = 0)
        {
            #region Comment

            // Pad with NULL bytes the end of the file.
            // This is not mandated by the XNA spec, but it is here to save slightly malformed files from crashing.
            // This should only happen if someone writing the input files accidentally write some information that is not correct, but correct
            // enough to work if we prevent the game from throwing an exception by reading out of bounds in the file stream by adding these
            // padding bytes...
            // Depending on how big the fuck up is, the padding needed to save the file could potentially be massive,
            // so by default we're writing 64 bytes with the value 0 and calling it a day.
            
            #endregion

            logger?.Log(1, "Writing padding bytes...");
            logger?.Log(2, $" - Bytes : {numBytes}");
            logger?.Log(2, $" - Value : {value}");

            byte[] bytes = new byte[numBytes];
            for (int i = 0; i < numBytes; ++i) // Kind of an slow initialization... with C we could do this out of the box, and maybe even use memset if we wanted... in C# (afaik) we have to manually iterate due to object construction and shit...
                bytes[i] = value;
            writer.Write(bytes);
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
