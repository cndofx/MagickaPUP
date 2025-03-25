using MagickaPUP.MagickaClasses.Effects;
using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class EffectAdditiveReader : TypeReader<EffectAdditive>
    {
        public EffectAdditiveReader() { }

        public override EffectAdditive Read(EffectAdditive instance, MBinaryReader reader, DebugLogger logger = null)
        {
            EffectAdditive ans = new EffectAdditive();
            ans.ReadInstance(reader, logger);
            return ans;
        }
    }
}
