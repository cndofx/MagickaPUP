using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using MagickaPUP.Utility.Exceptions;
using MagickaPUP.MagickaClasses.Data;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    public class DamageGrip : AnimationAction
    {
        public bool DamageAffectsOwner { get; set; } // Maybe rename to "DamageAffectsOwner" or "DamageOwner" or "DamagesOwner" or whatever...
        public int NumDamages { get; set; } // Maybe get rid of this field? we don't really need it... I'm being pretty inconsistent regarding when I keep it and when I discard it tbh lol...
        public Damage[] Damages { get; set; } 

        public DamageGrip()
        {
            this.DamageAffectsOwner = false;
            this.NumDamages = 0;
            this.Damages = Array.Empty<Damage>();
        }

        public DamageGrip(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading DamageGrip AnimationAction...");

            this.DamageAffectsOwner = reader.ReadBoolean();
            this.NumDamages = reader.ReadInt32();
            if (this.NumDamages > 5)
                throw new MagickaLoadException($"Magicka does not support more than 5 damage entries for a DamageGrip! ({this.NumDamages} were found)"); // NOTE : This exception here might be kinda weird tbh... because this is on the XNB decompression step, so that would mean that we're converting into JSON an input XNB file that could come from the base game and be malformed, so do we really need to error here? Or would it make more sense to only error when packing a JSON file into an XNB file? Idk, not sure, who knows... we'll see.
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

            writer.Write(this.DamageAffectsOwner);

            if (this.NumDamages > 5)
                throw new MagickaWriteException($"Magicka does not support more than 5 damage entires for DamageGrip! ({this.NumDamages} were found)");
            writer.Write(this.NumDamages);

            foreach (var damage in this.Damages)
            {
                writer.Write((int)damage.AttackProperty);
                writer.Write((int)damage.Element);
                writer.Write(damage.Amount);
                writer.Write(damage.Magnitude);
            }
        }
    }
}
