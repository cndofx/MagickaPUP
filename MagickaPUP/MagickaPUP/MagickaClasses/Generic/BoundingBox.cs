using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagickaPUP.IO;
using MagickaPUP.XnaClasses;

namespace MagickaPUP.MagickaClasses.Generic
{
    public class BoundingBox : XnaObject
    {
        #region Variables

        public Vec3 Min { get; set; }
        public Vec3 Max { get; set; }

        #endregion

        #region Constructors

        public BoundingBox(Vec3 min, Vec3 max)
        {
            this.Min = min;
            this.Max = max;
        }

        public BoundingBox(float minX, float minY, float minZ, float maxX, float maxY, float maxZ)
        {
            this.Min = new Vec3(minX, minY, minZ);
            this.Max = new Vec3(maxX, maxY, maxZ);
        }

        public BoundingBox()
        {
            this.Min = new Vec3();
            this.Max = new Vec3();
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading BoundingBox...");

            this.Min = Vec3.Read(reader, null);
            this.Max = Vec3.Read(reader, null);

            logger?.Log(2, $" - BoundingBox.min = < {this.Min.x}, {this.Min.y}, {this.Min.z} >");
            logger?.Log(2, $" - BoundingBox.max = < {this.Max.x}, {this.Max.y}, {this.Max.z} >");
        }

        public static BoundingBox Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new BoundingBox();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing BoundingBox...");
            this.Min.WriteInstance(writer, logger);
            this.Max.WriteInstance(writer, logger);
        }

        #endregion
    }
}
