using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses
{
    public class VertexDeclarationEntry : XnaObject
    {
        #region Variables

        public short stream { get; set; }
        public short offset { get; set; }
        public byte elementFormat { get; set; }
        public byte elementMethod { get; set; }
        public byte elementUsage { get; set; }
        public byte usageIndex { get; set; }

        #endregion

        #region Constructors

        public VertexDeclarationEntry()
        {
            this.stream = 0;
            this.offset = 0;
            this.elementFormat = 0;
            this.elementMethod = 0;
            this.elementUsage = 0;
            this.usageIndex = 0;
        }

        public VertexDeclarationEntry(short stream = 0, short offset = 0, byte format = 0, byte method = 0, byte usage = 0, byte index = 0)
        {
            this.stream = stream;
            this.offset = offset;
            this.elementFormat = format;
            this.elementMethod = method;
            this.elementUsage = usage;
            this.usageIndex = index;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            this.stream = reader.ReadInt16();
            this.offset = reader.ReadInt16();
            this.elementFormat = reader.ReadByte();
            this.elementMethod = reader.ReadByte();
            this.elementUsage = reader.ReadByte();
            this.usageIndex = reader.ReadByte();

            logger?.Log(2, $"  - Entry : {{ stream = {this.stream}, offset = {this.offset}, elementFormat = {this.elementFormat}, elementMethod = {this.elementMethod}, elementUsage = {this.elementUsage}, usageIndex = {this.usageIndex} }}");
        }

        public static VertexDeclarationEntry Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new VertexDeclarationEntry();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing VertexDeclarationEntry...");

            writer.Write(this.stream);
            writer.Write(this.offset);
            writer.Write(this.elementFormat);
            writer.Write(this.elementMethod);
            writer.Write(this.elementUsage);
            writer.Write(this.usageIndex);

            logger?.Log(2, $"  - Entry : {{ stream = {this.stream}, offset = {this.offset}, elementFormat = {this.elementFormat}, elementMethod = {this.elementMethod}, elementUsage = {this.elementUsage}, usageIndex = {this.usageIndex} }}");
        }

        #endregion
    }

    public class VertexDeclaration : XnaObject
    {
        #region Variables

        public int numEntries { get; set; }
        public List<VertexDeclarationEntry> entries { get; set; }

        #endregion

        #region Constructor

        public VertexDeclaration()
        {
            this.numEntries = 0;
            this.entries = new List<VertexDeclarationEntry>();
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading VertexDeclaration...");

            this.numEntries = reader.ReadInt32();

            logger?.Log(2, $" - Num Entries : {this.numEntries}");

            for (int i = 0; i < this.numEntries; ++i)
            {
                VertexDeclarationEntry entry = VertexDeclarationEntry.Read(reader, logger);
                this.entries.Add(entry);
            }
        }

        public static VertexDeclaration Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new VertexDeclaration();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing VertexDeclaration...");

            writer.Write(this.numEntries);

            for (int i = 0; i < this.numEntries; ++i)
            {
                this.entries[i].WriteInstance(writer, logger);
            }
        }

        public override ContentTypeReader GetObjectContentTypeReader()
        {
            return new ContentTypeReader("Microsoft.Xna.Framework.Content.VertexDeclarationReader", 0);
        }

        #endregion
    }
}
