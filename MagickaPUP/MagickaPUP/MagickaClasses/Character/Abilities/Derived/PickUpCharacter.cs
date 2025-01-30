using MagickaPUP.IO;
using MagickaPUP.MagickaClasses.Character.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Abilities.Derived
{
    public class PickUpCharacter : Ability
    {
        public float MinRange { get; set; }
        public float MaxRange { get; set; }
        public float Angle { get; set; }
        public float MaxWeight { get; set; }
        public string DropAnimation { get; set; }

        public PickUpCharacter()
        {
            this.MinRange = 0.0f;
            this.MaxRange = 1.0f;
            this.Angle = 0.0f;
            this.MaxWeight = 0.0f;
            this.DropAnimation = default;
        }

        public PickUpCharacter(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading PickUpCharacter Ability...");

            this.MaxRange = reader.ReadSingle();
            this.MinRange = reader.ReadSingle();
            this.Angle = reader.ReadSingle();
            this.MaxWeight = reader.ReadSingle();
            this.DropAnimation = reader.ReadString();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing PickUpCharacterAbility...");

            writer.Write(this.MaxRange);
            writer.Write(this.MinRange);
            writer.Write(this.Angle);
            writer.Write(this.MaxWeight);
            writer.Write(this.DropAnimation);
        }

    }
}
