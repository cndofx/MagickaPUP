using MagickaPUP.MagickaClasses.Effects;
using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class EffectAdditiveWriter : TypeReader<EffectAdditive>
    {
        public EffectAdditiveWriter() { }

        public override EffectAdditive Read(MBinaryReader reader, DebugLogger logger = null)
        {
            throw new NotImplementedException();
        }

        public override void Write(EffectAdditive instance, MBinaryWriter writer, DebugLogger logger = null)
        {
            throw new NotImplementedException();
        }
    }
}
