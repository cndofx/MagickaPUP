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
            // Handle invalid window sizes
            if (window < 15 || window > 21)
                throw new LzxException($"Unsupported Window Size! Window Size is {window}, but must be in range [15, 21]");

            // Window Size management
            uint wndsize = (uint)(1 << window);
            int posn_slots;

            // Initialize LZX state
            m_state = new LzxState();
            m_state.actual_size = 0;
            m_state.window = new byte[wndsize];
            for (int i = 0; i < wndsize; i++) m_state.window[i] = 0xDC;
            m_state.actual_size = wndsize;
            m_state.window_size = wndsize;
            m_state.window_posn = 0;

            
        }
    }
}
