using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Aura.Derived
{
    // TODO : Implement
    public class AuraBoost : Aura
    {
        public float Magnitude { get; set; }

        public AuraBoost()
        {
            this.Magnitude = 0.0f;
        }

        public AuraBoost(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading AuraBoost...");

            this.Magnitude = reader.ReadSingle();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing AuraBoost...");

            writer.Write(this.Magnitude);
        }
    }
}
