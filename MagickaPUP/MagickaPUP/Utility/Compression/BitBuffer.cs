using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Utility.Compression
{
    // TODO : Maybe get rid of this class cause this is from the monogame version, I'm going to start working off of the XNA 3.1 version again just in case there's any big differences that break stuff
    public class BitBuffer
    {
        uint buffer;
        byte bitsleft;
        Stream byteStream;

        public BitBuffer(Stream stream)
        {
            byteStream = stream;
            InitBitStream();
        }

        public void InitBitStream()
        {
            buffer = 0;
            bitsleft = 0;
        }

        public void EnsureBits(byte bits)
        {
            while (bitsleft < bits)
            {
                int lo = (byte)byteStream.ReadByte();
                int hi = (byte)byteStream.ReadByte();
                //int amount2shift = sizeof(uint)*8 - 16 - bitsleft;
                buffer |= (uint)(((hi << 8) | lo) << (sizeof(uint) * 8 - 16 - bitsleft));
                bitsleft += 16;
            }
        }

        public uint PeekBits(byte bits)
        {
            return (buffer >> ((sizeof(uint) * 8) - bits));
        }

        public void RemoveBits(byte bits)
        {
            buffer <<= bits;
            bitsleft -= bits;
        }

        public uint ReadBits(byte bits)
        {
            uint ret = 0;

            if (bits > 0)
            {
                EnsureBits(bits);
                ret = PeekBits(bits);
                RemoveBits(bits);
            }

            return ret;
        }

        public uint GetBuffer()
        {
            return buffer;
        }

        public byte GetBitsLeft()
        {
            return bitsleft;
        }
    }
}
