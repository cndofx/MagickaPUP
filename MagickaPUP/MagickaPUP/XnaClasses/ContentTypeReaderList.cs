using System;
using System.Collections.Generic;

namespace MagickaPUP.XnaClasses
{
    public class ContentTypeReaderList
    {
        public List<ContentTypeReader> ContentTypeReaders { get; private set; }
        public int Count { get { return this.ContentTypeReaders.Count; } }

        public ContentTypeReaderList()
        {
            this.ContentTypeReaders = new List<ContentTypeReader>();
        }

        public void AddReader(ContentTypeReader reader)
        {
            this.ContentTypeReaders.Add(reader);
        }

        public void AddReaders(ContentTypeReader[] readers)
        {
            foreach (var reader in readers)
                this.ContentTypeReaders.Add(reader);
        }

        public void AddReaders(List<ContentTypeReader> readers)
        {
            foreach (var reader in readers)
                this.ContentTypeReaders.Add(reader);
        }

        public int GetReaderIndex(string name)
        {
            for (int i = 0; i < this.ContentTypeReaders.Count; ++i)
                if (name == ContentTypeReaders[i].Name)
                    return i;
            return -1;
        }
        
    }
}
