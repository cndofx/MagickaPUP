using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Data
{
    public enum BloodType : int // mark it as int since we have to read it as an i32
    {
        Regular,
        Green,
        Black,
        Wood,
        Insect,
        None
    }
}
