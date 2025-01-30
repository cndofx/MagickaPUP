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
                throw new MagickaLoadException($"The specified Ability Type does not exist! (\"{this.AbilityName}\")");

            switch (abilityType)
            {
                case AbilityType.Block:
                    this.Ability = new Block();
                    break;
                case AbilityType.CastSpell:
                    this.Ability = new CastSpell();
                    break;
                case AbilityType.ConfuseGrip:
                    this.Ability = new ConfuseGrip();
                    break;
                case AbilityType.DamageGrip:
                    this.Ability = new DamageGrip();
                    break;
                case AbilityType.Dash:
                    this.Ability = new Dash();
                    break;
                case AbilityType.ElementalSteal:
                    this.Ability = new ElementSteal();
                    break;
                case AbilityType.GripCharacterFromBehind:
                    this.Ability = new GripCharacterFromBehind();
                    break;
                case AbilityType.Jump:
                    this.Ability = new Jump();
                    break;
                case AbilityType.Melee:
                    this.Ability = new Melee();
                    break;
                case AbilityType.PickUpCharacter:
                    this.Ability = new PickUpCharacter();
                    break;
                case AbilityType.Ranged:
                    this.Ability = new Ranged();
                    break;
                case AbilityType.RemoveStatus:
                    this.Ability = new RemoveStatus();
                    break;
                case AbilityType.SpecialAbilityAbility: // can't get over the fact that this is an actual name, damn... lmao.
                    this.Ability = new SpecialAbilityAbility();
                    break;
                case AbilityType.ThrowGrip:
                    this.Ability = new ThrowGrip();
                    break;
                case AbilityType.ZombieGrip:
                    this.Ability = new ZombieGrip();
                    break;
                default:
                    throw new MagickaLoadException("WTF this should never happen!!! (Ability.cs)"); // lol
                    break; // OMEGALUL
            }

            this.Cooldown = reader.ReadSingle();
            this.Target = (Target)reader.ReadByte();
            this.HasFuzzyExpression = reader.ReadBoolean();
            if (this.HasFuzzyExpression)
                this.FuzzyExpression = reader.ReadString();
            this.NumAnimations = reader.ReadInt32();
            this.AnimationKeys = new string[this.NumAnimations];
            for (int i = 0; i < this.NumAnimations; ++i)
                this.AnimationKeys[i] = reader.ReadString();

            this.Ability.ReadInstance(reader, logger);
        }

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing AbilityStorage...");
            throw new NotImplementedException("Write AbilityStorage is not implemented yet!");
        }
    }
}
