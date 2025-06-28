using MagickaPUP.Utility.Exceptions;
using MagickaPUP.Utility.IO;
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
    class Unpacker
    {
        #region Variables

        private string readfilename;
        private string writefilename;
        private bool shouldIndent;

        private MBinaryReader reader;
        private DebugLogger logger;
        private ReadContext context;

        #endregion

        #region Constructors

        public Unpacker(string infilename, string outfilename, int debugLevel = 1, bool shouldIndent = false)
        {
            this.readfilename = infilename;
            this.writefilename = outfilename;
            this.logger = new DebugLogger("Unpacker", debugLevel);
            this.context = new ReadContext(reader, logger);
            this.shouldIndent = shouldIndent;
        }

        #endregion

        #region PublicMethods

        public int Unpack()
        {
            byte[] data = File.ReadAllBytes(this.readfilename);
            this.reader = new MBinaryReader(new MemoryStream(data));

            try
            {
                var xnbFile = ReadXnbFile();
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

        private XnbFile ReadXnbFile()
        {
            // Read the input XNB file
            XnbFile xnbFile = new XnbFile(this.reader, this.logger);
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
            string nameBase = Path.GetFileNameWithoutExtension(name);
            string extension = Path.GetExtension(name);
            bool hasExtension = extension.Length > 0;
            
            string chosenExtension;
            Action<string, XnbFile> chosenFunction = null;

            // Texture2D needs to be processed as image files
            if (xnbFile.XnbFileData.PrimaryObject is Texture2D)
            {
                chosenExtension = ".png";
                chosenFunction = WriteFilePng;
            }
            // All other types are treated as JSON
            else
            {
                chosenExtension = ".json";
                chosenFunction = WriteFileJson;
            }

            // If the user has provided their own extension, use that instead of the automatically chosen one
            if (hasExtension)
                chosenExtension = extension;

            // Call the function and generate the output file
            chosenFunction($"{nameBase}{chosenExtension}", xnbFile);

            // TODO : In the future, maybe change it so that the provided extension is not just a visual thing. Like, make it so that
            // when I write ".json", I can force even image files to be generated as json files without having to provide any other compilation flags that are
            // specific to image files.
            // That way, we can explore the XNB contents of the image freely from within the JSON without must more effort, including the other mip maps.
        }

        #endregion
    }
}
