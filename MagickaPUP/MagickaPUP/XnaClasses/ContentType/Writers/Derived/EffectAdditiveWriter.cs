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
    public class EffectAdditiveWriter : TypeWriter<EffectAdditive>
    {
        public EffectAdditiveWriter() { }

        public override void Write(EffectAdditive instance, MBinaryWriter writer, DebugLogger logger = null)
        {
            throw new NotImplementedException();
        }
    }
}
