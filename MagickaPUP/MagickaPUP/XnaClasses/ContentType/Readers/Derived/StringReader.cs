using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class StringReader : TypeReader<string>
    {
        public StringReader()
        { }

        public override string Read(string instance, MBinaryReader reader, DebugLogger logger = null)
        {
            string ans = reader.ReadString();
            return ans;
        }
    }
}
