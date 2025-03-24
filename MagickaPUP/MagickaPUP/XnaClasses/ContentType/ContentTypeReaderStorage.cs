using System;
using System.Collections.Generic;

namespace MagickaPUP.XnaClasses
{
    // NOTE : Maybe it would be a better name to call this "ContentTypeReaderList" like we used to, but internally this could be anything other than a list in
    // future implementations, so I'm renaming it to storage.
    // It also makes it clearer what the difference is between the ContentTypeReaderManager and this class.
    // The ContentTypeReaderManager is in charge of managining lookups for all content type readers known to this program.
    // The ContentTypeReaderStorage is in charge of managining lookups for all content type readers known to a given specific XNB file.
    public class ContentTypeReaderStorage
    {
        public List<ContentTypeReader> ContentTypeReaders { get; private set; }
        public int Count { get { return this.ContentTypeReaders.Count; } }

        public ContentTypeReaderStorage()
        {
            this.ContentTypeReaders = new List<ContentTypeReader>();
        }

        public int AddReader(ContentTypeReader reader)
        {
            this.ContentTypeReaders.Add(reader);
            return this.ContentTypeReaders.Count - 1;
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

        public bool ContainsReader(string name) {
            return GetReaderIndex(name) >= 0;
        }
        
    }
}
