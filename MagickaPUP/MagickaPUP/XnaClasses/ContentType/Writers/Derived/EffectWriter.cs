using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.ContentType.Writers.Derived
{
    public class EffectWriter : TypeWriter<EffectCode>
    {
        public EffectWriter()
        { }

        public override void Write(EffectCode instance, MBinaryWriter writer, DebugLogger logger = null)
        {
            instance.WriteInstance(writer, logger);
        }
    }
}
