using MagickaPUP.MagickaClasses.Map;
using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses.ContentType.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class LevelModelWriter : TypeWriter<LevelModel>
    {
        public LevelModelWriter()
        { }

        public override void Write(LevelModel instance, MBinaryWriter writer, DebugLogger logger = null)
        {
            throw new NotImplementedException();
        }
    }
}
