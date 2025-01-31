using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using MagickaPUP.MagickaClasses.Data;

namespace MagickaPUP.MagickaClasses.Character.Buffs.Derived
{
    public class BuffResistance : Buff
    {
        public Resistance Resistance { get; set; }

        public BuffResistance()
        {
            this.Resistance = new Resistance();
        }

        public BuffResistance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading BuffResistance...");

            this.Resistance = new Resistance(reader, logger);
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing BuffResistance...");

            this.Resistance.Write(writer, logger);
        }
    }
}
