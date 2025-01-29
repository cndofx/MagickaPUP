using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    public class SpecialAbility : AnimationAction
    {
        public int Weapon { get; set; }
        public int Ability { get; set; }

        public SpecialAbility()
        {
            this.Weapon = 0;
            this.Ability = 0;
        }

        public SpecialAbility(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading SpecialAbility AnimationAction...");

            this.Weapon = reader.ReadInt32();
            if (this.Weapon < 0)
                this.Ability = reader.ReadInt32();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing SpecialAbility AnimationAction...");

            writer.Write(this.Weapon);
            if (this.Weapon < 0)
                writer.Write(this.Ability);
        }
    }
}
