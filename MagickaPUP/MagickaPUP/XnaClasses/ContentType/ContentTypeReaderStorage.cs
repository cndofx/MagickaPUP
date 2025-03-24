using System;
using System.Collections.Generic;

namespace MagickaPUP.XnaClasses
{
    // NOTE : Maybe it would be a better name to call this "ContentTypeReaderList" like we used to, but internally this could be anything other than a list in
    // future implementations, so I'm renaming it to storage.
    // It also makes it clearer what the difference is between the ContentTypeReaderManager and this class.
    // The ContentTypeReaderManager is in charge of managining lookups for all content type readers known to this program.
    // The ContentTypeReaderStorage is in charge of managining lookups for all content type readers known to a given specific XNB file.
    // NOTE : This class is useful for 2 main purposes:
    // 1) Detecting malformed XNB files on XNB decompilation.
    // When reading an input XNB file, if we find a request to read a content type reader that is not found on the file's header, then we can error out.
    // Note that we could become more permissive with a flag in the future so as to allow XNB recompilation for fixup, altough this usecase is kind of rare in the
    // context of Magicka modding.
    // 2) Storing the content type readers required as we go writing the data of the ouput XNB file on XNB compilation.
    public class ContentTypeReaderStorage
    {
        public List<ContentTypeReader> ContentTypeReaders { get; private set; }
        public int Count { get { return this.ContentTypeReaders.Count; } }

        public ContentTypeReaderStorage()
        {
            this.ContentTypeReaders = new List<ContentTypeReader>();
        }

        #region PublicMethods - AddReaders

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

        #endregion

        #region PublicMethods - GetReaderIndex

        public int GetReaderIndex(string name)
        {
            for (int i = 0; i < this.ContentTypeReaders.Count; ++i)
                if (name == ContentTypeReaders[i].Name)
                    return i;
            return -1;
        }

        #endregion

        #region PublicMethods - ContainsReader

        public bool ContainsReader(string name) {
            return GetReaderIndex(name) >= 0;
        }

        #endregion
    }
}
