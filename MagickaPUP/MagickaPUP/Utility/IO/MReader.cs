using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.XnaClasses;
using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;
using MagickaPUP.XnaClasses.Readers;

namespace MagickaPUP.Utility.IO
{
    public class MBinaryReader : BinaryReader/*, IMReader*/
    {
        #region Variables

        public ContentTypeReaderManager ContentTypeReaderManager { get; private set; }
        public ContentTypeReaderStorage ContentTypeReaderStorage { get; private set; }

        #endregion

        #region Constructor

        public MBinaryReader(Stream stream) : base(stream)
        {
            this.ContentTypeReaderStorage = new ContentTypeReaderStorage();
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
