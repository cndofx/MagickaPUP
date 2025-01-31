using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagickaPUP.MagickaClasses.Effects;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace MagickaPUP.MagickaClasses.Liquids
{
    [JsonDerivedType(typeof(Liquid), typeDiscriminator: "liquid_base")]
    [JsonDerivedType(typeof(Water), typeDiscriminator: "liquid_water")]
    [JsonDerivedType(typeof(Lava), typeDiscriminator: "liquid_lava")]
    public class Liquid : XnaObject
    {
        #region Variables

        public Effect effect { get; set; }

        #endregion

        #region Constructor

        public Liquid()
        { }

        #endregion

        #region PublicMethods

        /*
            README : This is a fucking mess caused by the way Magicka's code handles Liquid polymorphism and due to the fact that you can't have virtual static methods in C#
            This could be somewhat fixed if instead, Liquid contained a member variable of some hypothetical LiquidType class, and Water and Lava inherit from that class instead.
            For now, we're going the patchy way because I really can't come up with a better way when using this weird language that is C#.
            
            Another solution would be to scrap the current ReadInstance() and instead, rather than having a static method, make it so that we have a Read() virtual method that
            returns an XnaObject. Then, no matter what type we return or try to assign this to, since it's an XnaObject, it should always compile.

            Then, on XnaObject, have a static method with XnaObject return, that is the actual "public" static Read() method. And if you want, add a static Read() with the
            correct return type for every specific class, as we already have, only that this time it reads the "new" "ReadInstance" implementation that, rather than being void, returns
            the created object... could this work? idk... for now we'll make do with what we have.

            This is all just a consequence of trying to reverse Magicka's already terrible code, and sadly, as a consequence, inheriting it's terrible code structure...
            Hopefully, a rewrite will clean this all up once I have this figured out... or not...
            
            At the end of the day, this doesn't matter all that much if we think about the way Effect is implemented... I mean, it's "cleaner", but yeah...
        */

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Liquid...");

            throw new Exception("Base Liquid type cannot be read! Type is polymorphic and a child type must be used!");
        }

        public static Liquid Read(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Liquid...");

            Liquid ans = new Liquid();
            
            Effect effect = XnaObject.ReadObject<Effect>(reader, logger);

            if (effect is EffectDeferredLiquid)
            {
                ans = new Water();
            }
            else
            if (effect is EffectLava)
            {
                ans = new Lava();
            }
            else
            {
                throw new NotImplementedException("Requested Liquid Type is not implemented or is unknown!");
            }

            ans.effect = effect; // store the obtained effect within this liquid instance for future use.

            ans.ReadInstance(reader, logger); // read the created object before returning it.

            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Liquid...");

            XnaObject.WriteObject(this.effect, writer, logger);
            // this.effect.WriteInstance(writer, logger);

            // throw new Exception("Base Liquid type cannot be written! Type is polymorphic and a child type must be used!");
        }

        #endregion
    }
}
