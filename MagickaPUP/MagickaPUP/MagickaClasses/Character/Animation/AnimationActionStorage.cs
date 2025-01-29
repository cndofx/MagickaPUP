using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using MagickaPUP.Utility.Exceptions;
using MagickaPUP.MagickaClasses.Data;
using MagickaPUP.MagickaClasses.Character.Animation.Derived;

namespace MagickaPUP.MagickaClasses.Character.Animation
{
    public class AnimationActionStorage
    {
        #region Variables

        public string ActionType { get; set; }
        public float StartTime { get; set; }
        public float EndTime { get; set; }

        public AnimationAction AnimationAction { get; set; }

        #endregion

        #region Constructor

        public AnimationActionStorage()
        { }

        public AnimationActionStorage(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading AnimationActionStorage...");

            this.ActionType = reader.ReadString(); // TODO : Move this string out of this class or mark it as a non JSON serializable field, since it will be added again on the child AnimationAction's JSON object.
            this.StartTime = reader.ReadSingle();
            this.EndTime = reader.ReadSingle();

            logger?.Log(2, $" - AnimationActionType : {this.ActionType}");
            logger?.Log(2, $" - StartTime           : {this.StartTime}");
            logger?.Log(2, $" - EndTime             : {this.EndTime}");

            AnimationActionType type;
            bool success = Enum.TryParse<AnimationActionType>(this.ActionType, true, out type);
            if (!success)
                throw new MagickaLoadException($"Could not load the specified AnimationAction type. The type \"{this.ActionType}\" is not a valid AnimationAction for Magicka.");

            // wtf... I hate these chains I swear... such a fucking pain in the ass to write in C#
            switch (type)
            {
                case AnimationActionType.Block:
                    this.AnimationAction = new Block(reader, logger);
                    break;
                case AnimationActionType.BreakFree:
                    this.AnimationAction = new BreakFree(reader, logger);
                    break;
                case AnimationActionType.CameraShake:
                    this.AnimationAction = new CameraShake(reader, logger);
                    break;
                case AnimationActionType.CastSpell:
                    this.AnimationAction = new CastSpell(reader, logger);
                    break;
                case AnimationActionType.Crouch:
                    this.AnimationAction = new Crouch(reader, logger);
                    break;
                case AnimationActionType.DamageGrip:
                    this.AnimationAction = new DamageGrip(reader, logger);
                    break;
                case AnimationActionType.DealDamage:
                    this.AnimationAction = new DealDamage(reader, logger);
                    break;
                case AnimationActionType.DetachItem:
                    this.AnimationAction = new DetachItem(reader, logger);
                    break;
                case AnimationActionType.Ethereal:
                    this.AnimationAction = new Ethereal(reader, logger);
                    break;
                case AnimationActionType.Footstep:
                    this.AnimationAction = new Footstep(reader, logger);
                    break;
                case AnimationActionType.Grip:
                    this.AnimationAction = new Grip(reader, logger);
                    break;
                case AnimationActionType.Gunfire:
                    this.AnimationAction = new Gunfire(reader, logger);
                    break;
                case AnimationActionType.Immortal:
                    this.AnimationAction = new Immortal(reader, logger);
                    break;
                case AnimationActionType.Invisible:
                    this.AnimationAction = new Invisible(reader, logger);
                    break;
                case AnimationActionType.Jump:
                    this.AnimationAction = new Jump(reader, logger);
                    break;
                case AnimationActionType.Move:
                    this.AnimationAction = new Move(reader, logger);
                    break;
                case AnimationActionType.OverkillGrip:
                    this.AnimationAction = new OverkillGrip(reader, logger);
                    break;
                case AnimationActionType.PlayEffect:
                    this.AnimationAction = new PlayEffect(reader, logger);
                    break;
                case AnimationActionType.PlaySound:
                    this.AnimationAction = new PlaySound(reader, logger);
                    break;
                case AnimationActionType.ReleaseGrip:
                    this.AnimationAction = new ReleaseGrip(reader, logger);
                    break;
                case AnimationActionType.RemoveStatus:
                    this.AnimationAction = new RemoveStatus(reader, logger);
                    break;
                case AnimationActionType.SetItemAttach:
                    this.AnimationAction = new SetItemAttach(reader, logger);
                    break;
                case AnimationActionType.SpawnMissile:
                    this.AnimationAction = new SpawnMissile(reader, logger);
                    break;
                case AnimationActionType.SpecialAbility:
                    this.AnimationAction = new SpecialAbility(reader, logger);
                    break;
                case AnimationActionType.Suicide: // This is what I want to do right now :D
                    this.AnimationAction = new Suicide(reader, logger);
                    break;
                case AnimationActionType.ThrowGrip:
                    this.AnimationAction = new ThrowGrip(reader, logger);
                    break;
                case AnimationActionType.Tongue:
                    this.AnimationAction = new Tongue(reader, logger);
                    break;
                case AnimationActionType.WeaponVisibility:
                    this.AnimationAction = new WeaponVisibility(reader, logger);
                    break;
            }
        }

        #endregion

        #region PublicMethods

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing AnimationActionStorage...");

            writer.Write(this.ActionType);
            writer.Write(this.StartTime);
            writer.Write(this.EndTime);
            this.AnimationAction.Write(writer, logger);
        }

        #endregion
    }
}
