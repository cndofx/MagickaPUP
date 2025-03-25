using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses;
using MagickaPUP.XnaClasses.Xna;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Liquids
{
    public class Water : Liquid
    {
        #region Variables

        public VertexBuffer vertices { get; set; }
        public IndexBuffer indices { get; set; }
        public VertexDeclaration declaration { get; set; }

        public int vertexStride { get; set; }
        public int numVertices { get; set; }
        public int primitiveCount { get; set; }

        public bool flag { get; set; } // this flag determines whether entities can drown in the water or not.

        public bool freezable { get; set; }
        public bool autofreeze { get; set; }

        #endregion

        #region Constructor

        public Water()
        {
            this.vertices = new VertexBuffer();
            this.indices = new IndexBuffer();
            this.declaration = new VertexDeclaration();

            this.vertexStride = 0;
            this.numVertices = 0;
            this.primitiveCount = 0;

            this.flag = false;

            this.freezable = false;
            this.autofreeze = false;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Water...");

            this.vertices = XnaUtility.ReadObject<VertexBuffer>(reader, logger);
            this.indices = XnaUtility.ReadObject<IndexBuffer>(reader, logger);
            this.declaration = XnaUtility.ReadObject<VertexDeclaration>(reader, logger);

            this.vertexStride = reader.ReadInt32();
            this.numVertices = reader.ReadInt32();
            this.primitiveCount = reader.ReadInt32();

            this.flag = reader.ReadBoolean();

            this.freezable = reader.ReadBoolean();
            this.autofreeze = reader.ReadBoolean();

            logger?.Log(2, $" - Drowns Entities : {this.flag}");
            logger?.Log(2, $" - Is Freezable    : {this.freezable}");
            logger?.Log(2, $" - Auto Freeze     : {this.autofreeze}");
        }

        public static new Water Read(MBinaryReader reader, DebugLogger logger = null)
        {
            Water ans = new Water();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            // First, write the base liquid class, so that the effect is written.
            base.WriteInstance(writer, logger);

            logger?.Log(1, "Writing Water...");

            XnaUtility.WriteObject(this.vertices, writer, logger);
            XnaUtility.WriteObject(this.indices, writer, logger);
            XnaUtility.WriteObject(this.declaration, writer, logger);

            writer.Write(this.vertexStride);
            writer.Write(this.numVertices);
            writer.Write(this.primitiveCount);
            writer.Write(this.flag);
            writer.Write(this.freezable);
            writer.Write(this.autofreeze);
        }

        #endregion
    }
}
