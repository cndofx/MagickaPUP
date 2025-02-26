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

            // Initialize LZX Static Tables if they have not been initialized yet
            InitializeStaticTables();
        }

        private void InitializeStaticTables()
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
