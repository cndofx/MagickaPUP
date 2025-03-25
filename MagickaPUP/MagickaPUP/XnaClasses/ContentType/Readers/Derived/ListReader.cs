using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses.ContentType.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class ListReader<T, TReader> : TypeReader<List<T>> where T : class where TReader : TypeReaderBase, new()
    {
        public ListReader()
        { }

        public override List<T> Read(List<T> instance, MBinaryReader reader, DebugLogger logger = null)
        {
            TypeReaderBase elementReader = new TReader();

            List<T> ans = new List<T>();
            int count = reader.ReadInt32();
            for (int i = 0; i < count; ++i)
            {
                T x = (T)elementReader.Read(null, reader, logger);
                ans.Add(x);
            }

            return ans;
        }
    }
}
