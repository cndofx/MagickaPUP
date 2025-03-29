using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Item.SpecialAbilities
{
    public abstract class SpecialAbility
    {
        public SpecialAbility()
        { }

        public abstract void Write(MBinaryWriter writer, DebugLogger logger = null);
    }
}
