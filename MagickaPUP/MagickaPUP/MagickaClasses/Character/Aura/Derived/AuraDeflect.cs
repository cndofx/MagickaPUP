using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Aura.Derived
{
    public class AuraDeflect : Aura
    {
        public float Strength { get; set; }

        public AuraDeflect()
        {
            this.Strength = 0.0f;
        }

        public AuraDeflect(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading AuraDeflect...");

            this.Strength = reader.ReadSingle();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing AuraDeflect...");

            writer.Write(this.Strength);
        }
    }
}
