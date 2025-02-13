using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses;
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

            logger.Log(1, "Validaing XNB file...");
            // Validate as XNB file
            char x = reader.ReadChar();
            char n = reader.ReadChar();
            char b = reader.ReadChar();

            string headerString = $"{x}{n}{b}";

            if (!(x == 'X' && n == 'N' && b == 'B'))
            {
                logger.Log(1, $"Header \"{headerString}\" is not valid.");
                return 1;
            }

            logger.Log(1, $"Header \"{x}{n}{b}\" is valid.");

            // Check that it is for windows (no other platforms are supported so it really can't be anything else)
            char w = reader.ReadChar();
            if (w != 'w')
            {
                logger.Log(1, $"Platform \"{w}\" is not valid.");
                return 2;
            }
            logger.Log(1, $"Platform \"{w}\" is valid (Windows)");

            // Get Version number, even tho it does not matter that much in this case.
            int xnbVersion = reader.ReadByte();
            logger.Log(1, $"XNA Version : {{ byte = {xnbVersion}, version = {XnaVersion.XnaVersionString(((XnaVersion.XnaVersionByte)xnbVersion))} }}");

            // Get compression. Should always be uncompressed to be able to read it. Expect this vlaue to be 0x00. If it's 0x80 or anything else, bail out.
            int compressed = reader.ReadByte();
            bool isCompressed = (compressed & 0x80) == 0x80;

            logger.Log(1, $"Compressed : {isCompressed} (byte = {compressed})");

            if (isCompressed)
            {
                logger.Log(1, "Cannot read compressed files.");
                return 3;
            }

            // Get sizes. They can be whatever, it doesn't really matter since Magicka doesn't use these values actually...
            // btw, these values should be ushort but I'm keeping them as uint as originally, the XNB file reference I was following was for XNA 4.0 and those use u32 for the size variables. In short, I'm just keeping it like this to remember and for furutre feature support etc etc... could really just be changed to ushort without any problems tbh. Actually, the writer does use ushorts so yeah lol...
            uint packedSize = reader.ReadUInt16();
            uint unpackedSize = reader.ReadUInt16();
            logger.Log(1,
                $"File Size :\n" +
                $" - Packed   : {packedSize}\n" +
                $" - Unpacked : {unpackedSize}"
            );

            // Get the amount of TypeReaders and iterate through all of them.
            int typeReaderCount = reader.Read7BitEncodedInt();
            logger.Log(1, $"Content Type Reader Count : {typeReaderCount}");
            for (int i = 0; i < typeReaderCount; ++i)
            {
                ContentTypeReader currentReader = ContentTypeReader.Read(reader, logger);
                reader.ContentTypeReaders.Add(currentReader);
            }

            // Get shared resource amount.
            int sharedResourceCount = reader.Read7BitEncodedInt();
            logger.Log(1, $"Shared Resource Count : {sharedResourceCount}");

            // Create variable for XNB file container.
            XnbFileObject xnbFileObject = new XnbFileObject();

            // Read primary objects.
            logger.Log(1, "Reading Primary Object...");
            Wait();
            XnaObject primaryObject = XnaObject.ReadObject<XnaObject>(reader, logger);
            xnbFileObject.SetPrimaryObject(primaryObject);
            logger?.Log(1, "Finished Reading Primary Object!");
            Wait();

            // Read shared resources and store them.
            logger.Log(1, "Reading Shared Resources...");
            for (int i = 0; i < sharedResourceCount; ++i)
            {
                logger.Log(1, $"Reading Shared Resource {(i + 1)} / {sharedResourceCount}...");
                Wait();
                var sharedResource = XnaObject.ReadObject<XnaObject>(reader, logger);
                xnbFileObject.AddSharedResource(sharedResource);
                Wait();
            }
            logger.Log(1, "Finished reading XNB file!");

            // Write the JSON file.
            logger.Log(1, "Writing JSON file...");
            writer.Write(JsonSerializer.Serialize(xnbFileObject, new JsonSerializerOptions() { WriteIndented = true }));
            writer.Flush();
            logger.Log(1, "Finished writing JSON file!");

            return 0;
        }

        #endregion

        #region PrivateMethods
        #endregion

        #region Debug

        private void Wait()
        {
            /*
            Console.WriteLine("Press <ENTER> to continue...");
            Console.ReadLine();
            */
        }

        #endregion

    }
}
