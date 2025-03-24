using MagickaPUP.MagickaClasses.Effects;
using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class EffectLavaReader : TypeReader<EffectLava>
    {
        public EffectLavaReader()
        { }

        public override EffectLava Read(EffectLava instance, MBinaryReader reader, DebugLogger logger = null)
        {
            throw new NotImplementedException();
        }
    }
}
