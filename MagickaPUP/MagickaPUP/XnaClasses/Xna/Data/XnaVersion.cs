using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Xna.Data
{
    public static class XnaVersion
    {
        public enum XnaVersionByte : byte
        {
            Version_3_1 = 4,
            Version_4 = 5
        }

        public static string XnaVersionString(XnaVersionByte version)
        {
            switch (version)
            {
                default:
                    return "XNA Version Unkown";
                case XnaVersionByte.Version_3_1:
                    return "XNA Version 3.1";
                case XnaVersionByte.Version_4:
                    return "XNA Version 4.0";
            }
        }
    }
}
