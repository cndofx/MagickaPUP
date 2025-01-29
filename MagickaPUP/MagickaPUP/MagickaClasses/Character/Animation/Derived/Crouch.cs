using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    public class Crouch : AnimationAction
    {
        public float Radius { get; set; }
        public float Length { get; set; }

        public Crouch()
        {
            this.Radius = 1.0f;
            this.Length = 1.0f;
        }

        public Crouch(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Crouch AnimationAction...");

            this.Radius = reader.ReadSingle();
            this.Length = reader.ReadSingle();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Crouch AnimationAction...");

            writer.Write(this.Radius);
            writer.Write(this.Length);
        }
    }
}
