using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Data
{
    [Flags]
    public enum DamageTargets : byte
    {
        None = 0,
        Target = 0,
        Friendly = 1,
        Enemy = 2,
        NonCharacters = 4,
        All = 255
    }
}
