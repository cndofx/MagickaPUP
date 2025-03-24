using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class StringWriter : TypeReader<string>
    {
        public StringWriter()
        { }

        public override string Read(MBinaryReader reader, DebugLogger logger = null)
        {
            throw new NotImplementedException();
        }

        public override void Write(string instance, MBinaryWriter writer, DebugLogger logger = null)
        {
            throw new NotImplementedException();
        }
    }
}
