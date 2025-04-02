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
            // XNA requires that the reader for base type T is also present, so we must ensure that the reader is added, even if we don't read individual objects.
            // This is due to the fact that even structs use content type readers, so this behaviour will need to be streamlined in the future by improving the way
            // that list reading and writing works on mpup.
            var contentTypeReader = writer.ContentTypeReaderManager.GetContentTypeReader(typeof(T));
            writer.ContentTypeReaderStorage.AddReader(contentTypeReader);

            // WRITE THE FUCKING COUNT!!!
            writer.Write((int)instance.Count);

            // Write the data of this type
            TypeWriterBase elementWriter = new TWriter();
            for (int i = 0; i < instance.Count; ++i)
            {
                elementWriter.Write(instance[i], writer, logger);
            }
        }
    }
}
