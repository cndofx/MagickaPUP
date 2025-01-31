using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Buffs.Derived
{
    public class BuffModifySpellRange : Buff
    {
        public float RangeMultiplier { get; set; }
        public float RangeModifier { get; set; }

        public BuffModifySpellRange()
        {
            this.RangeMultiplier = 1.0f;
            this.RangeModifier = 0.0f;
        }

        public BuffModifySpellRange(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading BuffModifySpellRange...");

            this.RangeMultiplier = reader.ReadSingle();
            this.RangeModifier = reader.ReadSingle();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing BuffModifySpellRange...");

            writer.Write(this.RangeMultiplier);
            writer.Write(this.RangeModifier);
        }

    }
}
