using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Xna
{
    public static class XnaUtility
    {
        public static T ReadObject<T>(MBinaryReader reader, DebugLogger logger = null) where T : class
        {
            T ans = default(T);

            int indexXnb = reader.Read7BitEncodedInt();
            int indexMem = indexXnb - 1;

            if (indexXnb == 0)
            {
                logger?.Log(1, "Reading NULL Object");
                return null;
            }

            return ans;
        }
    }
}
