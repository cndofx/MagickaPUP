using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Abilities.Derived
{
    public class Ranged : Ability
    {
        public float MinRange { get; set; }
        public float MaxRange { get; set; }
        public float Elevation { get; set; }
        public float Arc { get; set; } // This value is kinda fucking useless considering how the game internally just overwrites the value to always be pi (it specifically uses the float approximation 3.1415927f)
        public float Accuracy { get; set; }
        public int[] Weapons { get; set; }

        public Ranged()
        {
            this.MinRange = 0.0f;
            this.MaxRange = 1.0f;
            this.Elevation = 1.0f;
            this.Arc = 0.0f;
            this.Accuracy = 1.0f;
            this.Weapons = new int[0];
        }

        public Ranged(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Ranged Ability...");

            this.MinRange = reader.ReadSingle();
            this.MaxRange = reader.ReadSingle();
            this.Elevation = reader.ReadSingle();
            this.Arc = reader.ReadSingle();
            this.Accuracy = reader.ReadSingle();

            int numWeapons = reader.ReadInt32();
            this.Weapons = new int[numWeapons];
            for (int i = 0; i < numWeapons; ++i)
                this.Weapons[i] = reader.ReadInt32();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Ranged Ability...");
            throw new NotImplementedException("Write Ranged Ability is not implemented yet!");
        }

    }
}
