using MagickaPUP.Utility.IO;
using MagickaPUP.MagickaClasses.Liquids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses
{
    public enum IndexElementSize
    {
        Bits16 = 0,
        Bits32
    }

    public class IndexBuffer : XnaObject
    {
        #region Variables

        public IndexElementSize indexSize { get; set; }
        /* [JsonIgnore] */public uint[] data { get; set; }
        public int numBytes { get; set; }

        #endregion

        #region Properties
        /*
        [JsonPropertyName("data")]
        public int[] BufferInt
        {
            get { return Array.ConvertAll(this.data, b => (int)b); }
            set { this.data = Array.ConvertAll(value, i => (byte)i); }
        }
        */

        #endregion

        #region Constructor

        public IndexBuffer()
        {
            this.indexSize = IndexElementSize.Bits16;
            this.data = new uint[0];
            this.numBytes = 0;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading IndexBuffer...");

            bool flag = reader.ReadBoolean(); // if true -> IndexElementSize is 16 bits. If false -> IndexElementSize is 32 bits.
            this.indexSize = (flag ? IndexElementSize.Bits16 : IndexElementSize.Bits32);

            this.numBytes = reader.ReadInt32();

            int length = this.numBytes / this.GetBytesPerEntry();

            this.data = new uint[length];

            if (flag)
            {
                for (int i = 0; i < length; ++i)
                    this.data[i] = reader.ReadUInt16();
            }
            else
            {
                for (int i = 0; i < length; ++i)
                    this.data[i] = reader.ReadUInt32();
            }   

            // TODO : Same as in the vertex buffer... either find a reasonable way to print this buffer, or just don't do it because it's fucking massive.

            logger?.Log(2, $" - Bytes : {this.numBytes}");
            logger?.Log(2, $" - Index Element Size : {(flag ? 16 : 32)} bits.");
            logger?.Log(2, $" - Elements Count : {length}");
        }

        public static IndexBuffer Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new IndexBuffer();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing IndexBuffer...");

            bool flag = this.indexSize == IndexElementSize.Bits16 ? true : false; // true -> 16 bits. false -> 32 bits.
            writer.Write(flag);

            writer.Write(this.numBytes);

            int length = this.numBytes / this.GetBytesPerEntry();

            if (flag)
            {
                for (int i = 0; i < length; ++i)
                    writer.Write((ushort)this.data[i]);
            }
            else
            {
                for (int i = 0; i < length; ++i)
                    writer.Write((uint)this.data[i]);
            }
            
            
            // writer.Write(this.data);
        }

        public override ContentTypeReaderStorage GetObjectContentTypeReader()
        {
            return new ContentTypeReaderStorage("Microsoft.Xna.Framework.Content.IndexBufferReader", 0);
        }

        #endregion

        #region PrivateMethods

        private int GetBytesPerEntry()
        {
            return this.indexSize == IndexElementSize.Bits16 ? 2 : 4;
        }

        #endregion
    }
}
