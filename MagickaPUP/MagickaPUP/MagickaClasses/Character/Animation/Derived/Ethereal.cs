using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    public class Ethereal : AnimationAction
    {
        public bool IsEthereal { get; set; }
        public float Alpha { get; set; }
        public float Speed { get; set; }

        public Ethereal()
        {
            this.IsEthereal = false;
            this.Alpha = 0.0f;
            this.Speed = 1.0f;
        }

        public Ethereal(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Ethereal AnimationAction...");

            this.IsEthereal = reader.ReadBoolean();
            this.Alpha = reader.ReadSingle();
            this.Speed = reader.ReadSingle();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Ethereal AnimationAction...");

            writer.Write(this.IsEthereal);
            writer.Write(this.Alpha);
            writer.Write(this.Speed);
        }
    }
}
