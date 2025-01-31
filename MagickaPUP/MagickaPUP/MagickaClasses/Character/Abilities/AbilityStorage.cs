using MagickaPUP.IO;
using MagickaPUP.MagickaClasses.Character.Abilities.Derived;
using MagickaPUP.MagickaClasses.Data;
using MagickaPUP.Utility.Exceptions;
using System;
using System.Collections.Generic;

namespace MagickaPUP.MagickaClasses.Character.Abilities
{
    // NOTE : This class does NOT exist within Magicka's code, it's just a proxy class made to make things easier to handle for me.
    public class AbilityStorage
    {
        public string AbilityName { get; set; } // TODO : Rename this in the future to AbilityType. Also, maybe change this into an enum variable and then handle the string conversions so that we can get some type validation more easily?
        public float Cooldown { get; set; }
        public Target Target { get; set; }
        public bool HasFuzzyExpression { get; set; }
        public string FuzzyExpression { get; set; }
        public int NumAnimations { get; set; }
        public string[] AnimationKeys { get; set; }
        public Ability Ability { get; set; } // NOTE : Polymorphic type

        public AbilityStorage()
        {
            this.AbilityName = string.Empty;
            this.Cooldown = 0.0f;
            this.Target = Target.Self;
            this.HasFuzzyExpression = false;
            this.FuzzyExpression = string.Empty;
            this.NumAnimations = 0;
            this.AnimationKeys = new string[0];
        }

        public AbilityStorage(MBinaryReader reader, DebugLogger logger = null)
        {
            this.AbilityName = reader.ReadString();
            AbilityType abilityType; // Read the TODO at the top of this class's variable declarations to understand what you've got to do lol...
            bool success = Enum.TryParse<AbilityType>(this.AbilityName, true, out abilityType);

            if (!success)
                throw new MagickaReadException($"The specified Ability Type does not exist! (\"{this.AbilityName}\")");

            this.Cooldown = reader.ReadSingle();
            this.Target = (Target)reader.ReadByte();
            this.HasFuzzyExpression = reader.ReadBoolean();
            if (this.HasFuzzyExpression)
                this.FuzzyExpression = reader.ReadString();
            this.NumAnimations = reader.ReadInt32();
            this.AnimationKeys = new string[this.NumAnimations];
            for (int i = 0; i < this.NumAnimations; ++i)
                this.AnimationKeys[i] = reader.ReadString();

            switch (abilityType)
            {
                case AbilityType.Block:
                    this.Ability = new Block(reader, logger);
                    break;
                case AbilityType.CastSpell:
                    this.Ability = new CastSpell(reader, logger);
                    break;
                case AbilityType.ConfuseGrip:
                    this.Ability = new ConfuseGrip(reader, logger);
                    break;
                case AbilityType.DamageGrip:
                    this.Ability = new DamageGrip(reader, logger);
                    break;
                case AbilityType.Dash:
                    this.Ability = new Dash(reader, logger);
                    break;
                case AbilityType.ElementalSteal:
                    this.Ability = new ElementSteal(reader, logger);
                    break;
                case AbilityType.GripCharacterFromBehind:
                    this.Ability = new GripCharacterFromBehind(reader, logger);
                    break;
                case AbilityType.Jump:
                    this.Ability = new Jump(reader, logger);
                    break;
                case AbilityType.Melee:
                    this.Ability = new Melee(reader, logger);
                    break;
                case AbilityType.PickUpCharacter:
                    this.Ability = new PickUpCharacter(reader, logger);
                    break;
                case AbilityType.Ranged:
                    this.Ability = new Ranged(reader, logger);
                    break;
                case AbilityType.RemoveStatus:
                    this.Ability = new RemoveStatus(reader, logger);
                    break;
                case AbilityType.SpecialAbilityAbility: // can't get over the fact that this is an actual name, damn... lmao.
                    this.Ability = new SpecialAbilityAbility(reader, logger);
                    break;
                case AbilityType.ThrowGrip:
                    this.Ability = new ThrowGrip(reader, logger);
                    break;
                case AbilityType.ZombieGrip:
                    this.Ability = new ZombieGrip(reader, logger);
                    break;
                default:
                    throw new MagickaReadException("WTF this should never happen!!! (Ability.cs)"); // lol...
                    break; // OMEGALUL
            }
        }

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing AbilityStorage...");

            writer.Write(this.AbilityName);

            writer.Write(this.Cooldown);
            writer.Write((byte)this.Target);
            writer.Write(this.HasFuzzyExpression);
            if (this.HasFuzzyExpression)
                writer.Write(this.FuzzyExpression);
            writer.Write(this.NumAnimations);
            foreach (var key in this.AnimationKeys)
                writer.Write(key);

            this.Ability.Write(writer, logger);
        }
    }
}
