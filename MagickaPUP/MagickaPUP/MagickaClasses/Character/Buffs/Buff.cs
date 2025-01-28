using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Buffs
{
    public abstract class Buff
    {
        public abstract void Write(MBinaryWriter writer, DebugLogger logger = null);
    }
}
