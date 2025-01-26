using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Data
{
    // This enum contains flags to set what action an NPC is supposed to react to.
    [Flags]
    public enum ReactTo : byte
    {
        None = 0,
        Attack = 1,
        Proximity = 2
    }
}
