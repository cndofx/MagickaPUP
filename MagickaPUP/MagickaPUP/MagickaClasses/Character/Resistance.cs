using MagickaPUP.IO;
using MagickaPUP.MagickaClasses.Data;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character
{
    // NOTE : Yes, this is NOT an XnaObject... tbh, all classes should be modified to stop this stupid inheritance and virtual/override bullshit. But that requires some massive rework at this point...
    public class Resistance
    {
        #region Variables

        public Elements elements { get; set; } // The element which this resistance is resistant against. Originally named "ResistanceAgainst".
        public float multiplier { get; set; }
        public float modifier { get; set; }
        public bool statusResistance { get; set; } // TODO : Figure out what this does. Probably determines whether you get a resistance against burning / cold / poisoned status effects (or any other status effect given by an element).

        #endregion

        #region Constructor

        public Resistance()
        {
            this.elements = Elements.None;
            this.multiplier = 1.0f;
            this.modifier = 0.0f;
            this.statusResistance = false;
        }

        #endregion
    }
}
