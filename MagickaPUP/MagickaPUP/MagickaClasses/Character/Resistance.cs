using MagickaPUP.Utility.IO;
using MagickaPUP.MagickaClasses.Data;
using System;
using System.Collections.Generic;

namespace MagickaPUP.MagickaClasses.Character
{
    // NOTE : Yes, this is NOT an XnaObject... tbh, all classes should be modified to stop this stupid inheritance and virtual/override bullshit. But that requires some massive rework at this point...
    public class Resistance
    {
        #region Variables

        public Elements Elements { get; set; } // The element which this resistance is resistant against. Originally named "ResistanceAgainst".
        public float Multiplier { get; set; }
        public float Modifier { get; set; }
        public bool StatusResistance { get; set; } // TODO : Figure out what this does. Probably determines whether you get a resistance against burning / cold / poisoned status effects (or any other status effect given by an element).

        #endregion

        #region Constructor

        public Resistance()
        {
            this.Elements = Elements.None;
            this.Multiplier = 1.0f;
            this.Modifier = 0.0f;
            this.StatusResistance = false;
        }

        public Resistance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Resistance...");

            this.Elements = (Elements)reader.ReadInt32();
            this.Multiplier = reader.ReadSingle();
            this.Modifier = reader.ReadSingle();
            this.StatusResistance = reader.ReadBoolean();
        }

        #endregion

        #region PublicMethods

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Resistance...");

            writer.Write((int)this.Elements);
            writer.Write(this.Multiplier);
            writer.Write(this.Modifier);
            writer.Write(this.StatusResistance);
        }

        #endregion

        // NOTE : Actually, we could reimplement (again lol) this class as an XnaObject class and then make it so that on the CharacterTemplate class we call Read()
        // and then access resistance.elements to get the index, rather than reading within the character template class itself.
    }
}
