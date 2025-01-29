using MagickaPUP.IO;
using MagickaPUP.MagickaClasses.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    public class DetachItem : AnimationAction
    {
        public int Item { get; set; }
        public Vec3 Velocity { get; set; }

        public DetachItem()
        {
            this.Item = 0;
            this.Velocity = new Vec3();
        }

        public DetachItem(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading DetachItem AnimationAction...");

            this.Item = reader.ReadInt32();
            this.Velocity = Vec3.Read(reader, logger);
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing DetachItem AnimationAction...");

            writer.Write(this.Item);
            this.Velocity.WriteInstance(writer, logger);
        }
    }
}
