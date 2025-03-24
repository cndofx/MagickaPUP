using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class IndexBufferReader : TypeReader<IndexBuffer>
    {
        public IndexBufferReader()
        { }

        public override IndexBuffer Read(MBinaryReader reader, DebugLogger logger = null)
        {
            throw new NotImplementedException();
        }
    }
}
