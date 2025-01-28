using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Aura.Derived
{
    public class AuraLove : Aura
    {
        public float Radius { get; set; }
        public float Time { get; set; }

        public AuraLove()
        {
            this.Radius = 1.0f;
            this.Time = 1.0f;
        }

        public AuraLove(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading AuraLove...");

            this.Radius = reader.ReadSingle();
            this.Time = reader.ReadSingle();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing AuraLove...");

            writer.Write(this.Radius);
            writer.Write(this.Time);
        }
    }
}
