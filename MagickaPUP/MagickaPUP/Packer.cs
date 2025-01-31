using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using MagickaPUP.XnaClasses;
using MagickaPUP.MagickaClasses.Map;
using MagickaPUP.IO;

namespace MagickaPUP
{
    class Packer
    {
        #region Variables

        private string inFilename;
        private string outFilename;

        private FileStream writeFile;
        private MBinaryWriter writer;

        private FileStream readFile;
        private StreamReader reader;

        private DebugLogger logger;

        #endregion

        #region Constructor

        public Packer(string infilename, string outfilename, int debuglevel = 1)
        {
            this.inFilename = infilename;
            this.outFilename = outfilename;
            this.logger = new DebugLogger("Packer", debuglevel);
        }

        #endregion

        #region PublicMethods

        public int Pack()
        {
            this.writeFile = new FileStream(outFilename, FileMode.Create, FileAccess.Write);
            this.writer = new MBinaryWriter(this.writeFile);

            this.readFile = new FileStream(inFilename, FileMode.Open, FileAccess.Read);
            this.reader = new StreamReader(readFile);

            this.WriteHeader();
            // this.WriteContentTypeReaders_OLD();

            string contents = ReadJSONFile();
            XnbFileObject obj = DeserializeJSONFile(contents);

            if (obj == null)
                throw new Exception("The JSON file is not valid and has produced a nul object!");

            this.WriteContentTypeReaders(obj);

            this.WriteSharedResourceCount(obj);
            this.WritePrimaryObject(obj);
            this.WriteSharedResources(obj);


            // Pad with NUL bytes the end of the file. This is not mandated by the XNA spec, but it is here to save slightly malformed files from crashing.
            // This should only happen if someone writing the input files accidentally write some information that is not correct, but correct
            // enough to work if we prevent the game from throwing an exception by reading out of bounds in the file stream by adding this
            // extra bit of padding... depending on how big the fuck up is, the padding needed to save the file could potentially be massive,
            // so by default we're writing 64 bytes with the value 0 (NUL).
            this.WritePaddingBytes();

            logger?.Log(1, "Finished writing XNB file!");

            return 0;
        }

        #endregion

        // README : A lot of this functionality could be moved into the XnbFileObject class if we did what the TODO in that class said,
        // which is implementing the packer and unpacker logic within the read and write functions.

        #region PrivateMethodsSharedResources

        // README : If the shared resource count is in the input JSON, always append a null object at the end.
        // Otherwise, don't append anything and just write the shared resources.
        // This is done for padding reasons, because apparently Magicka requires at least 1 shared resource to exist, so many official maps just bundle
        // a null shared resource at the end of the file.

        private void WriteSharedResourceCount(XnbFileObject obj)
        {
            logger?.Log(1, "Writing Shared Resource Count...");
            int count = obj.numSharedResources <= 0 ? 1 : obj.numSharedResources;
            writer.Write7BitEncodedInt((int)count);
        }

        private void WriteSharedResources(XnbFileObject obj)
        {
            logger?.Log(1, "Writing Shared Resources...");

            if (obj.numSharedResources <= 0)
            {
                XnaObject.WriteEmptyObject(writer, logger);
            }
            else
            {
                for (int i = 0; i < obj.numSharedResources; ++i)
                {
                    XnaObject.WriteObject(obj.sharedResources[i], writer, logger);
                }
            }
        }

        #endregion

        #region PrivateMethods

        private void WriteHeader()
        {
            logger?.Log(1, "Writing XNB Header...");

            byte[] bytes = { (byte)'X', (byte)'N', (byte)'B', (byte)'w', (byte)XnaVersion.XnaVersionByte.Version_3_1, 0 };
            writer.Write(bytes);

            // These sizes are placeholders. For now, we just write the max possible size and call it a day.
            ushort packedSize = 65535;
            ushort unpackedSize = 65535;
            writer.Write(packedSize);
            writer.Write(unpackedSize);
        }

        // DEPRECATED
        // TODO : Remove old unused code...
        private void WriteContentTypeReaders_OLD()
        {
            logger?.Log(1, "Writing Content Type Readers...");

            // README : For now, we'll just add all of the type readers to the files because we can.
            // In the future, it would be cool if we could just pick and choose those that are needed, but
            // does it really matter that much? it barely has any overhead (as of now...), so...
            // better to just bundle all of the known type readers just in case and call it a day.

            // pass it to an int to make sure that the number is in the right type... who knows, maybe a future C#
            // implementation of the containers library will make it so that .Count() returns an unsigned type,
            // and maybe the unerlying implementation of the write 7 bit encoded int method could break with that.
            // Better not risk it with future changes and enforce the correct type, I guess!
            int numReaders = XnaInfo.ContentTypeReaders.Length;

            writer.Write7BitEncodedInt(numReaders);

            for (int i = 0; i < numReaders; ++i)
            {
                XnaInfo.ContentTypeReaders[i].WriteInstance(writer, logger);
            }
        }

        private void WriteContentTypeReaders(XnaObject obj)
        {
            logger?.Log(1, "Writing Content Type Readers...");
            var readers = obj.GetRequiredContentReaders();
            if (readers.Length > 0)
            {
                // If the obtained object defines its own content type reader list, then we write those...
                writer.Write7BitEncodedInt(readers.Length);
                foreach (var reader in readers)
                    reader.WriteInstance(writer, logger);
            }
            else
            {
                // If the obtained object does not define its own content type reader list, then we write them all just in case...
                writer.Write7BitEncodedInt(XnaInfo.ContentTypeReaders.Length);
                foreach (var reader in XnaInfo.ContentTypeReaders)
                    reader.WriteInstance(writer, logger);
            }
        }

        private string ReadJSONFile()
        {
            logger?.Log(1, "Reading input JSON file...");

            // Read the contents of the JSON file
            string contents = reader.ReadToEnd();

            return contents;
        }

        private XnbFileObject DeserializeJSONFile(string contents)
        {
            logger?.Log(1, "Deserializing input JSON file...");

            // Deserialize the JSON file into a tree-like C# class structure
            XnbFileObject obj = JsonSerializer.Deserialize<XnbFileObject>(contents);

            return obj;
        }

        private void WritePrimaryObject(XnbFileObject obj)
        {
            logger?.Log(1, "Writing Primary Object...");

            // Write the primary object's required reader 7 bit integer, then write the primary object itself
            XnaObject.WriteObject(obj.primaryObject, writer, logger);
        }

        private void WritePaddingBytes(int numBytes = 64, byte value = 0)
        {
            logger?.Log(1, "Writing padding bytes...");
            logger?.Log(2, $" - Bytes : {numBytes}");
            logger?.Log(2, $" - Value : {value}");

            byte[] bytes = new byte[numBytes];
            for (int i = 0; i < numBytes; ++i)
                bytes[i] = value;
            writer.Write(bytes);
        }

        #endregion
    }
}
