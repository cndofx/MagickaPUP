
using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class VertexBufferReader : TypeReader<VertexBuffer>
    {
        public VertexBufferReader()
        { }

        public override VertexBuffer Read(VertexBuffer instance, MBinaryReader reader, DebugLogger logger = null)
        {
            VertexBuffer ans = new VertexBuffer();
            ans.ReadInstance(reader, logger);
            return ans;
        }
    }
}
