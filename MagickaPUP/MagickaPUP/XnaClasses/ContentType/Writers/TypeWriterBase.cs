using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.ContentType.Writers
{
    public abstract class TypeWriterBase
    {
        public TypeWriterBase()
        { }

        public abstract void Write(object instance, MBinaryWriter writer, DebugLogger logger = null);
    }
}
