using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    public class PlayEffect : AnimationAction
    {
        public string BoneBindPose { get; set; }
        public bool Attach { get; set; }
        public string Effect { get; set; }
        public float WTF { get; set; } // What is the purpose of this read? it's not even stored within a variable in Magicka's code, it's just a lost read operation??? left behind for an unused value? alignment purposes IN FILES??? WHAT?

        public PlayEffect()
        {
            this.BoneBindPose = string.Empty;
            this.Attach = false;
            this.Effect = string.Empty;
            this.WTF = 69.4203421f;
        }

        public PlayEffect(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading PlayEffect AnimationAction...");

            this.BoneBindPose = reader.ReadString();
            this.Attach = reader.ReadBoolean();
            this.Effect = reader.ReadString();
            this.WTF = reader.ReadSingle(); // Literally just conserving the value so that I can figure out wtf it does.
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing PlayEffect AnimationAction...");

            writer.Write(this.BoneBindPose);
            writer.Write(this.Attach);
            writer.Write(this.Effect);
            writer.Write(this.WTF); // WHAT THE FUCK DOES THIS DO???
        }
    }
}
