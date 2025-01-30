using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Abilities.Derived
{
    public class GripCharacterFromBehind : Ability
    {
        public float MinRange { get; set; }
        public float MaxRange { get; set; }
        public float Angle { get; set; }
        public float MaxWeight { get; set; }

        public GripCharacterFromBehind()
        {
            this.MinRange = 0.0f;
            this.MaxRange = 1.0f;
            this.Angle = 0.0f;
            this.MaxWeight = 0.0f;
        }

        public GripCharacterFromBehind(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading GripCharacterFromBehind Ability...");

            this.MaxRange = reader.ReadSingle();
            this.MinRange = reader.ReadSingle();
            this.Angle = reader.ReadSingle();
            this.MaxWeight = reader.ReadSingle();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing GripCharacterFromBehind Ability...");
            throw new NotImplementedException("Write GripCharacterFromBehind is not implemented yet!");
        }
    }
}
