using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    // WTF Did we suddenly switch games from Magicka into fucking L4D2? wtf is this? I seriously cannot think of any moments where there were any "tongues" in the game...
    public class Tongue : AnimationAction
    {
        public float MaxLength { get; set; } // Ok, hear me out, what the fuck happens if you set this to infinite? and a negative value? lol...

        public Tongue()
        {
            this.MaxLength = 1.0f;
        }

        public Tongue(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Tongue AnimationAction...");

            this.MaxLength = reader.ReadSingle();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Tongue AnimationAction...");

            writer.Write(this.MaxLength);
        }
    }
}
