using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses.Specific;
using MagickaPUP.XnaClasses.Xna;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.ContentType.Readers.Derived
{
    public class DictionaryReader<KeyT, ValueT> : TypeReader<Dictionary<KeyT, ValueT>> where KeyT : class where ValueT : class
    {
        public DictionaryReader()
        { }

        public override Dictionary<KeyT, ValueT> Read(Dictionary<KeyT, ValueT> instance, MBinaryReader reader, DebugLogger logger = null)
        {
            int capacity = reader.ReadInt32();
            Dictionary<KeyT, ValueT> ans = new Dictionary<KeyT, ValueT>(capacity);
            for (int i = 0; i < capacity; ++i)
            {
                KeyT key = XnaUtility.ReadObject<KeyT>(reader, logger);
                ValueT value = XnaUtility.ReadObject<ValueT>(reader, logger);
                ans.Add(key, value);
            }
            return ans;
        }
    }
}
