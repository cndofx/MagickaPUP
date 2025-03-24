using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Xna
{
    public static class XnaUtility
    {
        public static T ReadObject<T>(MBinaryReader reader, DebugLogger logger = null) where T : class
        {
            T ans = default(T);

            int indexXnb = reader.Read7BitEncodedInt();
            int indexMem = indexXnb - 1;

            if (indexXnb == 0)
            {
                logger?.Log(1, "Reading NULL Object");
                return null;
            }

            if (indexMem < 0 || indexMem >= reader.ContentTypeReaderStorage.Count)
            {
                throw new Exception($"Requested Content Type Reader does not exist! (Index = {indexXnb})");
            }

            ContentTypeReader contentTypeReader = reader.ContentTypeReaderStorage.GetReader(indexMem);
            LogContentTypeReader(contentTypeReader, logger);

            var typeReader = reader.ContentTypeReaderManager.Get(contentTypeReader);
            ans = (T)typeReader.Read(reader, logger); // TODO : Implement a check that throws an exception if the requested reader returns a type that is not casteable to T.

            return ans;
        }

        public static void WriteObject<T>(T obj, MBinaryWriter writer, DebugLogger logger = null)
        {
            if (obj == null)
            {
                WriteNullObject(writer, logger);
                return;
            }

            // TODO : Implement
        }

        private static void WriteNullObject(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing NULL object to XNB file...");
            writer.Write7BitEncodedInt(0);
        }

        private static void LogContentTypeReader(ContentTypeReader reader, DebugLogger logger)
        {
            logger?.Log(1, $"Required ContentTypeReader : {{ Name = \"{reader.Name}\", Version = {reader.Version} }}");
        }
    }
}
