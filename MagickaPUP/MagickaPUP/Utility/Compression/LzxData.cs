using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Utility.Compression
{
    // NOTE : Constants and values translated from the XNA LzxDecoder implementation.
    public static class LzxData
    {
        #region Constants

        public static readonly ushort MIN_MATCH = 2;
        public static readonly ushort MAX_MATCH = 257;
        public static readonly ushort NUM_CHARS = 256;

        public static readonly ushort PRETREE_NUM_ELEMENTS = 20;
        public static readonly ushort ALIGNED_NUM_ELEMENTS = 8;
        public static readonly ushort NUM_PRIMARY_LENGTHS = 7;
        public static readonly ushort NUM_SECONDARY_LENGTHS = 249;

        public static readonly ushort PRETREE_MAXSYMBOLS = (ushort)(LzxData.PRETREE_NUM_ELEMENTS);
        public static readonly ushort PRETREE_TABLEBITS = 6;
        public static readonly ushort MAINTREE_MAXSYMBOLS = (ushort)(LzxData.NUM_CHARS + (50 * 8));
        public static readonly ushort MAINTREE_TABLEBITS = 12;
        public static readonly ushort LENGTH_MAXSYMBOLS = (ushort)(LzxData.NUM_SECONDARY_LENGTHS + 1);
        public static readonly ushort LENGTH_TABLEBITS = 12;
        public static readonly ushort ALIGNED_MAXSYMBOLS = (ushort)(LzxData.ALIGNED_NUM_ELEMENTS);
        public static readonly ushort ALIGNED_TABLEBITS = 7;

        public static readonly ushort LENTABLE_SAFETY = 64;

        #endregion

        #region Enums

        public enum BlockType
        {
            Invalid = 0,
            Verbatim = 1,
            Aligned = 2,
            Uncompressed = 3,
        }

        #endregion
    }
}
