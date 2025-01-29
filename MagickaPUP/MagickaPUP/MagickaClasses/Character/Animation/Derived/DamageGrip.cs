using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using MagickaPUP.Utility.Exceptions;
using MagickaPUP.MagickaClasses.Data;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    public class DamageGrip : AnimationAction
    {
        public bool DamageOwner { get; set; } // Maybe rename to "DamageAffectsOwner"
        public int NumDamages { get; set; } // Maybe get rid of this field? we don't really need it... I'm being pretty inconsistent regarding when I keep it and when I discard it tbh lol...
        public Damage[] Damages { get; set; } 

        public DamageGrip()
        {
            this.DamageOwner = false;
            this.NumDamages = 0;
            this.Damages = Array.Empty<Damage>();
        }

        public DamageGrip(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading DamageGrip AnimationAction...");

            this.DamageOwner = reader.ReadBoolean();
            this.NumDamages = reader.ReadInt32();
            if (this.NumDamages > 5)
                throw new MagickaLoadException($"Magicka does not support more than 5 damage entries for a DamageGrip! ({this.NumDamages} were found)");
            this.Damages = new Damage[this.NumDamages];
            for (int i = 0; i < this.NumDamages; ++i)
            {
                this.Damages[i] = new Damage
                {
                    AttackProperty = (AttackProperties)reader.ReadInt32(),
                    Element = (Elements)reader.ReadInt32(),
                    Amount = reader.ReadSingle(),
                    Magnitude = reader.ReadSingle()
                };
            }
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing DamageGrip AnimationAction...");
            throw new NotImplementedException("Write DamageGrip AnimationAction is not implemented yet!");
        }
    }
}
