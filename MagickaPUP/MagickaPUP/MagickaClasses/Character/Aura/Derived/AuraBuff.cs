using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Aura.Derived
{
    // TODO : Implement
    public class AuraBuff : Aura
    {
        public float Strength { get; set; }

        public AuraBuff()
        { }

        public AuraBuff(MBinaryReader reader, DebugLogger logger = null)
        { }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {

        }
    }
}
