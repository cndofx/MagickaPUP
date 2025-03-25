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
            logger?.Log(1, ()=>$"Requesting ContentTypeReader {GetContentTypeReaderFormattedString(contentTypeReader)} to read object of type \"{typeof(T).GetType().Name}\"");

            logger?.Log(1, () => $"Reading XNA Object with required ContentTypeReader {GetContentTypeReaderFormattedString(contentTypeReader)}");
            var typeReader = reader.ContentTypeReaderManager.GetTypeReader(contentTypeReader);
            ans = (T)typeReader.Read(null, reader, logger); // TODO : Implement a check that throws an exception if the requested reader returns a type that is not casteable to T.

            return ans;
        }

        public static void WriteObject<T>(T obj, MBinaryWriter writer, DebugLogger logger = null)
        {
            if (obj == null)
            {
                WriteNullObject(writer, logger);
                return;
            }

            var contentTypeReader = writer.ContentTypeReaderManager.GetContentTypeReader(obj.GetType()); // NOTE : That the GetContentTypeReader() method returns one of the versions of the reader for the requested type. If you want to use a different version of the reader, then you can just change the .Version field from the returned content type reader struct and use that. If the version exists, it will be used instead. Obviously, this is just a workaround for now, which doesn't really matter since in Magicka all content type readers only have a single version (AFAIK)...
            var typeWriter = writer.ContentTypeReaderManager.GetTypeWriter(contentTypeReader);
            int indexXnb;
            int indexMem;

            logger?.Log(1, ()=>$"Requesting ContentTypeReader {GetContentTypeReaderFormattedString(contentTypeReader)} to read object of type \"{obj.GetType().Name}\"");
            indexMem = writer.ContentTypeReaderStorage.GetReaderIndex(contentTypeReader);
            if (indexMem < 0)
                indexMem = writer.ContentTypeReaderStorage.AddReader(contentTypeReader);
            indexXnb = indexMem + 1;

            logger?.Log(1, ()=>$"Writing XNA Object with required ContentTypeReader {GetContentTypeReaderFormattedString(contentTypeReader)}");
            writer.Write7BitEncodedInt(indexXnb);
            typeWriter.Write(obj, writer, logger);
        }

        // WriteObject overload that specifies the version of the type to write so that other programs that will read this data will know which version of their
        // readers for this content type they must invoke.
        // public static void WriteObject<T>(T obj, int version, MBinaryWriter writer, DebugLogger logger = null)
        // {
        //     // TODO : Implement
        // }

        private static void WriteNullObject(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing NULL object to XNB file...");
            writer.Write7BitEncodedInt(0);
        }

        private static string GetContentTypeReaderFormattedString(ContentTypeReader reader)
        {
            return $"{{ Name : \"{reader.Name}\", Version : {reader.Version} }}";
        }
    }
}
