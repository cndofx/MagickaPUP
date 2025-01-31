using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Abilities.Derived
{
    public class RemoveStatus : Ability
    {
        public RemoveStatus()
        { }

        public RemoveStatus(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading RemoveStatus Ability...");
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing RemoveStatus Ability...");
        }
    }
}
