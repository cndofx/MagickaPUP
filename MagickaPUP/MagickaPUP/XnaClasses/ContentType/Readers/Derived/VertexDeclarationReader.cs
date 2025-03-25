using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class VertexDeclarationReader : TypeReader<VertexDeclaration>
    {
        public VertexDeclarationReader()
        { }

        public override VertexDeclaration Read(VertexDeclaration instance, MBinaryReader reader, DebugLogger logger = null)
        {
            VertexDeclaration ans = new VertexDeclaration();
            ans.ReadInstance(reader, logger);
            return ans;
        }
    }
}
