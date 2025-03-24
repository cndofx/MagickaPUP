using System;
using System.Collections.Generic;
using System.Xml.Linq;

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

        public int GetReaderIndex(ContentTypeReader reader)
        {
            #region Comment

            // The logic of this function is as follows:
            // We look for the content type reader that has exactly the information we're looking for, aka, both the name and version match with our query.
            // If we cannot find it, we search for the next best candidate, which is the first reader whose reader name matches with our query.
            // This is done because it's pretty clear that XNA programmers mostly preferred to encode the version in the reader string itself rather than
            // the fucking dedicated version number field... which leads to most readers having a version field with value 0, making it completely meaningless.
            // We could just ignore it like we used to, but there comes a time where potentially the reader version COULD come into play at some point in the future.
            // Who knows what kind of content type readers we could find??? That's why this shit is here... it slows down lookup a bit, but that's ok, for the sake
            // of making sure that future code works just fine...

            // NOTE : This could potentially be fixed by doing the correct thing, which is adding another entry to the readers manager with the same string, but
            // the corresponding version value, which would make it a completely different key and prevent the rest of the code from breaking. That would make this
            // lookup completely pointless and actually wrong, since it could offer a reader that may or may not be valid for the specified version of the content.
            // For now, this hack stays... we'll see in the future what happens... I'm sure it will actually come bite me in the ass in the future. I can feel it.

            // Note that the old implementation used to be as simple as:
            // for (int i = 0; i < this.ContentTypeReaders.Count; ++i)
            // if (name == ContentTypeReaders[i].Name && version == ContentTypeReaders[i].Version)
            //     return i;
            // return -1;

            #endregion

            int bestCandidate = -1;

            for (int i = 0; i < this.ContentTypeReaders.Count; ++i)
            {
                if (reader.Name == ContentTypeReaders[i].Name)
                {
                    if (reader.Version == this.ContentTypeReaders[i].Version)
                    {
                        bestCandidate = i;
                        break;
                    }
                    else
                    {
                        bestCandidate = i; // We simply set this to be the best candidate out of them all, but we keep searching in case we find a better one.
                    }
                }
            }

            return bestCandidate;
        }

        public int GetReaderIndex(string name)
        {
            return GetReaderIndex(new ContentTypeReader(name));
        }

        public int GetReaderIndex(string name, int version)
        {
            return GetReaderIndex(new ContentTypeReader(name, version));
        }

        #endregion

        #region PublicMethods - GetReader

        public ContentTypeReader GetReader(int index)
        {
            return this.ContentTypeReaders[index];
        }

        #endregion

        #region PublicMethods - ContainsReader

        public bool ContainsReader(ContentTypeReader reader)
        {
            return GetReaderIndex(reader) >= 0;
        }

        public bool ContainsReader(string name)
        {
            return ContainsReader(new ContentTypeReader(name));
        }

        public bool ContainsReader(string name, int version)
        {
            return ContainsReader(new ContentTypeReader(name, version));
        }

        #endregion
    }
}
