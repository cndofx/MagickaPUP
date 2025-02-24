using MagickaPUP.Utility.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Utility.Compression
{
    // TODO : Implement
    public class LzxDecoder
    {
        public uint[] positionBase = null;
        public byte[] extraBits = null;

        public LzxDecoder(int windowSize)
        {
            uint windowSizeShifted = (uint)(1 << windowSize);
            int posSlots;

            if (windowSize < 15 || windowSize > 21)
                throw new LzxException($"Unsupported Window Size! Window Size is {windowSize}, but must be in range [15, 21]");


        }
    }
}
