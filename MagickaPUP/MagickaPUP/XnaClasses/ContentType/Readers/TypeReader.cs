using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses.ContentType.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific
{
    public abstract class TypeReader<T> : TypeReaderBase where T : class
    {
        public TypeReader()
        { }

        public abstract T Read(T instance, MBinaryReader reader, DebugLogger logger = null);

        public override object Read(object instance, MBinaryReader reader, DebugLogger logger = null)
        {
            if (instance == null)
                instance = default(T);
            return Read((T)instance, reader, logger);
        }
    }
}
