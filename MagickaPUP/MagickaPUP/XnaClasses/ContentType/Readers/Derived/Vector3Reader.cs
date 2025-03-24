using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class Vector3Reader : TypeReader<Vec3>
    {
        public Vector3Reader()
        { }

        public override Vec3 Read(MBinaryReader reader, DebugLogger logger = null)
        {
            throw new NotImplementedException();
        }
    }
}
