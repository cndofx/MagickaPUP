using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Data
{
    public enum Target : byte
    {
        Self = 1,
        S = 1,
        Enemy = 2,
        E = 2,
        Friendly = 3,
        F = 3
    }
}
