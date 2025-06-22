using MagickaPUP.Utility.Exceptions;
using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses;
using MagickaPUP.XnaClasses.Xna.Data;
using MagickaPUP.XnaClasses.Xnb;
using System;
using System.Collections.Generic;
using System.IO;
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
            // Texture2D needs to be processed as image files
            if (xnbFile.XnbFileData.PrimaryObject is Texture2D)
            {
                WriteFilePng($"{name}.png", xnbFile);
            }
            // All other types are treated as JSON
            else
            {
                WriteFileJson($"{name}.xnb", xnbFile);
            }
        }

        #endregion
    }
}
