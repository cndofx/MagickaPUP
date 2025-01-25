using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Events
{
    [Flags]
    public enum EventConditionType : byte
    {
        Default = 1,
        Hit = 2,
        Collision = 4,
        Damaged = 8,
        Timer = 16,
        Death = 32,
        OverKill = 64
    }
}
