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
    public class Resistance : XnaObject
    {
        #region Variables

        public Elements element { get; set; } // The element which this resistance is resistant against. Originally named "ResistanceAgainst".
        public float multiplier { get; set; }
        public float modifier { get; set; }
        public bool statusResistance { get; set; } // TODO : Figure out what this does. Probably determines whether you get a resistance against burning / cold / poisoned status effects (or any other status effect given by an element).

        #endregion

        #region Constructor

        public Resistance()
        {
            this.element = Elements.None;
            this.multiplier = 1.0f;
            this.modifier = 0.0f;
            this.statusResistance = false;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            base.ReadInstance(reader, logger);
        }

        public static Resistance Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new Resistance();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            base.WriteInstance(writer, logger);
        }

        #endregion
    }
}
