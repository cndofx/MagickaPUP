using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Data
{
    [Flags]
    public enum StatusEffects : short
    {
        None = 0,
        Burning = 1,
        Wet = 2,
        Frozen = 4,
        Cold = 8,
        Poisoned = 16,
        Healing = 32,
        Life = 32,
        Greased = 64,
        Steamed = 128,
        Bleeding = 256,
        NumberOfTypes = 512
    }
}
