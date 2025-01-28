using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Aura.Derived
{
    // TODO : Implement
    public class AuraLifeSteal : Aura
    {
        public float Amount { get; set; }

        public AuraLifeSteal()
        {
            this.Amount = 0.0f;
        }

        public AuraLifeSteal(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading AuraLifeSteal...");

            this.Amount = reader.ReadSingle();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing AuraLifeSteal...");

            writer.Write(this.Amount);
        }
    }
}
