using MagickaPUP.IO;
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

            this.Resistance = new Resistance
            {
                elements = (Elements)reader.ReadInt32(),
                modifier = reader.ReadSingle(),
                multiplier = reader.ReadSingle(),
                statusResistance = reader.ReadBoolean()
            };
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing BuffResistance...");

            writer.Write((int)this.Resistance.elements);
            writer.Write((float)this.Resistance.modifier);
            writer.Write((float)this.Resistance.multiplier);
            writer.Write((bool)this.Resistance.statusResistance);
        }
    }
}
