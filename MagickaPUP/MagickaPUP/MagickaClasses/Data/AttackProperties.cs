using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Data
{
    [Flags]
    public enum AttackProperties : short
    {
        Damage = 1,
        Knockdown = 2,
        Pushed = 4,
        Knockback = 6,
        Piercing = 8,
        ArmourPiercing = 16,
        Status = 32,
        Entanglement = 64,
        Stun = 128,
        Bleed = 256,
        NumberOfTypes = 512
    }
}
