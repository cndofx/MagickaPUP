using MagickaPUP.Utility.IO;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Physics
{
    // TODO : Implement
    public class ForceField : XnaObject
    {
        #region Variables

        public Vec3 color { get; set; }
        public float width { get; set; }
        public float alphaPower { get; set; }
        public float alphaFalloffPower { get; set; }
        public float maxRadius { get; set; }
        public float rippleDistortion { get; set; }
        public float mapDistortion { get; set; }
        public bool vertexColorEnabled { get; set; }
        public string displacementMap { get; set; } /* ER */
        public float ttl { get; set; }
        public VertexBuffer vertices { get; set; }
        public IndexBuffer indices { get; set; }
        public VertexDeclaration declaration { get; set; }
        public int vertexStride { get; set; }
        public int numVertices { get; set; }
        public int primitiveCount { get; set; }

        #endregion

        #region Constructor

        public ForceField()
        {
            this.color = new Vec3();
            this.width = 0.0f;
            this.alphaPower = 0.0f;
            this.alphaFalloffPower = 0.0f;
            this.maxRadius = 0.0f;
            this.rippleDistortion = 0.0f;
            this.mapDistortion = 0.0f;
            this.vertexColorEnabled = false;
            this.displacementMap = default; /* ER */
            this.ttl = 0.0f;
            this.vertices = new VertexBuffer();
            this.indices = new IndexBuffer();
            this.declaration = new VertexDeclaration();
            this.vertexStride = 0;
            this.numVertices = 0;
            this.primitiveCount = 0;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading ForceField...");

            this.color = Vec3.Read(reader, null);
            this.width = reader.ReadSingle();
            this.alphaPower = reader.ReadSingle();
            this.alphaFalloffPower = reader.ReadSingle();
            this.maxRadius = reader.ReadSingle();
            this.rippleDistortion = reader.ReadSingle();
            this.mapDistortion = reader.ReadSingle();
            this.vertexColorEnabled = reader.ReadBoolean();
            this.displacementMap = reader.ReadString(); /* ER */
            this.ttl = reader.ReadSingle();
            this.vertices = XnaObject.ReadObject<VertexBuffer>(reader, logger);
            this.indices = XnaObject.ReadObject<IndexBuffer>(reader, logger);
            this.declaration = XnaObject.ReadObject<VertexDeclaration>(reader, logger);
            this.vertexStride = reader.ReadInt32();
            this.numVertices = reader.ReadInt32();
            this.primitiveCount = reader.ReadInt32();
        }

        public static ForceField Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new ForceField();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing ForceField...");

            this.color.WriteInstance(writer, logger);
            writer.Write(this.width);
            writer.Write(this.alphaPower);
            writer.Write(this.alphaFalloffPower);
            writer.Write(this.maxRadius);
            writer.Write(this.rippleDistortion);
            writer.Write(this.mapDistortion);
            writer.Write(this.vertexColorEnabled);
            writer.Write(this.displacementMap); /* ER */
            writer.Write(this.ttl);
            XnaObject.WriteObject(this.vertices, writer, logger);
            XnaObject.WriteObject(this.indices, writer, logger);
            XnaObject.WriteObject(this.declaration, writer, logger);
            writer.Write(this.vertexStride);
            writer.Write(this.numVertices);
            writer.Write(this.primitiveCount);
        }

        #endregion
    }
}
