using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagickaPUP.MagickaClasses.Character.Buffs;

namespace MagickaPUP.MagickaClasses.Character.Aura.Derived
{
    // TODO : Implement
    public class AuraBuff : Aura
    {
        public BuffStorage Buff { get; set; }

        public AuraBuff()
        {
            this.Buff = new BuffStorage();
        }

        public AuraBuff(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading AuraBuff...");

            this.Buff = new BuffStorage(reader, logger);
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing AuraBuff...");

            this.Buff.Write(writer, logger);
        }
    }
}
