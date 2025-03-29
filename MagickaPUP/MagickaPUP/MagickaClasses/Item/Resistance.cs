using MagickaPUP.MagickaClasses.Data;
using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Item
{
    // Resistances for Items
    // NOTE : Once again, this struct is duplicated, because each object reads whatever the fuck it wants, and we want to simplify the JSON reading process as much as possible, so yeah...
    public struct Resistance
    {
        public Elements ResistanceAgainst { get; set; }
        public float Multiplier { get; set; }
        public float Modifier { get; set; }
        public bool StatusResistance { get; set; }

        public Resistance(Elements resitanceAgainst = default, float multiplier = 1.0f, float modifier = 0.0f, bool statusResistance = false)
        {
            this.ResistanceAgainst = resitanceAgainst;
            this.Multiplier = multiplier;
            this.Modifier = modifier;
            this.StatusResistance = statusResistance;
        }

        public Resistance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Item Resistance...");

            this.ResistanceAgainst = (Elements)reader.ReadInt32();
            this.Multiplier = reader.ReadSingle();
            this.Modifier = reader.ReadSingle();
            this.StatusResistance = reader.ReadBoolean();
        }

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Item Resistance...");

            writer.Write((int)this.ResistanceAgainst);
            writer.Write(this.Multiplier);
            writer.Write(this.Modifier);
            writer.Write(this.StatusResistance);
        }
    }
}
