using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class ListReader<T> : TypeReader<List<T>>
    {
        public ListReader()
        { }

        public override List<T> Read(MBinaryReader reader, DebugLogger logger = null)
        {
            throw new NotImplementedException();
        }

        public override void Write(List<T> instance, MBinaryWriter writer, DebugLogger logger = null)
        {
            throw new NotImplementedException();
        }
    }
}
