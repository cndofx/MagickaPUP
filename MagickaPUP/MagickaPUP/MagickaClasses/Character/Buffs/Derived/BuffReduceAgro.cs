using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Buffs.Derived
{
    public class BuffReduceAgro : Buff
    {
        public float Amount { get; set; }

        public BuffReduceAgro()
        {
            this.Amount = 0.0f;
        }

        public BuffReduceAgro(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading BuffReduceAgro...");

            this.Amount = reader.ReadSingle();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing BuffReduceAgro...");

            writer.Write(this.Amount);
        }
    }
}
