using MagickaPUP.MagickaClasses.Effects;
using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses.ContentType.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class EffectDeferredLiquidWriter : TypeWriter<EffectDeferredLiquid>
    {
        public EffectDeferredLiquidWriter()
        { }

        public override void Write(EffectDeferredLiquid instance, MBinaryWriter writer, DebugLogger logger = null)
        {
            instance.WriteInstance(writer, logger);
        }
    }
}
