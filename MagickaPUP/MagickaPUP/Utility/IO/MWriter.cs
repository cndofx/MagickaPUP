using System.IO;
using MagickaPUP.Utility.IO.Data;
using MagickaPUP.XnaClasses;
using MagickaPUP.XnaClasses.Readers;

namespace MagickaPUP.Utility.IO
{
    public class MBinaryWriter : BinaryWriter/*, IMWriter*/
    {
        #region Variables

        public ContentTypeReaderManager ContentTypeReaderManager { get; private set; }
        public ContentTypeReaderStorage ContentTypeReaderStorage { get; private set; }
        public GameVersion GameVersion { get; set; }

        #endregion

        #region Constructor

        public MBinaryWriter(Stream stream) : base(stream)
        {
            this.ContentTypeReaderStorage = new ContentTypeReaderStorage();
            this.ContentTypeReaderManager = new ContentTypeReaderManager();
        }

        #endregion

        #region PublicMethods

        public new void Write7BitEncodedInt(int x)
        {
            base.Write7BitEncodedInt(x);
        }

        #endregion
    }

    public class MTextWriter : StreamWriter
    {
        public MTextWriter(Stream stream) : base(stream) { }

    }
}
