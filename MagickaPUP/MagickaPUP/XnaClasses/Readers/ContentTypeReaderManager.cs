using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Readers
{
    public class ContentTypeReaderManager
    {
        public Dictionary<ContentTypeReader, object> contentTypeReaders = new() {
            { new ContentTypeReader(), new XnaObject() },
        };


    }
}
