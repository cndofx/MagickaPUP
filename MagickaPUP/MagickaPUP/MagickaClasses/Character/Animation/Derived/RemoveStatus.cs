using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using MagickaPUP.Utility.Exceptions;
using MagickaPUP.MagickaClasses.Data;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    // NOTE : We throw exceptions in the case that the string for the status effect is not a valid status effect found within Magicka's code.
    public class RemoveStatus : AnimationAction
    {
        public string StatusEffect { get; set; }

        public RemoveStatus()
        {
            this.StatusEffect = string.Empty;
        }

        public RemoveStatus(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading RemoveStatus AnimationAction...");

            this.StatusEffect = reader.ReadString();
            if (!CheckStatusEffect(this.StatusEffect)) // NOTE : This is kinda worthless since I think it would be logical to trust that a packed XNB file should theoretically hold a valid value, so there should be no reason to check this but still... I'll leave it in for now... just in case, I suppose.
                throw new MagickaLoadException($"The specified status effect \"{this.StatusEffect}\" does not exist!");
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing RemoveStatus AnimationAction...");

            if (!CheckStatusEffect(this.StatusEffect))
                throw new MagickaLoadException($"The specified status effect \"{this.StatusEffect}\" does not exist!");
            writer.Write(this.StatusEffect);
        }

        private bool CheckStatusEffect(string str)
        {
            StatusEffects effect;
            bool ans = Enum.TryParse<StatusEffects>(str, true, out effect);
            return ans;
        }
    }
}
