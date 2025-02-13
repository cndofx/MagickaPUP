using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses.Xna.Data;
using System;
using System.Collections.Generic;
using MagickaPUP.XnaClasses.Xnb.Data;

namespace MagickaPUP.XnaClasses.Xnb
{
    public class XnbFile
    {
        #region Variables

        public XnbHeader Header { get; set; } // TODO : Implement reading and writing for the header data...
        public XnaObject PrimaryObject { get; set; }
        public List<XnaObject> SharedResources { get; set; }

        #endregion

        #region Constructor

        public XnbFile()
        {
            this.PrimaryObject = new XnaObject();
            this.SharedResources = new List<XnaObject>();
        }

        public XnbFile(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading XNB File...");

            // Validate the input data to check if it is a valid XNB file
            logger?.Log(1, "Validating XNB File...");
            char x = reader.ReadChar();
            char n = reader.ReadChar();
            char b = reader.ReadChar();
            string headerString = $"{x}{n}{b}";
            if (!(x == 'X' && n == 'N' && b == 'B'))
            {
                logger.Log(1, $"Header \"{headerString}\" is not valid!");
                return;
            }
            logger?.Log(1, "Header \"XNB\" is valid!");

            // Perform platform validation.
            // Check if the platform is Windows. (No other platforms are supported in Magicka, so it really can't be anything else...)
            char platform = reader.ReadChar(); // TODO : Maybe replace this with a ReadByte() call to ensure that users with a different system encoding don't break the program?
            if (platform != 'w')
            {
                logger.Log(1, $"Platform \"{platform}\" is not valid.");
                return;
            }
            logger?.Log(1, $"Platform \"{platform}\" is valid (Windows)");

            // Validate version number.
            // Gets the version number and validates that it is an XNB file for XNA 3.1, even tho it does not matter that much in this case.
            byte xnbVersion = reader.ReadByte();
            logger?.Log(1, $"XNA Version : {{ byte = {xnbVersion}, version = {XnaVersion.XnaVersionString(((XnaVersion.XnaVersionByte)xnbVersion))} }}");
            if (xnbVersion != (byte)XnaVersion.XnaVersionByte.Version_3_1)
            {
                logger?.Log(1, "The XNA version is not supported by Magicka!");
                return;
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
            if (isCompressedLz4 || isCompressedLzx) // TODO : Implement decompression support in the future!
            {
                logger?.Log(1, "Cannot read compressed files!");
                return;
            }
        }

        #endregion

        #region PublicMethods

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {

        }

        public void SetPrimaryObject(XnaObject obj)
        {
            this.PrimaryObject = obj;
        }

        public void AddSharedResource(XnaObject obj)
        {
            this.SharedResources.Add(obj);
        }

        // MAYBE TODO : Maybe move all of the Packer and Unpacker logic to the Read and Write functions of this object? and we could also add other data such as the xnb header stuff as private or non json serializable variables and whatnot...
        // INDEED, It would make quite a lot of sense to move the read and write logic to this object rigth here to make things easier, including the header and stuff like that...

        #endregion
    }
}
