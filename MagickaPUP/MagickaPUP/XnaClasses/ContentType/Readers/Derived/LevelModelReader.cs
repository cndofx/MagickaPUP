using MagickaPUP.MagickaClasses.Map;
using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class LevelModelReader : TypeReader<LevelModel>
    {
        public LevelModelReader()
        { }

        public override LevelModel Read(LevelModel instance, MBinaryReader reader, DebugLogger logger = null)
        {
            LevelModel ans = new LevelModel();
            ans.ReadInstance(reader, logger);
            return ans;
        }
    }
}
