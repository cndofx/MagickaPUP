using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Core
{
    public static class ContentManager
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

        public static void PackContent(PackSettings settings)
        { }

        public static void UnpackContent(UnpackSettings settings)
        { }
    }
}
