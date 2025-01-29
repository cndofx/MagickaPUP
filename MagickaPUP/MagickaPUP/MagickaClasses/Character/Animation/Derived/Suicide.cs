using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    public class Suicide : AnimationAction
    {
        public bool Overkill { get; set; }

        public Suicide()
        {
            this.Overkill = false;
        }

        public Suicide(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Suicide AnimationAction...");

            this.Overkill = reader.ReadBoolean();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Suicide AnimationAction...");

            writer.Write(this.Overkill);
        }
    }
}
