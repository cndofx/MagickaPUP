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
        public static uint[] position_base = null;
        public static byte[] extra_bits = null;

        private LzxState m_state;
        private int window;

        public LzxDecoder(int window = 16)
        {
            // Assign window value to local LZX decoder
            this.window = window;

            // Handle invalid window sizes
            if (window < 15 || window > 21)
                throw new LzxException($"Unsupported Window Size! Window Size is {window}, but must be in range [15, 21]");

            // Initialize LZX State
            Lzx_InitializeState();

            // Initialize LZX Static Tables if they have not been initialized yet
            Lzx_InitializeStaticTables();

            // Calculate required position slots
            int posn_slots;
            if (window == 20) posn_slots = 42;
            else if (window == 21) posn_slots = 50;
            else posn_slots = window << 1;

            // Modify LZX State according to number of required position slots
            m_state.R0 = m_state.R1 = m_state.R2 = 1;
            m_state.main_elements = (ushort)(LzxConstants.NUM_CHARS + (posn_slots << 3));
            m_state.header_read = 0;
            m_state.frames_read = 0;
            m_state.block_remaining = 0;
            m_state.block_type = LzxConstants.BLOCKTYPE.INVALID;
            m_state.intel_curpos = 0;
            m_state.intel_started = 0;

            // Initialize LZX State Arrays
            m_state.PRETREE_table = new ushort[(1 << LzxConstants.PRETREE_TABLEBITS) + (LzxConstants.PRETREE_MAXSYMBOLS << 1)];
            m_state.PRETREE_len = new byte[LzxConstants.PRETREE_MAXSYMBOLS + LzxConstants.LENTABLE_SAFETY];
            m_state.MAINTREE_table = new ushort[(1 << LzxConstants.MAINTREE_TABLEBITS) + (LzxConstants.MAINTREE_MAXSYMBOLS << 1)];
            m_state.MAINTREE_len = new byte[LzxConstants.MAINTREE_MAXSYMBOLS + LzxConstants.LENTABLE_SAFETY];
            m_state.LENGTH_table = new ushort[(1 << LzxConstants.LENGTH_TABLEBITS) + (LzxConstants.LENGTH_MAXSYMBOLS << 1)];
            m_state.LENGTH_len = new byte[LzxConstants.LENGTH_MAXSYMBOLS + LzxConstants.LENTABLE_SAFETY];
            m_state.ALIGNED_table = new ushort[(1 << LzxConstants.ALIGNED_TABLEBITS) + (LzxConstants.ALIGNED_MAXSYMBOLS << 1)];
            m_state.ALIGNED_len = new byte[LzxConstants.ALIGNED_MAXSYMBOLS + LzxConstants.LENTABLE_SAFETY];

            // Initialize tables to 0. Deltas will be applied later.
            for (int i = 0; i < LzxConstants.MAINTREE_MAXSYMBOLS; i++) m_state.MAINTREE_len[i] = 0;
            for (int i = 0; i < LzxConstants.LENGTH_MAXSYMBOLS; i++) m_state.LENGTH_len[i] = 0;
        }

        private void Lzx_InitializeState()
        {
            uint wndsize = (uint)(1 << this.window);
            this.m_state = new LzxState();
            this.m_state.actual_size = 0;
            this.m_state.window = new byte[wndsize];
            for (int i = 0; i < wndsize; ++i)
                this.m_state.window[i] = 0xDC;
            this.m_state.actual_size = wndsize;
            this.m_state.window_size = wndsize;
            this.m_state.window_posn = 0;
        }

        private void Lzx_InitializeStaticTables()
        {
            // If any of the static tables has not been initialized yet, we inititalize it now.
            // This call should always be performed on LzxDecoder() construction, so that means that this acts sort of like a singleton-like initialization of sorts.
            
            // NOTE : In the future, this could be moved to some sort of initialization handler so that we don't have to check for null every single time the constructor
            // is called, but that does not matter as much tbh. It would make more sense to allow the tables to be compiletime constants, but in C# we don't have constexpr,
            // so yeah, either code gen or suck it up...

            // Initialize the extra_bits table
            if (extra_bits == null)
            {
                extra_bits = new byte[52];
                for (int i = 0, j = 0; i <= 50; i += 2)
                {
                    extra_bits[i] = extra_bits[i + 1] = (byte)j;
                    if ((i != 0) && (j < 17)) j++;
                }
            }

            // Initialize position_base table
            if (position_base == null)
            {
                position_base = new uint[51];
                for (int i = 0, j = 0; i <= 50; i++)
                {
                    position_base[i] = (uint)j;
                    j += 1 << extra_bits[i];
                }
            }
        }
    }
}
