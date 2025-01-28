using MagickaPUP.IO;
using MagickaPUP.MagickaClasses.Character.Animation;
using MagickaPUP.MagickaClasses.Data;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character
{
    public class MovementData
    {
        public MovementProperties MovementProperties { get; set; }
        public string[] Animations { get; set; }

        public MovementData()
        {
            this.MovementProperties = default(MovementProperties);
            this.Animations = Array.Empty<string>();
        }

        public MovementData(MBinaryReader reader, DebugLogger logger = null)
        {
            this.MovementProperties = (MovementProperties)reader.ReadByte();
            this.Animations = new string[reader.ReadInt32()];
            for (int i = 0; i < this.Animations.Length; ++i)
                this.Animations[i] = reader.ReadString();
        }

        // TODO : Implement writing logic...

    }
}
