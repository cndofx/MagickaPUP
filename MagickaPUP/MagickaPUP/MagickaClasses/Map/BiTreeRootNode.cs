using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagickaPUP.IO;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.XnaClasses;
using MagickaPUP.MagickaClasses.Effects;
using System.Runtime.Remoting.Messaging;

namespace MagickaPUP.MagickaClasses.Map
{
    public class BiTreeRootNode : XnaObject
    {
        #region Variables

        public bool isVisible { get; set; }
        public bool castsShadows { get; set; }

        public float sway { get; set; }
        public float entityInfluence { get; set; }
        public float groundLevel { get; set; }

        public int numVertices { get; set; }
        public int vertexStride { get; set; }

        public VertexDeclaration vertexDeclaration { get; set; }
        public VertexBuffer vertexBuffer { get; set; }
        public IndexBuffer indexBuffer { get; set; }

        public Effect effect { get; set; }

        public int primitiveCount { get; set; }
        public int startIndex { get; set; }

        public BoundingBox boundingBox { get; set; }

        public bool hasChildA { get; set; }
        public bool hasChildB { get; set; }

        public BiTreeNode childA { get; set; }
        public BiTreeNode childB { get; set; }


        #endregion

        #region Constructor

        public BiTreeRootNode()
        {
            this.isVisible = false;
            this.castsShadows = false;

            this.sway = 0.0f;
            this.entityInfluence = 0.0f;
            this.groundLevel = 0.0f;

            this.numVertices = 0;
            this.vertexStride = 0;

            this.vertexDeclaration = new VertexDeclaration();
            this.vertexBuffer = new VertexBuffer();
            this.indexBuffer = new IndexBuffer();

            this.effect = new Effect();

            this.primitiveCount = 0;
            this.startIndex = 0;

            this.boundingBox = new BoundingBox();

            this.hasChildA = false;
            this.hasChildB = false;

            this.childA = null;
            this.childB = null;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading BiTreeRootNode...");
            
            this.isVisible = reader.ReadBoolean();
            this.castsShadows = reader.ReadBoolean();
            this.sway = reader.ReadSingle();
            this.entityInfluence = reader.ReadSingle();
            this.groundLevel = reader.ReadSingle();
            this.numVertices = reader.ReadInt32();
            this.vertexStride = reader.ReadInt32();

            logger?.Log(2, $" - Vertices : {this.numVertices}");

            this.vertexDeclaration = XnaObject.ReadObject<VertexDeclaration>(reader, logger); // this used to call VertexDeclaration.Read(reader);
            this.vertexBuffer = XnaObject.ReadObject<VertexBuffer>(reader, logger); // this used to call VertexBuffer.Read(reader);
            this.indexBuffer = XnaObject.ReadObject<IndexBuffer>(reader, logger); // this used to call IndexBuffer.Read(reader);
            this.effect = XnaObject.ReadObject<Effect>(reader, logger); // this used to call Effect.Read(reader);

            this.primitiveCount = reader.ReadInt32();
            this.startIndex = reader.ReadInt32();

            logger?.Log(2, $" - Primitive Count : {this.primitiveCount}");
            logger?.Log(2, $" - Start Index     : {this.startIndex}");

            this.boundingBox = BoundingBox.Read(reader, null);

            this.hasChildA = reader.ReadBoolean();
            if (this.hasChildA)
                this.childA = BiTreeNode.Read(reader, logger);

            this.hasChildB = reader.ReadBoolean();
            if (this.hasChildB)
                this.childB = BiTreeNode.Read(reader, logger);
        }

        public static BiTreeRootNode Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new BiTreeRootNode();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing BiTreeRootNode...");

            writer.Write(this.isVisible);
            writer.Write(this.castsShadows);
            writer.Write(this.sway);
            writer.Write(this.entityInfluence);
            writer.Write(this.groundLevel);
            writer.Write(this.numVertices);
            writer.Write(this.vertexStride);

            XnaObject.WriteObject(this.vertexDeclaration, writer, logger);
            XnaObject.WriteObject(this.vertexBuffer, writer, logger);
            XnaObject.WriteObject(this.indexBuffer, writer, logger);
            XnaObject.WriteObject(this.effect, writer, logger);

            writer.Write(this.primitiveCount);
            writer.Write(this.startIndex);

            this.boundingBox.WriteInstance(writer, logger);

            writer.Write(this.hasChildA);
            if (this.hasChildA)
                this.childA.WriteInstance(writer, logger);

            writer.Write(this.hasChildB);
            if (this.hasChildB)
                this.childB.WriteInstance(writer, logger);
        }

        #endregion
    }
}
