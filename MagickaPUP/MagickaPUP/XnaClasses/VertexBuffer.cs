using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.Remoting.Messaging;

namespace MagickaPUP.XnaClasses
{
    public class VertexBuffer : XnaObject
    {
        #region Variables

        [JsonIgnore] private int numBytes;
        [JsonIgnore] private byte[] buffer;

        #endregion

        #region Properties

        public int NumBytes { get { return numBytes; } set { numBytes = value; } }
        
        [JsonIgnore]
        public byte[] Buffer { get { return buffer; } }

        [JsonPropertyName("Buffer")]
        public int[] BufferInt
        {
            get { return Array.ConvertAll(this.buffer, b => (int)b); }
            set { this.buffer = Array.ConvertAll(value, i => (byte)i); }
        }

        #endregion

        #region Constructor

        public VertexBuffer()
        {
            this.numBytes = 0;
            this.buffer = new byte[this.numBytes];
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading VertexBuffer...");

            this.numBytes = reader.ReadInt32();

            logger?.Log(2, $" - Bytes : {this.numBytes}");

            this.buffer = reader.ReadBytes(this.numBytes);

            // TODO : Find the most reasonable way to log all of this data to the terminal... if you need to, because these buffers are fucking massive and it would be impossible to read.
        }

        public static VertexBuffer Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new VertexBuffer();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing VertexBuffer...");

            writer.Write(this.numBytes);
            writer.Write(this.buffer);
        }

        public override ContentTypeReader GetObjectContentTypeReader()
        {
            return new ContentTypeReader("Microsoft.Xna.Framework.Content.VertexBufferReader", 0);
        }

        #endregion
    }
}
