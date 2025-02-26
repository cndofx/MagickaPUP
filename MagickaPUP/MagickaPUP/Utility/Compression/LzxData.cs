using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Utility.Compression
{
    // NOTE : Constants and values translated from the XNA LzxDecoder implementation.
    public static class LzxConstants
    {
        #region Constants

        public static readonly ushort MIN_MATCH = 2;
        public static readonly ushort MAX_MATCH = 257;
        public static readonly ushort NUM_CHARS = 256;

        public static readonly ushort PRETREE_NUM_ELEMENTS = 20;
        public static readonly ushort ALIGNED_NUM_ELEMENTS = 8;
        public static readonly ushort NUM_PRIMARY_LENGTHS = 7;
        public static readonly ushort NUM_SECONDARY_LENGTHS = 249;

        public static readonly ushort PRETREE_MAXSYMBOLS = (ushort)(LzxConstants.PRETREE_NUM_ELEMENTS);
        public static readonly ushort PRETREE_TABLEBITS = 6;
        public static readonly ushort MAINTREE_MAXSYMBOLS = (ushort)(LzxConstants.NUM_CHARS + (50 * 8));
        public static readonly ushort MAINTREE_TABLEBITS = 12;
        public static readonly ushort LENGTH_MAXSYMBOLS = (ushort)(LzxConstants.NUM_SECONDARY_LENGTHS + 1);
        public static readonly ushort LENGTH_TABLEBITS = 12;
        public static readonly ushort ALIGNED_MAXSYMBOLS = (ushort)(LzxConstants.ALIGNED_NUM_ELEMENTS);
        public static readonly ushort ALIGNED_TABLEBITS = 7;

        public static readonly ushort LENTABLE_SAFETY = 64;

        #endregion

        #region Enums

        public enum BLOCKTYPE
        {
            INVALID = 0,
            VERBATIM = 1,
            ALIGNED = 2,
            UNCOMPRESSED = 3,
        }

        #endregion
    }
}
