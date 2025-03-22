using MagickaPUP.XnaClasses;
using MagickaPUP.XnaClasses.Readers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Utility.IO
{
    public class MBinaryWriter : BinaryWriter/*, IMWriter*/
    {
        #region Variables

        public ContentTypeReaderManager ContentTypeReaderManager { get; set; }
        public ContentTypeReaderStorageList ContentTypeReaders { get; private set; }

        #endregion

        #region Constructor

        public MBinaryWriter(Stream stream) : base(stream)
        {
            this.ContentTypeReaders = new ContentTypeReaderStorageList();
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
