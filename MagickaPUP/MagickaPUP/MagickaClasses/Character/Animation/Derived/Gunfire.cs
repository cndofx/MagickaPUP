using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    public class Gunfire : AnimationAction
    {
        public int Weapon { get; set; }
        public float Accuracy { get; set; }

        public Gunfire()
        {
            this.Weapon = 0;
            this.Accuracy = 1.0f;
        }

        public Gunfire(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Gunfire AnimationAction...");

            this.Weapon = reader.ReadInt32();
            this.Accuracy = reader.ReadSingle();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Gunfire AnimationAction...");

            writer.Write(this.Weapon);
            writer.Write(this.Accuracy);
        }
    }
}
