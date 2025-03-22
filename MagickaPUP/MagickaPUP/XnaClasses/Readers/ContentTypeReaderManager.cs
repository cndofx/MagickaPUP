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

        public object Get(string name, int version)
        {
            return Get(new ContentTypeReader(name, version));
        }

        public object Get(ContentTypeReader contentType)
        {
            if (this.contentTypeReaders.ContainsKey(contentType))
                return this.contentTypeReaders[contentType];
            return null;
        }

        public void Add(ContentTypeReader contentTypeReader, object content)
        {
            if (!this.contentTypeReaders.ContainsKey(contentTypeReader))
                this.contentTypeReaders.Add(contentTypeReader, content);
        }
    }
}
