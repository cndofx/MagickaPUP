using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses
{
    public class EffectMaterial : XnaObject
    {
        public EffectMaterial()
        { }

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading EffectMaterial...");
            throw new NotImplementedException("Read EffectMaterial is not implemented yet!");
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing EffectMaterial...");
            throw new NotImplementedException("Write EffectMaterial is not implemented yet!");
        }
    }
}
