using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    public class BreakFree : AnimationAction
    {
        public float Magnitude { get; set; }
        public int Weapon { get; set; }

        public BreakFree()
        {
            this.Magnitude = 0.0f;
            this.Weapon = 0;
        }

        public BreakFree(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading BreakFree AnimationAction...");

            this.Magnitude = reader.ReadSingle();
            this.Weapon = reader.ReadInt32();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing BreakFree AnimationAction...");

            writer.Write(this.Magnitude);
            writer.Write(this.Weapon);
        }
    }
}
