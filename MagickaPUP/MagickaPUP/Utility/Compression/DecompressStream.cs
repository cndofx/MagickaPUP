using System;
using System.IO;
using System.Collections.Generic;

namespace MagickaPUP.Utility.Compression
{
    public class DecompressStream : Stream
    {
        private Stream baseStream;
        private int compressedTodo;
        private int decompressedTodo;
        private byte[] compressedBuffer;
        private byte[] decompressedBuffer;

        public DecompressStream(Stream baseStream, int compressedTodo, int decompressedTodo)
        {

        }

        public override bool CanRead => throw new NotImplementedException();

        public override bool CanSeek => throw new NotImplementedException();

        public override bool CanWrite => throw new NotImplementedException();

        public override long Length => throw new NotImplementedException();

        public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}
