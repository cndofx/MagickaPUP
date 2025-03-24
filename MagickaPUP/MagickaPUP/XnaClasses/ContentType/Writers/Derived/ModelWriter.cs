using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class ModelWriter : TypeReader<Model>
    {
        public ModelWriter()
        { }

        public override Model Read(MBinaryReader reader, DebugLogger logger = null)
        {
            throw new NotImplementedException();
        }

        public override void Write(Model instance, MBinaryWriter writer, DebugLogger logger = null)
        {
            throw new NotImplementedException();
        }
    }
}
