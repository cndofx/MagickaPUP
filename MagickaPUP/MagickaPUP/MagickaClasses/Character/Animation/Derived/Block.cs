using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    public class Block : AnimationAction
    {
        public int Weapon { get; set; }

        public Block()
        {
            this.Weapon = 0;
        }

        public Block(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Block AnimationAction...");

            this.Weapon = reader.ReadInt32();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing BlockAnimationAction...");

            writer.Write(this.Weapon);
        }
    }
}
