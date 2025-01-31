using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Abilities.Derived
{
    public class Jump : Ability
    {
        public float MinRange { get; set; }
        public float MaxRange { get; set; }
        public float Angle { get; set; }
        public float Elevation { get; set; }

        public Jump()
        {
            this.MinRange = 0.0f;
            this.MaxRange = 1.0f;
            this.Angle = 0.0f;
            this.Elevation = 1.0f;
        }

        public Jump(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Jump Ability...");

            this.MaxRange = reader.ReadSingle();
            this.MinRange = reader.ReadSingle();
            this.Angle = reader.ReadSingle();
            this.Elevation = reader.ReadSingle();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Jump Ability...");

            writer.Write(this.MaxRange);
            writer.Write(this.MinRange);
            writer.Write(this.Angle);
            writer.Write(this.Elevation);
        }
    }
}
