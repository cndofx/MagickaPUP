using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class Texture2DReader : TypeReader<Texture2D>
    {
        public Texture2DReader()
        { }

        public override Texture2D Read(Texture2D instance, MBinaryReader reader, DebugLogger logger = null)
        {
            Texture2D ans = new Texture2D();
            ans.ReadInstance(reader, logger);
            return ans;
        }
    }
}
