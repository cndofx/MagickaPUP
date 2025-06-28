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
            CleanUpFileNames();

            this.readFile = new FileStream(this.readFilename, FileMode.Open, FileAccess.Read);
            this.reader = new StreamReader(readFile);

            this.writeFile = new FileStream(this.writeFilename, FileMode.Create, FileAccess.Write);
            this.writer = new MBinaryWriter(this.writeFile);

            try
            {
                var xnbFile = ReadSystemFile();
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

        private XnbFile ReadFileJson()
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

        private XnbFile ReadFilePng()
        {
            // TODO : Implement!!! (the logic was moved due to a bug, now needs to be reimplemented... remember to do that at some point in the future!)
            
            // Read the contents of the PNG file
            //etc...

            // Deserialize the PNG file into the C# XNB File class structure
            // etc...

            // Throw an exception if the read PNG is not valid
            // etc...

            return null;
        }

        private XnbFile ReadSystemFile()
        {
            // TODO : Maybe roll this back to use the system where we detected the file type based on contents rather than extension? although that was a little bit
            // slower, and more unix-like, less windows-like, so most users would be quite possibly confused by the idea that extensions don't really mean shit for
            // the actual binary data stored within the file itself...

            string extension = Path.GetExtension(readFilename).ToLower();
            if (extension == ".json")
            {
                return ReadFileJson();
            }
            else
            if (extension == ".png")
            {
                return ReadFilePng();
            }
            else
            {
                throw new Exception("Unsupported file type detected!");
            }
        }

        private void WriteXnbFile(XnbFile xnbFile)
        {
            xnbFile.Write(writer, logger);
        }

        private void CleanUpFileNames()
        {
            string writeExtension = Path.GetExtension(this.writeFilename).ToLower();
            if (writeExtension != ".xnb")
                writeFilename += ".xnb";
        }

        #endregion
    }
}
