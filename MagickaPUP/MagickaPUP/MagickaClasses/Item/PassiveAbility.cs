using MagickaPUP.MagickaClasses.Data;
using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Item
{
    public struct PassiveAbility
    {

        public PassiveAbilityType Ability { get; set; }
        public float Variable { get; set; } // TODO : Figure out what the fuck this thing does... but by the looks of it, it is probably the magnitude of the ability, or something like that...

        public PassiveAbility(PassiveAbilityType ability = PassiveAbilityType.None, float variable = 0.0f)
        {
            this.Ability = ability;
            this.Variable = variable;
        }

        public PassiveAbility(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Passive Ability...");

            this.Ability = (PassiveAbilityType)reader.ReadByte();
            this.Variable = reader.ReadSingle();
        }

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Passive Ability...");

            writer.Write((byte)this.Ability);
            writer.Write(this.Variable);
        }
    }
}
