using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Generic
{
    public class Quaternion : XnaObject
    {
        #region Variables

        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public float w { get; set; }

        #endregion

        #region Constructor

        public Quaternion(float x = 0.0f, float y = 0.0f, float z = 0.0f, float w = 0.0f)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Quaternion()
        {
            this.x = 0.0f;
            this.y = 0.0f;
            this.z = 0.0f;
            this.w = 0.0f;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Quaternion...");

            this.x = reader.ReadSingle();
            this.y = reader.ReadSingle();
            this.z = reader.ReadSingle();
            this.w = reader.ReadSingle();

            logger?.Log(2, $" - Quaternion = < x = {this.x}, y = {this.y}, z = {this.z}, w = {this.w} >");
        }

        public static Quaternion Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new Quaternion();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Quaternion...");

            writer.Write(this.x);
            writer.Write(this.y);
            writer.Write(this.z);
            writer.Write(this.w);
        }

        #endregion
    }
}
