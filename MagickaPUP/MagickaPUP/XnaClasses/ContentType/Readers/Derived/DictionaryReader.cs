using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses.Readers;
using MagickaPUP.XnaClasses.Specific;
using MagickaPUP.XnaClasses.Xna;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.ContentType.Readers.Derived
{
    public class DictionaryReader<KeyT, ValueT, KeyReader, ValueReader> : TypeReader<Dictionary<KeyT, ValueT>>
        where KeyT : class
        where ValueT : class
        where KeyReader : TypeReaderBase, new()
        where ValueReader : TypeReaderBase, new()
    {
        private TypeReaderBase keyReader;
        private TypeReaderBase valueReader;

        public DictionaryReader()
        { }

        public override Dictionary<KeyT, ValueT> Read(Dictionary<KeyT, ValueT> instance, MBinaryReader reader, DebugLogger logger = null)
        {
            TypeReaderBase keyReader = new KeyReader();
            TypeReaderBase valueReader = new ValueReader();

            int capacity = reader.ReadInt32();
            Dictionary<KeyT, ValueT> ans = new Dictionary<KeyT, ValueT>(capacity);
            for (int i = 0; i < capacity; ++i)
            {
                KeyT key = (KeyT)keyReader.Read(null, reader, logger);
                ValueT value = (ValueT)valueReader.Read(null, reader, logger);
                ans.Add(key, value);
            }
            return ans;
        }
    }
}
