using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    public class Immortal : AnimationAction
    {
        public bool Collide { get; set; }

        public Immortal()
        {
            this.Collide = false;
        }

        public Immortal(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Immortal AnimationAction...");

            this.Collide = reader.ReadBoolean();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Immortal AnimationAction...");

            writer.Write(this.Collide);
        }
    }
}
