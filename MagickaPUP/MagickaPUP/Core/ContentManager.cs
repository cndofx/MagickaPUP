using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Core
{
    public class ContentManager
    {
        #region Structs

        public struct PackSettings
        {
            public string InputFileName;
            public string OutputFileName;
            public bool CompressOutput;
            public int DebugLevel;
        }

        public struct UnpackSettings
        {
            public string InputFileName;
            public string OutputFileName;
            public int DebugLevel;
            public bool ShouldIndent;
        }

        #endregion

        #region PublicMethods

        public void PackContent(PackSettings settings)
        { }

        public void UnpackContent(UnpackSettings settings)
        { }

        #endregion

        #region PrivateMethods - Packer (JSON to XNB)

        #endregion

        #region PrivateMethods - Unpacker (XNB to JSON)

        #endregion
    }
}
