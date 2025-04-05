using System.IO;
using MagickaPUP.Utility.IO.Data;
using MagickaPUP.XnaClasses;
using MagickaPUP.XnaClasses.Readers;

namespace MagickaPUP.Utility.IO
{
    public class MBinaryReader : BinaryReader/*, IMReader*/
    {
        #region Variables

        public ContentTypeReaderManager ContentTypeReaderManager { get; private set; }
        public ContentTypeReaderStorage ContentTypeReaderStorage { get; private set; }
        public GameVersion GameVersion { get; set; }

        #endregion

        #region Constructor

        public MBinaryReader(Stream stream) : base(stream)
        {
            this.ContentTypeReaderStorage = new ContentTypeReaderStorage();
            this.ContentTypeReaderManager = new ContentTypeReaderManager();
        }

        #endregion

        #region Public Methods

        public new int Read7BitEncodedInt()
        {
            return base.Read7BitEncodedInt();
        }

        #endregion

        #region PrivateMethods

        #endregion
    }

    public class MStreamReader : StreamReader
    {
        MStreamReader(Stream stream) : base(stream)
        { }
    }

}
