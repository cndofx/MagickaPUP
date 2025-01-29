using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    public class CameraShake : AnimationAction
    {
        public string Text { get; set; }
        public float Duration { get; set; }
        public float Magnitude { get; set; }

        public CameraShake()
        {
            this.Text = string.Empty;
            this.Duration = 0.0f;
            this.Magnitude = 0.0f;
        }

        public CameraShake(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading CameraShake AnimationAction...");

            this.Text = reader.ReadString();
            this.Duration = reader.ReadSingle();
            this.Magnitude = reader.ReadSingle();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing CameraShake AnimationAction...");

            writer.Write(this.Text);
            writer.Write(this.Duration);
            writer.Write(this.Magnitude);
        }
    }
}
