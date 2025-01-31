using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Abilities.Derived
{
    // Same as ConfuseGrip. Read the comment in that class for context.
    public class DamageGrip : Ability
    {
        public DamageGrip()
        { }

        public DamageGrip(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading DamageGrip Ability...");
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing DamageGrip Ability...");
        }
    }
}
