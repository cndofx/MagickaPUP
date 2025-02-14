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

        private FileStream readFile;
        private FileStream writeFile;

        private MBinaryReader reader;
        private StreamWriter writer;

        private DebugLogger logger;

        private ReadContext context;

        #endregion

        #region Constructors

        public Unpacker(string infilename, string outfilename, int debugLevel = 1)
        {
            this.readfilename = infilename;
            this.writefilename = outfilename;
            this.logger = new DebugLogger("Unpacker", debugLevel);
            this.context = new ReadContext(reader, logger);
        }

        #endregion

        #region PublicMethods

        public int Unpack()
        {
            this.readFile = new FileStream(this.readfilename, FileMode.Open, FileAccess.Read);
            this.reader = new MBinaryReader(this.readFile);

            this.writeFile = new FileStream(this.writefilename, FileMode.Create, FileAccess.Write);
            this.writer = new StreamWriter(this.writeFile);

            try
            {
                var xnbFile = ReadXnbFile();
                WriteJsonFile(xnbFile);
            }
            catch (MagickaReadExceptionPermissive)
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

        private void WriteJsonFile(XnbFile xnbFile)
        {
            // Write the output JSON file
            logger?.Log(1, "Writing JSON file...");
            writer.Write(JsonSerializer.Serialize(xnbFile, new JsonSerializerOptions() { WriteIndented = true }));
            writer.Flush();
            logger?.Log(1, "Finished writing JSON file!");
        }

        #endregion
    }
}
