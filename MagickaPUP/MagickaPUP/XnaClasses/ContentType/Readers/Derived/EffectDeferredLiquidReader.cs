using MagickaPUP.MagickaClasses.Effects;
using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class EffectDeferredLiquidReader : TypeReader<EffectDeferredLiquid>
    {
        public EffectDeferredLiquidReader()
        { }

        public override EffectDeferredLiquid Read(EffectDeferredLiquid instance, MBinaryReader reader, DebugLogger logger = null)
        {
            EffectDeferredLiquid ans = new EffectDeferredLiquid();
            ans.ReadInstance(reader, logger);
            return ans;
        }
    }
}
