using MagickaPUP.MagickaClasses.Effects;
using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class EffectDeferredReader : TypeReader<EffectDeferred>
    {
        public EffectDeferredReader()
        { }

        public override EffectDeferred Read(EffectDeferred instance, MBinaryReader reader, DebugLogger logger = null)
        {
            EffectDeferred ans = new EffectDeferred();
            ans.ReadInstance(reader, logger);
            return ans;
        }
    }
}
