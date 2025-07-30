using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using MagickaPUP.XnaClasses;
using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses.Xnb;
using MagickaPUP.XnaClasses.Xna.Data;
using MagickaPUP.Utility.Exceptions;
using MagickaPUP.Utility.FileSystem;

namespace MagickaPUP.Core
{
    class Packer
    {
        #region Variables

        private string readFilename;
        private string writeFilename;

        private DebugLogger logger;

        #endregion

        #region Constructor

        public Packer(string infilename, string outfilename, int debuglevel = 1)
        {
            this.readFilename = infilename;
            this.writeFilename = outfilename;
            this.logger = new DebugLogger("Packer", debuglevel);
        }

        #endregion

        #region PublicMethods

        public int Pack()
        {
            try
            {
                var xnbFile = ReadSystemFile(this.readFilename);
                WriteXnbFile(this.writeFilename, xnbFile);
            }
            catch (MagickaWriteExceptionPermissive) // NOTE : If you think about it, all magicka exceptions are isolated to their specific file, so we don't really need a "permissive" one, just catch the base MagickaException class and call it a day! Altough that would remove support of knowing where an specific exception took place, we can always know where the error comes from by reading the exception message.
            {
                logger?.Log(1, "Cancelling Pack Operation...");
            }

            return 0; // TODO : Implement success counting on the top level program so that we can print how many operations succeeded after we finished
        }

        #endregion

        #region PrivateMethods

        private XnbFile ReadFileJson(string name)
        {
            XnbFile xnbFile;

            // Read the contents of the JSON file
            logger?.Log(1, "Reading input JSON file...");
            string contents = File.ReadAllText(name);

            // Deserialize the JSON file into a tree-like C# class structure
            logger?.Log(1, "Deserializing input JSON file...");
            xnbFile = JsonSerializer.Deserialize<XnbFile>(contents);

            // Throw an exception if the read JSON object is not valid
            if (xnbFile == null)
                throw new Exception("The JSON file is not valid and has produced a NULL object!");

            return xnbFile;
        }

        private XnbFile ReadFilePng(string name)
        {
            logger?.Log(1, "Reading input PNG file...");

            byte[] data = File.ReadAllBytes(name);

            // TODO : Implement!!! (the logic was moved due to a bug, now needs to be reimplemented... remember to do that at some point in the future!)
            
            // Read the contents of the PNG file
            //etc...

            // Deserialize the PNG file into the C# XNB File class structure
            // etc...

            // Throw an exception if the read PNG is not valid
            // etc...

            return null;
        }

        private XnbFile ReadSystemFile(string name)
        {
            // TODO : Maybe roll this back to use the system where we detected the file type based on contents rather than extension? although that was a little bit
            // slower, and more unix-like, less windows-like, so most users would be quite possibly confused by the idea that extensions don't really mean shit for
            // the actual binary data stored within the file itself...

            XnbFile ans;

            logger?.Log(1, "Reading Input File...");

            string extension = Path.GetExtension(readFilename).ToLower();
            if (extension == ".json") // NOTE : When more extensions are supported, this should probably be changed into a switch(), even tho switch on string compiles to the same as an if-else ladder, it still has better readability...
            {
                ans = ReadFileJson(name);
            }
            else
            if (extension == ".png")
            {
                ans = ReadFilePng(name);
            }
            else
            {
                throw new Exception("Unsupported file type detected!");
            }

            logger?.Log(1, "Finished Reading Input File!");
            return ans;
        }

        private void WriteXnbFile(string name, XnbFile xnbFile)
        {
            string finalPath = FSUtil.GetPathWithoutExtension(name) + ".xnb";
            using (var stream = new FileStream(finalPath, FileMode.Create, FileAccess.Write))
            using (var writer = new MBinaryWriter(stream))
            {
                xnbFile.Write(writer, logger);
            }
        }

        #endregion
    }
}
