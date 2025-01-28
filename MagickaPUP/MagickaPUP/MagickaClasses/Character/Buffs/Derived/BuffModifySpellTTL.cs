using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Buffs.Derived
{
    public class BuffModifySpellTTL : Buff
    {
        public float TimeMultiplier { get; set; }
        public float TimeModifier { get; set; }

        public BuffModifySpellTTL()
        {
            this.TimeMultiplier = 1.0f;
            this.TimeModifier = 0.0f;
        }

        public BuffModifySpellTTL(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading BuffModifySpellTTL...");

            this.TimeMultiplier = reader.ReadSingle();
            this.TimeModifier = reader.ReadSingle();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing BuffModifySpellTLL...");

            writer.Write(this.TimeMultiplier);
            writer.Write(this.TimeModifier);
        }
    }
}
