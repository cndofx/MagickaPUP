using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    public class Invisible : AnimationAction
    {
        public bool NoEffect { get; set; }

        public Invisible()
        {
            this.NoEffect = false;
        }

        public Invisible(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Invisible AnimationAction...");

            this.NoEffect = reader.ReadBoolean();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Invisible AnimationAction...");

            writer.Write(this.NoEffect);
        }
    }
}
