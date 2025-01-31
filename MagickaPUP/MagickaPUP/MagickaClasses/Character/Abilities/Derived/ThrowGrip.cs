using MagickaPUP.Utility.IO;
using MagickaPUP.MagickaClasses.Data;
using System;
using System.Collections.Generic;
using MagickaPUP.Utility.Exceptions;

namespace MagickaPUP.MagickaClasses.Character.Abilities.Derived
{
    public class ThrowGrip : Ability
    {
        public float MinRange { get; set; }
        public float MaxRange { get; set; }
        public float Elevation { get; set; }
        public Damage[] Damage { get; set; } // NOTE : The max amount of damage fields allowed here is any value, but Magicka clamps this to min(count, 4), so we should have 4 fields at most for correctness...

        public ThrowGrip(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading ThrowGrip Ability...");

            this.MinRange = reader.ReadSingle();
            this.MaxRange = reader.ReadSingle();
            this.Elevation = reader.ReadSingle();

            // TODO : Recheck this part within Magicka's code to make sure that capping this at 4 and throwing an exception is the wisest / best thing to do...

            // We could either throw an exception and halt in case that we input more than the 4 allowed values, or have some kind of warning log msg.
            // We could also just choose to be permissive and silently pick the 4 first fields so as to not freeze compilation of malformed input files...
            // tho here, on the XNB binary reading side, we should fail. On the JSON side is where we could be more permissive. Which is why we throw an exception here,
            // but not on the write side, where the input came from JSON instead.
            int numDamageFields = reader.ReadInt32(); // Math.Min(4, reader.ReadInt32());
            if (numDamageFields > 4)
                throw new MagickaReadException(GetDamagesCountException(numDamageFields));
            this.Damage = new Damage[numDamageFields];
            for (int i = 0; i < numDamageFields; ++i)
            {
                AttackProperties attackProperty = (AttackProperties)reader.ReadInt32();
                Elements element = (Elements)reader.ReadInt32();
                float amount = (float)reader.ReadInt32();
                float magnitude = reader.ReadSingle();
                this.Damage[i] = new Damage(attackProperty, element, amount, magnitude);
            }
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing ThrowGrip Ability...");

            writer.Write(this.MinRange);
            writer.Write(this.MaxRange);
            writer.Write(this.Elevation);

            int numDamageFields = this.Damage.Length;
            if (numDamageFields > 4)
                throw new MagickaWriteException(GetDamagesCountException(numDamageFields));
            foreach (var dmg in this.Damage)
            {
                writer.Write((int)dmg.AttackProperty);
                writer.Write((int)dmg.Element);
                writer.Write((int)dmg.Amount);
                writer.Write((float)dmg.Magnitude);
            }
        }

        private string GetDamagesCountException(int numDamageFields)
        {
            return $"ThrowGrip Ability allows up to 4 Damage fields, but {numDamageFields} were found!";
        }
    }
}
