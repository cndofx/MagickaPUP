using MagickaPUP.IO;
using MagickaPUP.MagickaClasses.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Abilities.Derived
{
    public class CastSpell : Ability
    {
        public float MinRange { get; set; }
        public float MaxRange { get; set; }
        public float Arc { get; set; }
        public float ChantSpeed { get; set; }
        public float Power { get; set; }
        public CastType CastType { get; set; }
        public Elements[] Elements { get; set; }

        public CastSpell()
        {
            this.MinRange = 0.0f;
            this.MaxRange = 1.0f;
            this.Arc = 1.0f;
            this.ChantSpeed = 1.0f;
            this.Power = 1.0f;
            this.CastType = default;
            this.Elements = new Elements[0];
        }

        public CastSpell(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading CastSpell Ability...");

            this.MinRange = reader.ReadSingle();
            this.MaxRange = reader.ReadSingle();
            this.Arc = reader.ReadSingle();
            this.ChantSpeed = reader.ReadSingle();
            this.Power = reader.ReadSingle();
            this.CastType = (CastType)reader.ReadInt32();

            int numElements = reader.ReadInt32(); // NOTE : I'm experimenting with this idea of not storing the number of elements within arrays and lists manually within the JSON since this should be obtained automatically when reading and encoded within the array / list object itself...
            this.Elements = new Elements[numElements];
            for (int i = 0; i < numElements; ++i)
                this.Elements[i] = (Elements)reader.ReadInt32();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing CastSpell Ability...");

            writer.Write(this.MinRange);
            writer.Write(this.MaxRange);
            writer.Write(this.Arc);
            writer.Write(this.ChantSpeed);
            writer.Write(this.Power);
            writer.Write((int)this.CastType);

            writer.Write((int)Elements.Length);
            foreach (var element in this.Elements)
                writer.Write((int)element);
        }
    }
}
