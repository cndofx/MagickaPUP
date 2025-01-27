using MagickaPUP.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using MagickaPUP.Utility.Exceptions;
using MagickaPUP.MagickaClasses.Character.Abilities.Derived;
using MagickaPUP.MagickaClasses.Data;
using MagickaPUP.MagickaClasses.Character.Animation;
using System.Runtime.Remoting.Messaging;

namespace MagickaPUP.MagickaClasses.Character.Abilities
{
    // TODO : I really need to rethink the whole XnaObject inheritance system.
    // It worked great for the whole map stuff, but here it's become an utter pain in the ass.
    // Altough, for the XNB writing side, inheritting from XnaObject again would make things easier... idk, maybe I just need to find a new interface to do things that doesn't require this much boilerplate...
    
    // TODO : Implement Writing logic too... and finish implementing the logic for read / write for all of the specific ability types.
    
    // NOTE : Maybe in the future it would make more sense to skip the whole polymorphic step and just make the Ability class contain all of the variables?
    // Similar to how things would be done on Blender's UI side of things where internally we reuse the same fields for certain objects that have repeated
    // characteristics / properties despite being of different types? Or maybe it's ok like this, idk. The point is that all of this stuff will eventually be
    // abstracted away from the user's point of view so it should not matter as long as the implementation is robust enough and fast af boiiii!!!!

    // NOTE : If you look at all of the internal parameters of each specific ability type, you may realize that most of the parameters collide...
    // we could have probably implemented this in an easier way by just having the base ability class contain all of the fields and change the data it reads
    // according to the type rather than holding it through polymorphism, since we don't implement the logic in the end...
    // Also maybe C unions would have made this orders of magnitude easier and less boilerplate-y, but whatever... "high level languages" or something...

    public class Ability : XnaObject
    {
        #region Variables

        public string AbilityName { get; set; }

        public float Cooldown { get; set; }
        public Target Target { get; set; }
        public bool HasFuzzyExpression { get; set; }
        public string FuzzyExpression { get; set; }
        public int NumAnimations { get; set; }
        public string[] AnimationKeys { get; set; }

        #endregion

        #region Constructor

        public Ability()
        {
            this.AbilityName = string.Empty;
            this.Cooldown = 0.0f;
            this.Target = Target.Self;
            this.HasFuzzyExpression = false;
            this.FuzzyExpression = string.Empty;
            this.NumAnimations = 0;
            this.AnimationKeys = new string[0];
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Ability...");
            throw new MagickaWriteException("Cannot read base Ability class directly! Type is polymorphic.");
        }

        public static Ability Read(MBinaryReader reader, DebugLogger logger = null)
        {
            string abilityName = reader.ReadString();
            AbilityType abilityType;
            bool success = Enum.TryParse<AbilityType>(abilityName, true, out abilityType);

            if (!success)
                throw new MagickaLoadException("The specified Ability Type does not exist!");

            Ability ans = null;
            switch (abilityType)
            {
                case AbilityType.Block:
                    ans = new Block();
                    break;
                case AbilityType.CastSpell:
                    ans = new CastSpell();
                    break;
                case AbilityType.ConfuseGrip:
                    ans = new ConfuseGrip();
                    break;
                case AbilityType.DamageGrip:
                    ans = new DamageGrip();
                    break;
                case AbilityType.Dash:
                    ans = new Dash();
                    break;
                case AbilityType.ElementalSteal:
                    ans = new ElementSteal();
                    break;
                case AbilityType.GripCharacterFromBehind:
                    ans = new GripCharacterFromBehind();
                    break;
                case AbilityType.Jump:
                    ans = new Jump();
                    break;
                case AbilityType.Melee:
                    ans = new Melee();
                    break;
                case AbilityType.PickUpCharacter:
                    ans = new PickUpCharacter();
                    break;
                case AbilityType.Ranged:
                    ans = new Ranged();
                    break;
                case AbilityType.RemoveStatus:
                    ans = new RemoveStatus();
                    break;
                case AbilityType.SpecialAbilityAbility: // can't get over the fact that this is an actual name, damn... lmao.
                    ans = new SpecialAbilityAbility();
                    break;
                case AbilityType.ThrowGrip:
                    ans = new ThrowGrip();
                    break;
                case AbilityType.ZombieGrip:
                    ans = new ZombieGrip();
                    break;
                default:
                    throw new MagickaLoadException("WTF this should never happen!!! (Ability.cs)"); // lol
                    break; // OMEGALUL
            }

            ans.AbilityName = abilityName;
            ans.ReadAbilityData(reader, logger);
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Ability...");
            throw new MagickaWriteException("Cannot write base Ability class directly! Type is polymorphic.");
        }

        #endregion

        #region ProtectedMethods

        protected void ReadAbilityData(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Generic Ability Data...");

            this.Cooldown = reader.ReadSingle();
            this.Target = (Target)reader.ReadByte();
            this.HasFuzzyExpression = reader.ReadBoolean();
            if (this.HasFuzzyExpression)
                this.FuzzyExpression = reader.ReadString();
            this.NumAnimations = reader.ReadInt32();
            for (int i = 0; i < this.NumAnimations; ++i)
                this.AnimationKeys[i] = reader.ReadString();
        }

        protected void WriteAbilityData(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Generic Ability Data...");
            throw new NotImplementedException("Write Ability not implemented yet!");
        }

        #endregion
    }
}
