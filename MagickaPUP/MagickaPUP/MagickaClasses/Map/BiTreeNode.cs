using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses;
using System.Runtime.Remoting.Messaging;

namespace MagickaPUP.MagickaClasses.Map
{
    public class BiTreeNode : XnaObject
    {
        #region Variables

        public int PrimitiveCount { get; set; }
        public int StartIndex { get; set; }
        public BoundingBox BoundingBox { get; set; }
        public bool HasChildA { get; set; }
        public bool HasChildB { get; set; }
        public BiTreeNode ChildA { get; set; }
        public BiTreeNode ChildB { get; set; }

        #endregion

        #region Constructor

        public BiTreeNode()
        {
            this.PrimitiveCount = 0;
            this.StartIndex = 0;
            this.BoundingBox = new BoundingBox(0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f);
            this.HasChildA = false;
            this.HasChildB = false;
            this.ChildA = null;
            this.ChildB = null;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading BiTreeNode...");
            
            this.PrimitiveCount = reader.ReadInt32();
            this.StartIndex = reader.ReadInt32();
            this.BoundingBox = BoundingBox.Read(reader, null);

            logger?.Log(2, $" - Primitive Count : {this.PrimitiveCount}");
            logger?.Log(2, $" - Start Index     : {this.StartIndex}");

            this.HasChildA = reader.ReadBoolean();
            if (this.HasChildA)
                this.ChildA = BiTreeNode.Read(reader, logger);

            this.HasChildB = reader.ReadBoolean();
            if (this.HasChildB)
                this.ChildB = BiTreeNode.Read(reader, logger);
        }

        public static BiTreeNode Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new BiTreeNode();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing BiTreeNode...");

            writer.Write(this.PrimitiveCount);
            writer.Write(this.StartIndex);
            
            this.BoundingBox.WriteInstance(writer, logger);

            writer.Write(this.HasChildA);
            if (this.HasChildA)
                this.ChildA.WriteInstance(writer, logger);

            writer.Write(this.HasChildB);
            if (this.HasChildB)
                this.ChildB.WriteInstance(writer, logger);
        }

        #endregion
    }
}
