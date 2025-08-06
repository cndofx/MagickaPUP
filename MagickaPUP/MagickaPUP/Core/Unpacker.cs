using MagickaPUP.Utility.Exceptions;
using MagickaPUP.Utility.FileSystem;
using MagickaPUP.Utility.IO;
using MagickaPUP.Utility.IO.Data;
using MagickaPUP.XnaClasses;
using MagickaPUP.XnaClasses.Xna.Data;
using MagickaPUP.XnaClasses.Xnb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace MagickaPUP.Core
{
    // TODO : Modify the Unpacker class to use this as data holder and constructor input parameters instead of what it uses right now.
    // Also do the same for the Packer class.
    struct UnpackerSettings
    {
        public string InputFileName;
        public string OutputFileName;

        public int DebugLevel;

        public bool JsonShouldIndent;

        public GameVersion GameVersion;
    }

    class Unpacker
    {
        #region Variables

        private string readfilename;
        private string writefilename;
        private bool shouldIndent;
        private GameVersion gameVersion;

        private DebugLogger logger;

        #endregion

        #region Constructors

        public Unpacker(string infilename, string outfilename, int debugLevel = 1, bool shouldIndent = false, GameVersion gameVersion = GameVersion.Auto)
        {
            this.readfilename = infilename;
            this.writefilename = outfilename;
            this.logger = new DebugLogger("Unpacker", debugLevel);
            this.shouldIndent = shouldIndent;
            this.gameVersion = gameVersion;
        }

        #endregion

        #region PublicMethods

        public int Unpack()
        {
            try
            {
                var xnbFile = ReadXnbFile(this.readfilename);
                WriteSystemFile(this.writefilename, xnbFile);
            }
            catch (MagickaReadExceptionPermissive) // NOTE : If you think about it, all magicka exceptions are isolated to their specific file, so we don't really need a "permissive" one, just catch the base MagickaException class and call it a day!
            {
                logger?.Log(1, "Cancelling Unpack Operation...");
            }

            return 0; // TODO : Implement success counting on the top level program so that we can print how many operations succeeded after we finished
        }

        #endregion

        #region PrivateMethods

        private XnbFile ReadXnbFile(string name)
        {
            XnbFile xnbFile;

            // Read the data from the input file as bytes
            byte[] data = File.ReadAllBytes(name);

            // Create binary reader and read the input XNB file data into an instance of the XnbFile class
            using (var stream = new MemoryStream(data))
            using (var reader = new MBinaryReader(stream, this.gameVersion))
            {
                xnbFile = new XnbFile(reader, this.logger);
            }

            // return the newly created file
            return xnbFile;
        }

        private void WriteFileJson(string name, XnbFile xnbFile)
        {
            // Write the output JSON file
            logger?.Log(1, "Writing JSON file...");
            File.WriteAllText(name, JsonSerializer.Serialize(xnbFile, new JsonSerializerOptions() { WriteIndented = this.shouldIndent }));
            logger?.Log(1, "Finished writing JSON file!");
        }

        private void WriteFilePng(string name, XnbFile xnbFile)
        {
            //Write the output PNG file
            logger?.Log(1, "Writing PNG file...");
            var tex = xnbFile.XnbFileData.PrimaryObject as Texture2D;
            var bitmap = tex.GetBitmap();
            bitmap.Save(name, System.Drawing.Imaging.ImageFormat.Png);
            logger?.Log(1, "Finished writing PNG file!");
        }

        private void WriteSystemFile(string name, XnbFile xnbFile)
        {
            string chosenExtension;
            Action<string, XnbFile> chosenFunction;

            // Texture2D needs to be processed as image files
            if (xnbFile.XnbFileData.PrimaryObject is Texture2D)
            {
                chosenExtension = ".png";
                chosenFunction = WriteFilePng;
            }
            // All other types are treated as JSON (for now)
            else
            {
                chosenExtension = ".json";
                chosenFunction = WriteFileJson;
            }

            // Generate the string for the final file path
            string finalPath = FSUtil.GetPathWithoutExtension(name) + chosenExtension;

            // Call the function and generate the output file
            chosenFunction(finalPath, xnbFile);

            // TODO : In the future, maybe change it so that the provided extension is not just a visual thing. Like, make it so that
            // when I write ".json", I can force even image files to be generated as json files without having to provide any other compilation flags that are
            // specific to image files.
            // That way, we can explore the XNB contents of the image freely from within the JSON without must more effort, including the other mip maps.
        }

        #endregion
    }
}
