using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Abilities.Derived
{
    public class ElementSteal : Ability
    {
        public float Range { get; set; }
        public float Angle { get; set; }

        public ElementSteal()
        {
            this.Range = 0.0f;
            this.Angle = 0.0f;
        }

        public ElementSteal(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading ElementSteal Ability...");

            this.Range = reader.ReadSingle();
            this.Angle = reader.ReadSingle();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing ElementSteal Ability...");

            writer.Write(this.Range);
            writer.Write(this.Angle);
        }
    }
}
