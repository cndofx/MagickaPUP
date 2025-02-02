using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.PhysicsEntities
{
    public class BoundingBox
    {
        public string ID { get; set; }
        public Vec3 Position { get; set; }
        public Vec3 Scale { get; set; }
        public Quaternion Rotation { get; set; }

        public BoundingBox()
        {
            this.ID = string.Empty;
            this.Position = new Vec3();
            this.Scale = new Vec3(1.0f, 1.0f, 1.0f);
            this.Rotation = new Quaternion();
        }

        public BoundingBox(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading BoundingBox...");

            this.ID = reader.ReadString();
            this.Position = Vec3.Read(reader, logger);
            this.Scale = Vec3.Read(reader, logger);
            this.Rotation = Quaternion.Read(reader, logger);
        }

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing BoundingBox...");

            writer.Write(this.ID);
            this.Position.WriteInstance(writer, logger);
            this.Scale.WriteInstance(writer, logger);
            this.Rotation.WriteInstance(writer, logger);
        }
    }
}
