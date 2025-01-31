using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Aura
{
    public abstract class Aura
    {
        public Aura()
        { }

        public abstract void Write(MBinaryWriter writer, DebugLogger logger = null);
    }
}
