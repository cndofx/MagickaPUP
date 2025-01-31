using MagickaPUP.Utility.IO;
using MagickaPUP.MagickaClasses.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    public class PlaySound : AnimationAction
    {
        public string Sound { get; set; }
        public Banks Bank { get; set; }

        public PlaySound()
        {
            this.Sound = string.Empty;
            this.Bank = default(Banks);
        }

        public PlaySound(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading PlaySound AnimationAction...");

            this.Sound = reader.ReadString();
            this.Bank = (Banks)reader.ReadInt32();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing PlaySound AnimationAction...");

            writer.Write(this.Sound);
            writer.Write((int)this.Bank);
        }
    }
}
