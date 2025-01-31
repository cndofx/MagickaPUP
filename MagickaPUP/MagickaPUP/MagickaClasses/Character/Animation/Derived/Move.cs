using MagickaPUP.Utility.IO;
using MagickaPUP.MagickaClasses.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    public class Move : AnimationAction
    {
        public Vec3 Velocity { get; set; }

        public Move()
        {
            this.Velocity = new Vec3();
        }

        public Move(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Move AnimationAction...");

            this.Velocity = Vec3.Read(reader, logger);
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Move AnimationAction...");

            this.Velocity.WriteInstance(writer, logger);
        }
    }
}
