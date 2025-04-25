using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses
{
    #region Comment - EffectCode

    // NOTE : This class is the equivalent of the base Effect class in XNA.
    // Yes, I know we already have the other Effect base class within the magicka classes, but due to how the effect reading works, this patchy workaround
    // is just faster to deal with by having a different separate class. Maybe I'll refactor this in the future to actually do things the right way, but for now,
    // this is good enough, and the final effect (not as in the effect class but as in the result of the code execution lol) is going to be the same anyway, so yeah...
    // Basically, the other Effect class is for specific Effect config / data readers (materials / MaterialEffect maybe could be a good name to replace the other class?)
    // and this class is the one that holds the actual shader code of the material effect.

    #endregion
    public class EffectCode : XnaObject
    {
        int CodeByteCount { get; set; }
        byte[] CodeByteBuffer { get; set; }

        public EffectCode()
        {
            this.CodeByteCount = 0;
            this.CodeByteBuffer = Array.Empty<byte>();
        }

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Effect Shader Code...");
            throw new NotImplementedException("Read EffectCode is not implemented yet!");
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Effect Shader Code...");
            throw new NotImplementedException("Write EffectCode is not implemented yet!");
        }
    }
}
