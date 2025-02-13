using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Xnb.Data
{
    [Flags]
    public enum XnbFlags : byte
    {
        None = 0,
        HiDefProfile = 0x01,
        Lz4Compressed = 0x40, // 64
        LzxCompressed = 0x80, // 128
        Compressed = LzxCompressed | Lz4Compressed
    }
}
