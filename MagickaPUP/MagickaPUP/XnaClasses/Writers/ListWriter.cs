using MagickaPUP.MagickaClasses.Liquids;
using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Writers
{
    public class ListWriter<T> where T : XnaObject
    {
        /*
        public void Write(T list, MBinaryWriter writer, DebugLogger logger)
        {
            ContentTypeReader contentTypeReader = list.GetListContentTypeReader();
            int index;
            int realIndex;

            logger?.Log(1, $"Requesting ContentTypeReader \"{contentTypeReader.Name}\" to read type \"{list.GetType().Name}\"");
            index = writer.ContentTypeReaders.GetReaderIndex(contentTypeReader.Name);
            if (index < 0)
                index = writer.ContentTypeReaders.AddReader(contentTypeReader);
            realIndex = index + 1;

            logger?.Log(1, $"Writing List of XNA Objects with required ContentTypeReader \"{contentTypeReader.Name}\", index {realIndex}...");
            writer.Write7BitEncodedInt(realIndex);
            writer.Write((int)list.Count);
            if (list.Count <= 0)
                return;
            for (int i = 0; i < list.Count; ++i)
                list[i].WriteInstance(writer, logger);
        }
        */
    }
}
