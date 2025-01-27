using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Abilities.Derived
{
    public class Block : Ability
    {
        public float Arc { get; set; }
        public int Shield { get; set; }

        public Block()
        {
            this.Arc = 0.0f;
            this.Shield = 0;
        }

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Block Ability...");

            this.Arc = reader.ReadSingle();
            this.Shield = reader.ReadInt32();
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Block Ability...");

            writer.Write(this.Arc);
            writer.Write(this.Shield);
        }
    }
}
