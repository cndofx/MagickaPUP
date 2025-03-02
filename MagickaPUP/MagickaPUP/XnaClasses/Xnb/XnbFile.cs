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
using System.Runtime.InteropServices.ComTypes;

namespace MagickaPUP.XnaClasses.Xnb
{
    // NOTE : This is the "top-level" XNB File class.
    // This class handles the creation of its readers and writers, context creation takes place here, and stream management is peformed here as well.
    // This class is the one in charge of handling decompression stream handling.
    // This class manages the reading and writing of the XNB header data and file size encoding, as well as compression and decompression.
    // For handling of the contents stored within the XNB file itself, see the XnbFileData class
    public class XnbFile
    {
        #region Variables - Public

        public XnbFileData XnbFileData { get; set; }

        #endregion

        #region Constructor

        public XnbFile()
        {
            this.XnbFileData = new XnbFileData();
        }

        public XnbFile(MBinaryReader reader, DebugLogger logger = null)
        {
            #region Comment - using statements

            // Create a local variable to hold the final reader that will be used when reading the contents of the XNB file.
            // This can either be our local binary reader or a new one created after decompression takes place.
            // This way, the rest of the reading process can live completely unaware of the fact that decompression took place.

            #endregion

            logger?.Log(1, "Reading XNB File...");

            #region Comment - Char vs Byte

            // NOTE : All char reads ('X', 'N', 'B', and 'w') are performed with ReadByte() rather than ReadChar() since I want to ensure that future text encodings cannot fuck shit up for whatever reason.
            // The future is most likely UTF8 which is ASCII compatbile, so ReadChar() for those bytes should theoretically work just fine, but in C# guaranteed to maintain char as a single byte for future versions?
            // This isn't C, so I'd rather not risk it!

            // As a side note, as of today, the C# standard seems to say that char is 2 bytes in memory, but ReadChar() will read 1 single byte if the detected char corresponds to an ASCII compatible byte in the system encoding...
            // So yeah, all the more reason to NOT use char type in this case in C#...

            // In short: replaced all ReadChar() calls with ReadByte() calls to ensure that users with a different system encoding don't break the program.

            #endregion

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

            #region Comment - XNB Data and Compression

            // NOTE : Any data that is located AFTER the decompression flag is susceptible to being compressed.
            // Only the initial part of the header is always the same size in bytes both in compressed and non compressed files.
            // That is why we perform the decompression step (for the implemented compression algorithms that we support...) before continuing with
            // the rest of the reading.
            // After decompression takes place, we can perform the data reading as if nothing had happened. The rest of the process does not need to be
            // aware of whether decompression took place or not.

            #endregion

            #region XNB Magic

            // Validate the input data to check if it is a valid XNB file
            logger?.Log(1, "Validating XNB File Header...");
            byte x = reader.ReadByte(); // These 3 variables are the first 3 bytes of the program. These are known as the "XNB Magic", which are the "magic" or "special" words usually at the begining of a file, used to quickly identify them as a valid file of whatever type. In this case, XNB files identify themselves with the byte sequence 'X', 'N' and 'B'. Obviously, for this to be a valid XNB file, the rest of the bytes must also be valid, but you get what I mean...
            byte n = reader.ReadByte();
            byte b = reader.ReadByte();
            string headerString = $"{(char)x}{(char)n}{(char)b}";
            if (!(x == (byte)'X' && n == (byte)'N' && b == (byte)'B'))
            {
                logger?.Log(1, $"Header \"{headerString}\" is not valid!");
                throw new MagickaReadExceptionPermissive();
            }
            logger?.Log(1, $"Header \"{headerString}\" is valid!");

            #endregion

            #region XNB Platform

            // Perform platform validation.
            // Check if the platform is Windows. (No other platforms are supported in Magicka, so it really can't be anything else...)
            // TODO : Maybe modify this code to be flexible and allow the platform byte to be anything when reading?
            byte platform = reader.ReadByte(); // Should hold value 'w' since Magicka was designed for the Windows OS as primary platform.
            if (platform != (byte)'w')
            {
                logger?.Log(1, $"Platform \"{platform}\" is not valid.");
                throw new MagickaReadExceptionPermissive();
            }
            logger?.Log(1, $"Platform \"{platform}\" is valid (Windows)");

            #endregion

            #region XNB Version

            // Validate version number.
            // Gets the version number and validates that it is an XNB file for XNA 3.1, even tho it does not matter that much in this case.
            byte xnbVersion = reader.ReadByte();
            logger?.Log(1, $"XNA Version : {{ byte = {xnbVersion}, version = {XnaVersion.XnaVersionString(((XnaVersion.XnaVersionByte)xnbVersion))} }}");
            if (xnbVersion != (byte)XnaVersion.XnaVersionByte.Version_3_1)
            {
                logger?.Log(1, "The XNA version is not supported by Magicka!");
                throw new MagickaReadExceptionPermissive();
            }

            #endregion

            #region XNB Flags

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

            #endregion

            // Read file size
            int xnbFileSize = reader.ReadInt32(); // XNB files contain an i32 here that contains the size of the file itself as it is.

            // Handle XNB compression and decompress file if required
            bool isCompressed = isCompressedLz4 || isCompressedLzx;
            if (isCompressed)
            {
                int xnbFileSizeCompressed = xnbFileSize - 14; // The Compressed file size is obtained removing 14 bytes worth of size from the total file size. Those 14 bytes belong to the length of the XNB header and 2 size variables that compressed XNB files have.
                int xnbFileSizeDecompressed = reader.ReadInt32(); // Compressed XNB files contain an extra i32 that contains the expected size of the decompressed data.

                if (isCompressedLzx && isCompressedLz4)
                {
                    logger?.Log(1, "An XNB file cannot have multiple compression types!");
                    throw new MagickaReadExceptionPermissive(); // TODO : Change these with BAD XNB exceptions or something like that? That way we can have a "malformed xnb file: whatever message warning here!!!" kind of logging made by the exceptions rather than logs internal to the XnbFile class...
                }
                else
                if (isCompressedLz4)
                {
                    logger?.Log(1, "File is Compressed with LZ4 compression.");
                    logger?.Log(1, "Cannot read XNB files compressed with LZ4 compression!");
                    throw new MagickaReadExceptionPermissive(); // TODO : Same as the TODO above, and that way we could move the "cannot read etc..." message as an exception parameter.
                }
                else
                if (isCompressedLzx)
                {
                    logger?.Log(1, "File is Compressed with LZX compression.");
                    logger?.Log(1, "Decompressing XNB file...");

                    LzxDecoder dec = new LzxDecoder(16);

                    using (var decompressedStream = new MemoryStream(xnbFileSizeDecompressed)) // NOTE : the buffer created is of a flexible size, so even if the input size is wrong, we can still expand if needed.
                    using (var decompressedReader = new MBinaryReader(decompressedStream))
                    {
                        // TODO : Maybe clean up some of the leftover shit here that is commented out and stuff... some day in the future...
                        while (reader.BaseStream.Position < reader.BaseStream.Length)
                        {
                            // the compressed stream is seperated into blocks that will decompress
                            // into 32Kb or some other size if specified.
                            // normal, 32Kb output blocks will have a short indicating the size
                            // of the block before the block starts
                            // blocks that have a defined output will be preceded by a byte of value
                            // 0xFF (255), then a short indicating the output size and another
                            // for the block size
                            // all shorts for these cases are encoded in big endian order
                            int hi = reader.ReadByte();
                            int lo = reader.ReadByte();
                            int block_size = (hi << 8) | lo;
                            int frame_size = 0x8000; // frame size is 32Kb by default
                                                     // does this block define a frame size?
                            if (hi == 0xFF)
                            {
                                hi = lo;
                                lo = (byte)reader.ReadByte();
                                frame_size = (hi << 8) | lo;
                                hi = (byte)reader.ReadByte();
                                lo = (byte)reader.ReadByte();
                                block_size = (hi << 8) | lo;
                                // pos += 5;
                            }
                            // else
                                // pos += 2;

                            // either says there is nothing to decode
                            if (block_size == 0 || frame_size == 0)
                                break;

                            dec.Decompress(reader.BaseStream, block_size, decompressedStream, frame_size);
                            //pos += block_size;

                            // reset the position of the input just incase the bit buffer
                            // read in some unused bytes
                            // stream.Seek(pos, SeekOrigin.Begin);
                        }

                        if (decompressedStream.Position != xnbFileSizeDecompressed)
                        {
                            logger?.Log(1, $"LZX Decompression failed! Expected output size was {xnbFileSizeDecompressed} but the decompressed size is {decompressedStream.Position}.");
                            throw new MagickaReadExceptionPermissive();
                        }

                        decompressedStream.Seek(0, SeekOrigin.Begin);

                        this.XnbFileData = new XnbFileData(decompressedReader, logger);
                    }
                }
            }
            else
            {
                this.XnbFileData = new XnbFileData(reader, logger);
            }

            logger?.Log(1, "Finished Reading XNB File!");
        }

        #endregion

        #region PublicMethods

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing XNB File...");

            WriteXnbFileHeader(writer, logger);
            WriteXnbFileSizes(writer, logger);
            WriteXnbFileContents(writer, logger);
            WriteXnbFilePaddingBytes(writer, logger);
            // TODO : In the future, we should handle compression related stuff here.

            logger?.Log(1, "Finished Writing XNB File!");
        }

        #endregion

        #region PrivateMethods - Read

        
        #endregion

        #region PrivateMethods - Write

        private void WriteXnbFileHeader(MBinaryWriter writer, DebugLogger logger = null)
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

        private void WriteXnbFileSizes(MBinaryWriter writer, DebugLogger logger = null)
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

        private void WriteXnbFileContents(MBinaryWriter writer, DebugLogger logger = null)
        {
            this.XnbFileData.Write(writer);
        }

        private void WriteXnbFilePaddingBytes(MBinaryWriter writer, DebugLogger logger = null, int numBytes = 64, byte value = 0)
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

        #endregion
    }
}
