using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Data
{
    [Flags]
    public enum MovementProperties
    {
        Default = 0,
        Water = 1,
        Jump = 2,
        Fly = 4,
        Dynamic = 128,
        All = 255
    }
}
