using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses.ContentType.Readers;
using MagickaPUP.XnaClasses.ContentType.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class ListWriter<T, TWriter> : TypeWriter<List<T>> where T : class where TWriter : TypeWriterBase, new()
    {
        public ListWriter()
        { }

        public override void Write(List<T> instance, MBinaryWriter writer, DebugLogger logger = null)
        {
            TypeWriterBase elementWriter = new TWriter();
            for (int i = 0; i < instance.Count; ++i)
            {
                elementWriter.Write(instance[i], writer, logger);
            }
        }
    }
}
