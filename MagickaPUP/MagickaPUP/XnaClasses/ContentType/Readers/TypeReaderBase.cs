using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.ContentType.Readers
{
    public abstract class TypeReaderBase
    {
        public TypeReaderBase()
        { }

        public abstract object Read(object instance, MBinaryReader reader, DebugLogger logger = null);
    }
}
