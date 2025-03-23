using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific
{
    // Base class dedicated toward implementing the read and write methods for each class.
    // Yes, the fucking name sucks since it does both reading and writing, but this is one of this things inherited from the nomenclature of the XNA framework...
    public abstract class TypeReader<T>
    {
        public abstract void Write(MBinaryWriter writer, DebugLogger logger = null);
        public abstract T Read(MBinaryReader reader, DebugLogger logger = null);
    }
}
