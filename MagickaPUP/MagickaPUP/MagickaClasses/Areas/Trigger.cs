using MagickaPUP.Utility.IO;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Areas
{
    public class Trigger : XnaObject
    {
        #region Variables

        public string Name { get; set; }
        public Vec3 Position { get; set; }
        public Vec3 SideLengths { get; set; }
        public Quaternion Rotation { get; set; }

        #endregion

        #region Constructor

        public Trigger()
        {
            this.Name = "__none__";
            this.Position = new Vec3();
            this.SideLengths = new Vec3();
            this.Rotation = new Quaternion();
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Trigger...");

            this.Name = reader.ReadString();
            this.Position = Vec3.Read(reader, logger);
            this.SideLengths = Vec3.Read(reader, logger);
            this.Rotation = Quaternion.Read(reader, logger);

            logger?.Log(2, $" - Name : \"{this.Name}\"");
        }

        public static Trigger Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new Trigger();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Trigger...");

            writer.Write(this.Name);
            this.Position.WriteInstance(writer, null);
            this.SideLengths.WriteInstance(writer, null);
            this.Rotation.WriteInstance(writer, null);

            logger?.Log(2, $" - Name : \"{this.Name}\"");
        }

        #endregion
    }
}
