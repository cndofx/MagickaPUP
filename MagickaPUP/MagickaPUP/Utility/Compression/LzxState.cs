using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Utility.Compression
{
    public struct LzxState
    {
        public uint R0, R1, R2;                        /* for the LRU offset system */
        public ushort main_elements;                   /* number of main tree elements */
        public int header_read;                        /* have we started decoding at all yet? */
        public LzxConstants.BLOCKTYPE block_type;      /* type of this block */
        public uint block_length;                      /* uncompressed length of this block */
        public uint block_remaining;                   /* uncompressed bytes still left to decode */
        public uint frames_read;                       /* the number of CFDATA blocks processed */
        public int intel_filesize;                     /* magic header value used for transform */
        public int intel_curpos;                       /* current offset in transform space */
        public int intel_started;                      /* have we seen any translateable data yet? */

        public ushort[] PRETREE_table;
        public byte[] PRETREE_len;
        public ushort[] MAINTREE_table;
        public byte[] MAINTREE_len;
        public ushort[] LENGTH_table;
        public byte[] LENGTH_len;
        public ushort[] ALIGNED_table;
        public byte[] ALIGNED_len;

        // NEEDED MEMBERS
        // CAB actualsize
        // CAB window
        // CAB window_size
        // CAB window_posn
        public uint actual_size;
        public byte[] window;
        public uint window_size;
        public uint window_posn;
    }
}
