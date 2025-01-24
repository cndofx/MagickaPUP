using MagickaPUP.MagickaClasses.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Generic
{
    public static class MagickaDefines
    {
        #region Constants

        public static readonly double ONE_OVER_LN2_F64 = 1.0d / Math.Log(2.0d);
        public static readonly float ONE_OVER_LN2_F32 = 1.0f / (float)Math.Log(2.0f);

        #endregion

        #region PublicMethods

        // This is some wonky af shit!
        public static int ElementIndex(Elements iElement)
        {
            if (iElement == Elements.All)
            {
                return 11;
            }
            return (int)(Math.Log((double)iElement) * MagickaDefines.ONE_OVER_LN2_F64 + 0.5d);
        }

        // WTF... all of this looks like one giant fucking hack. Not my fault tho.
        public static Elements ElementFromIndex(int iIndex)
        {
            if (iIndex == 11)
            {
                return Elements.All;
            }
            return (Elements)(Math.Pow(2.0d, (double)iIndex) + 0.5d);
        }

        #endregion
    }
}
