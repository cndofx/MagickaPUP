using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Core.Args
{
    public static class ArgsUtility
    {
        public static string ParseString(string str)
        {
            return str; // LMAO
        }

        public static bool ParseBool(string str)
        {
            try
            {
                bool ans = bool.Parse(str);
                return ans;
            }
            catch
            {
                return false;
            }
        }

        public static int ParseInt(string str)
        {
            try
            {
                int ans = int.Parse(str);
                return ans;
            }
            catch
            {
                return 0;
            }
        }

        public static long ParseLong(string str)
        {
            try
            {
                long ans = long.Parse(str);
                return ans;
            }
            catch
            {
                return 0;
            }
        }

        public static float ParseFloat(string str)
        {
            try
            {
                float ans = float.Parse(str);
                return ans;
            }
            catch
            {
                return 0.0f;
            }
        }

        public static double ParseDouble(string str)
        {
            try
            {
                double ans = double.Parse(str);
                return ans;
            }
            catch
            {
                return 0.0d;
            }
        }

        public static byte ParseByte(string str)
        {
            try
            {
                byte ans = byte.Parse(str);
                return ans;
            }
            catch
            {
                return 0;
            }
        }

        public static char ParseChar(string str)
        {
            try
            {
                char ans = char.Parse(str);
                return ans;
            }
            catch
            {
                return '0';
            }
        }
    }
}
