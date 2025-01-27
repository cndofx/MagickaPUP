using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Abilities.Derived
{
    // NOTE : This class literally reads fucking NOTHING in Magicka's code, so here it exists as a sort of proxy for the reading process.
    internal class ConfuseGrip : Ability
    {
        public ConfuseGrip()
        { }

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading ConfuseGrip Ability...");
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing ConfuseGrip Ability...");
        }
    }
}
