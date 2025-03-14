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
            bool ans;
            try
            {
                ans = bool.Parse(str);
            }
            catch
            {
                try
                {
                    ans = long.Parse(str) > 0;
                }
                catch
                {
                    try
                    {
                        ans = double.Parse(str) > 0.0d;
                    }
                    catch
                    {
                        ans = false;
                    }
                }
            }
            return ans;
        }

        public static int ParseInt(string str)
        {
            return (int)ParseLong(str);
        }

        public static long ParseLong(string str)
        {
            long ans;
            try
            {
                ans = long.Parse(str);
            }
            catch
            {
                try
                {
                    ans = (long)double.Parse(str);
                }
                catch
                {
                    try
                    {
                        ans = bool.Parse(str) ? 1 : 0;
                    }
                    catch
                    {
                        ans = 0;
                    }
                }
            }
            return ans;
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
