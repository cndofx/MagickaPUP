using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.ContentType.Writers
{
    public abstract class TypeWriter<T> : TypeWriterBase where T : class
    {
        public TypeWriter()
        { }
        
        public abstract void Write(T instance, MBinaryWriter writer, DebugLogger logger = null);

        public override void Write(object instance, MBinaryWriter writer, DebugLogger logger = null)
        {
            Write((T)instance, writer, logger);
        }
    }
}
