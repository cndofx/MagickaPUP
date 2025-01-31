using MagickaPUP.MagickaClasses.Data;
using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.PhysicsEntities
{
    // NOTE : This class is within Magicka's code the same as the Resistance class used for characters. In mpup's codebase tho they are 2 different structs,
    // because the reading process is different for each, and when using the automatic JSON serialization from JsonSerializer, the serialized JSON files would
    // have the extra boolean field that goes unused in the case of PhysicsEntities.
    public class Resistance
    {
        public Elements Elements { get; set; }
        public float Multiplier { get; set; }
        public float Modifier { get; set; }

        public Resistance()
        {
            this.Elements = default(Elements);
            this.Multiplier = 1.0f;
            this.Modifier = 0.0f;
        }

        public Resistance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Resistance...");

            this.Elements = (Elements)reader.ReadInt32();
            this.Multiplier = reader.ReadSingle();
            this.Modifier = reader.ReadSingle();
        }

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Resistance...");

            writer.Write((int)this.Elements);
            writer.Write(this.Multiplier);
            writer.Write(this.Modifier);
        }
    }
}
