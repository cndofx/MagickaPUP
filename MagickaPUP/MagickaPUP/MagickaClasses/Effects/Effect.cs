using System;
using System.Collections.Generic;
using System.Text;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.IO;
using MagickaPUP.XnaClasses;
using System.Runtime.Remoting.Messaging;
using MagickaPUP.MagickaClasses.Areas;
using System.Text.Json.Serialization;

namespace MagickaPUP.MagickaClasses.Effects
{
    // TODO : Find a map with lava to get the text of the type reader that corresponds to lava
    // TODO : Remove outdated TODOs!!! lmao...
    [JsonDerivedType(typeof(Effect), typeDiscriminator: "effect_base")]
    [JsonDerivedType(typeof(EffectDeferred), typeDiscriminator: "effect_deferred")]
    [JsonDerivedType(typeof(EffectDeferredLiquid), typeDiscriminator: "effect_deferred_liquid")]
    [JsonDerivedType(typeof(EffectLava), typeDiscriminator: "effect_lava")]
    [JsonDerivedType(typeof(EffectAdditive), typeDiscriminator: "effect_additive")]
    public class Effect : XnaObject
    {
        #region Variables

        // Nothing to see here! lol...

        #endregion

        #region Constructor

        // Well, it almost feels like I'm legally obligated to make a constructor!
        public Effect()
        {}

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Effect...");

            throw new Exception("Base Effect type cannot be read! Type is polymorphic and a child type must be used!");
        }

        public static Effect Read(MBinaryReader reader, DebugLogger logger = null)
        {
            Effect ans = new Effect();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Effect...");

            throw new Exception("Base Effect type cannot be written! Type is polymorphic and a child type must be used!");
        }

        #endregion
    }
}
