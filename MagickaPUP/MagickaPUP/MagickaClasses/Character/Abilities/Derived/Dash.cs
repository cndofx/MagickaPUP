using MagickaPUP.IO;
using MagickaPUP.MagickaClasses.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Abilities.Derived
{
    public class Dash : Ability
    {
        public float MinRange { get; set; }
        public float MaxRange { get; set; }
        public float Arc { get; set; }
        public Vec3 Vector { get; set; }

        public Dash()
        {
            this.MinRange = 0.0f;
            this.MaxRange = 1.0f;
            this.Arc = 1.0f;
            this.Vector = new Vec3();
        }

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Dash Ability...");
            
            this.MinRange = reader.ReadSingle();
            this.MaxRange = reader.ReadSingle();
            this.Arc = reader.ReadSingle();
            this.Vector = Vec3.Read(reader, logger);
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Dash Ability...");
            throw new NotImplementedException("Write Dash Ability is not implemented yet!");
        }
    }
}
