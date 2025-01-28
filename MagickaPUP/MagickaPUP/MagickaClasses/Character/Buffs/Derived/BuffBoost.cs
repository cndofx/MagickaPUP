using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Buffs.Derived
{
    public class BuffBoost : Buff
    {
        float Amount { get; set; }

        public BuffBoost()
        {
            this.Amount = 0.0f;
        }

        public BuffBoost(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading BuffBoost...");

            this.Amount = reader.ReadSingle();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing BuffBoost...");

            writer.Write(this.Amount);
        }
    }
}
