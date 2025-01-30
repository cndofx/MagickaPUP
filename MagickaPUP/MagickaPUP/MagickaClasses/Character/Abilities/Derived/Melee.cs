using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Abilities.Derived
{
    public class Melee : Ability
    {
        public float MinRange { get; set; }
        public float MaxRange { get; set; }
        public float Arc { get; set; }
        public int[] Weapons { get; set; }
        public bool Rotate { get; set; }

        public Melee()
        {
            this.MinRange = 0.0f;
            this.MaxRange = 1.0f;
            this.Arc = 0.0f;
            this.Weapons = new int[0];
            this.Rotate = false;
        }

        public Melee(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Melee Ability...");

            this.MinRange = reader.ReadSingle();
            this.MaxRange = reader.ReadSingle();
            this.Arc = reader.ReadSingle();

            int numWeapons = reader.ReadInt32();
            this.Weapons = new int[numWeapons];
            for (int i = 0; i < numWeapons; ++i)
            {
                this.Weapons[i] = reader.ReadInt32();
            }

            this.Rotate = reader.ReadBoolean();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Melee Ability...");
            throw new NotImplementedException("Write Melee Ability is not implemented yet!");
        }

    }
}
