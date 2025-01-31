using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Collision
{
    public class CollisionTriangle : XnaObject
    {
        #region Variables

        public int index0 { get; set; }
        public int index1 { get; set; }
        public int index2 { get; set; }

        #endregion

        #region Constructor

        public CollisionTriangle()
        {
            this.index0 = -1;
            this.index1 = -1;
            this.index2 = -1;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Collision Triangle...");

            this.index0 = reader.ReadInt32();
            this.index1 = reader.ReadInt32();
            this.index2 = reader.ReadInt32();

            logger?.Log(2, $" - Index 0 : {this.index0}");
            logger?.Log(2, $" - Index 1 : {this.index1}");
            logger?.Log(2, $" - Index 2 : {this.index2}");
        }

        public static CollisionTriangle Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new CollisionTriangle();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Collision Triangle...");

            writer.Write(this.index0);
            writer.Write(this.index1);
            writer.Write(this.index2);

            logger?.Log(2, $" - Index 0 : {this.index0}");
            logger?.Log(2, $" - Index 1 : {this.index1}");
            logger?.Log(2, $" - Index 2 : {this.index2}");
        }

        #endregion
    }
}
