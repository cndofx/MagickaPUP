using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using MagickaPUP.XnaClasses;
using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses.Xnb;
using MagickaPUP.XnaClasses.Xna.Data;
using MagickaPUP.Utility.Exceptions;

namespace MagickaPUP.Core
{
    class Packer
    {
        #region Variables

        private string readFilename;
        private string writeFilename;

        private FileStream writeFile;
        private FileStream readFile;

        private StreamReader reader;
        private MBinaryWriter writer;

        private DebugLogger logger;

        private WriteContext context;

        #endregion

        #region Constructor

        public Packer(string infilename, string outfilename, int debuglevel = 1)
        {
            this.readFilename = infilename;
            this.writeFilename = outfilename;
            this.logger = new DebugLogger("Packer", debuglevel);
            this.context = new WriteContext(writer, logger);
        }

        #endregion

        #region PublicMethods

        public int Pack()
        {
            this.readFile = new FileStream(this.readFilename, FileMode.Open, FileAccess.Read);
            this.reader = new StreamReader(readFile);

            this.writeFile = new FileStream(this.writeFilename, FileMode.Create, FileAccess.Write);
            this.writer = new MBinaryWriter(this.writeFile);

            try
            {
                var xnbFile = ReadJsonFile();
                WriteXnbFile(xnbFile);
            }
            catch (MagickaWriteExceptionPermissive) // NOTE : If you think about it, all magicka exceptions are isolated to their specific file, so we don't really need a "permissive" one, just catch the base MagickaException class and call it a day! Altough that would remove support of knowing where an specific exception took place, we can always know where the error comes from by reading the exception message.
            {
                logger?.Log(1, "Cancelling Pack Operation...");
            }

            return 0; // TODO : Implement success counting on the top level program so that we can print how many operations succeeded after we finished
        }

        #endregion

        #region PrivateMethods

        private XnbFile ReadJsonFile()
        {
            // Read the contents of the JSON file
            logger?.Log(1, "Reading input JSON file...");
            string contents = reader.ReadToEnd();

            // Deserialize the JSON file into a tree-like C# class structure
            logger?.Log(1, "Deserializing input JSON file...");
            XnbFile obj = JsonSerializer.Deserialize<XnbFile>(contents);

            // Throw an exception if the read JSON object is not valid
            if (obj == null)
                throw new Exception("The JSON file is not valid and has produced a NULL object!");

            return obj;
        }

        private void WriteXnbFile(XnbFile xnbFile)
        {
            xnbFile.Write(writer, logger);
        }

        #endregion
    }
}
