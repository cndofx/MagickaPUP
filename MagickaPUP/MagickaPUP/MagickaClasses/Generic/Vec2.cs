using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Generic
{
    public class Vec2 : XnaObject
    {
        #region Variables

        public float x { get; set; }
        public float y { get; set; }

        #endregion

        #region Constructor

        public Vec2(float x = 0.0f, float y = 0.0f)
        {
            this.x = x;
            this.y = y;
        }

        public Vec2()
        {
            this.x = 0.0f;
            this.y = 0.0f;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Vec2...");

            this.x = reader.ReadSingle();
            this.y = reader.ReadSingle();

            logger?.Log(2, $" - Vec2 = < x = {this.x}, y = {this.y} >");
        }

        public static Vec2 Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new Vec2();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Vec2...");

            writer.Write(this.x);
            writer.Write(this.y);

            logger?.Log(2, $" - < x = {this.x}, y = {this.y} >");
        }

        #endregion
    }
}
