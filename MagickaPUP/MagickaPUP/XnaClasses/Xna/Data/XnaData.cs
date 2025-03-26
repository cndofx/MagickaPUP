using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Xna.Data
{
    public static class XnaData
    {
        public enum XnaVersion : byte
        {
            Version_3_1 = 4,
            Version_4 = 5
        }

        public static string XnaVersionString(XnaVersion version)
        {
            switch (version)
            {
                default:
                    return "XNA Version Unkown";
                case XnaVersion.Version_3_1:
                    return "XNA Version 3.1";
                case XnaVersion.Version_4:
                    return "XNA Version 4.0";
            }
        }

        public static byte XnaVersionByte(XnaVersion version)
        {
            return (byte)(version);
        }
    }
}
