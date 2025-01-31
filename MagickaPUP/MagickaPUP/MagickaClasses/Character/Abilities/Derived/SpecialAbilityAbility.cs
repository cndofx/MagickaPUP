using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Abilities.Derived
{
    public class SpecialAbilityAbility : Ability
    {
        public float MinRange { get; set; }
        public float MaxRange { get; set; }
        public float Angle { get; set; }
        public int Weapon { get; set; }

        public SpecialAbilityAbility()
        {
            this.MinRange = 0.0f;
            this.MaxRange = 1.0f;
            this.Angle = 0.0f;
            this.Weapon = 0;
        }

        public SpecialAbilityAbility(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading SpecialAbilityAbility Ability...");

            this.MaxRange = reader.ReadSingle();
            this.MinRange = reader.ReadSingle();
            this.Angle = reader.ReadSingle();
            this.Weapon = reader.ReadInt32();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing SpecialAbilityAbility Ability...");

            writer.Write(this.MaxRange);
            writer.Write(this.MinRange);
            writer.Write(this.Angle);
            writer.Write(this.Weapon);
        }
    }
}
