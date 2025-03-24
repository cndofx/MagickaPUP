using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific
{
    public abstract class TypeReader<T>
    {
        public TypeReader()
        { }

        public abstract T Read(MBinaryReader reader, DebugLogger logger = null);
    }
}
