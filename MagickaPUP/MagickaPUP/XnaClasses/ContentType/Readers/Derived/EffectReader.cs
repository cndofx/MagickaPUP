using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses.Specific;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.ContentType.Readers.Derived
{
    public class EffectReader : TypeReader<EffectCode>
    {
        public EffectReader()
        { }

        public override EffectCode Read(EffectCode instance, MBinaryReader reader, DebugLogger logger = null)
        {
            EffectCode ans = new EffectCode();
            ans.ReadInstance(reader, logger);
            return ans;
        }
    }
}
