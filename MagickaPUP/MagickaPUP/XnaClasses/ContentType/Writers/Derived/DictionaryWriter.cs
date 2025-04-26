using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses.Xna;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.ContentType.Writers.Derived
{
    public class DictionaryWriter<KeyT, ValueT> : TypeWriter<Dictionary<KeyT, ValueT>> where KeyT : class where ValueT : class
    {
        public DictionaryWriter()
        { }

        public override void Write(Dictionary<KeyT, ValueT> instance, MBinaryWriter writer, DebugLogger logger = null)
        {
            // TODO : Implement fix for dictionary writer!!!
            writer.Write(instance.Count);
            foreach(var pair in instance)
            {
                XnaUtility.WriteObject(pair.Key, writer, logger);
                XnaUtility.WriteObject(pair.Value, writer, logger);
            }
        }
    }
}
