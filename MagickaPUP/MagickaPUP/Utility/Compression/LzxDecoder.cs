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
        private uint[] position_base = null;
        private byte[] extra_bits = null;

        private LzxState m_state;

        public LzxDecoder(int window = 16)
        {
            uint wndsize = (uint)(1 << window);
            int posn_slots;

            if (window < 15 || window > 21)
                throw new LzxException($"Unsupported Window Size! Window Size is {window}, but must be in range [15, 21]");


        }
    }
}
