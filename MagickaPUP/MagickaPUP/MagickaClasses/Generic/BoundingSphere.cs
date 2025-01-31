using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses;

namespace MagickaPUP.MagickaClasses.Generic
{
    public class BoundingSphere : XnaObject
    {
        #region Variables

        public Vec3 Center { get; set; }
        public float Radius { get; set; }

        #endregion

        #region Constructors

        public BoundingSphere(Vec3 center, float radius)
        {
            this.Center = center;
            this.Radius = radius;
        }

        public BoundingSphere(float x, float y, float z, float radius)
        {
            this.Center = new Vec3(x, y, z);
            this.Radius = radius;
        }

        public BoundingSphere()
        {
            this.Center = new Vec3();
            this.Radius = 0.0f;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading BoundingSphere...");

            this.Center = Vec3.Read(reader, null);
            this.Radius = reader.ReadSingle();

            logger?.Log(2, $" - BoundingSphere.Center = < {this.Center.x}, {this.Center.y}, {this.Center.z} >");
            logger?.Log(2, $" - BoundingSphere.Radius = {this.Radius}");
        }

        public static BoundingSphere Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new BoundingSphere();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing BoundingSphere...");
            
            this.Center.WriteInstance(writer, logger);
            writer.Write(this.Radius);
        }

        #endregion
    }
}
