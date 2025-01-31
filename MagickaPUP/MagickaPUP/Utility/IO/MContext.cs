using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Utility.IO
{
    public class MContext
    {
        #region Variables

        private MBinaryReader reader;
        private MBinaryWriter writer;
        private DebugLogger logger;

        #endregion

        #region Properties

        public MBinaryReader Reader { get { return reader; } }
        public MBinaryWriter Writer { get { return writer; } }
        public DebugLogger Logger { get { return logger; } }

        #endregion

        #region Constructor

        public MContext()
        {}

        #endregion
    }
}
