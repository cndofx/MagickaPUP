using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    public class Jump : AnimationAction
    {
        public float Elevation { get; set; }
        public bool HasMinRange { get; set; }
        public bool HasMaxRange { get; set; }
        public float MinRange { get; set; }
        public float MaxRange { get; set; }

        public Jump()
        {
            this.Elevation = 0.0f;
            this.HasMinRange = true;
            this.HasMaxRange = true;
            this.MinRange = 0.0f;
            this.MaxRange = 1.0f;
        }

        public Jump(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Jump AnimationAction...");

            this.Elevation = reader.ReadSingle();
            this.HasMinRange = reader.ReadBoolean();
            if (this.HasMinRange)
                this.MinRange = reader.ReadSingle();
            this.HasMaxRange = reader.ReadBoolean();
            if (this.HasMaxRange)
                this.MaxRange = reader.ReadSingle();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Jump AnimationAction...");

            writer.Write(this.Elevation);
            writer.Write(this.HasMinRange);
            if (this.HasMinRange)
                writer.Write(this.MinRange);
            writer.Write(this.HasMaxRange);
            if (this.HasMaxRange)
                writer.Write(this.MaxRange);
        }
    }
}
