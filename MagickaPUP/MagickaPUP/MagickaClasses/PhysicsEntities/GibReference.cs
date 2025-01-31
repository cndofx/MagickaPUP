using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.PhysicsEntities
{
    // NOTE : This class is pretty much identical to the GibReference class used by CharacterTemplate.
    // I created it again for similar reasons to the Resistance class, but this time around there is no difference so whatever...
    public class GibReference
    {
        public string Model { get; set; } // External Reference
        public float Mass { get; set; }
        public float Scale { get; set; }

        public GibReference()
        {
            this.Model = string.Empty;
            this.Mass = 1.0f;
            this.Scale = 1.0f;
        }

        public GibReference(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading GibReference...");

            this.Model = reader.ReadString(); // ER
            this.Mass = reader.ReadSingle();
            this.Scale = reader.ReadSingle();
        }

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing GibReference...");

            writer.Write(this.Model);
            writer.Write(this.Mass);
            writer.Write(this.Scale);
        }
    }
}
