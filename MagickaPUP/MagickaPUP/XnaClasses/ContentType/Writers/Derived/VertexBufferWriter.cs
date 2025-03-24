
using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses.ContentType.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class VertexBufferWriter : TypeWriter<VertexBuffer>
    {
        public VertexBufferWriter()
        { }

        public override void Write(VertexBuffer instance, MBinaryWriter writer, DebugLogger logger = null)
        {
            throw new NotImplementedException();
        }
    }
}
