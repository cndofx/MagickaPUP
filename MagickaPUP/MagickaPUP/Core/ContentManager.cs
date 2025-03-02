using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Core
{
    public class ContentManager
    {
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

        public void PackContent(PackSettings settings)
        { }

        public void UnpackContent(UnpackSettings settings)
        { }

        #region PrivateMethods - Packer (JSON to XNB)

        #endregion

        #region PrivateMethods - Unpacker (XNB to JSON)

        #endregion
    }
}
