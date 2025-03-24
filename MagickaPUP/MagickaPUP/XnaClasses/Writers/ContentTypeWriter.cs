using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Writers
{
    public class ContentWriter<T> where T : XnaObject, new()
    {
        public ContentWriter()
        { }

        public string Name { get; set; }
        public int Version { get; set; }

        public virtual void Write(T t, MBinaryWriter writer, DebugLogger logger = null)
        {
            t.WriteInstance(writer, logger);
        }
    }
}
