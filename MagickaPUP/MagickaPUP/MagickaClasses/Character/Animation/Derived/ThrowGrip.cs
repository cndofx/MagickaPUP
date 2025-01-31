using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    public class ThrowGrip : AnimationAction
    {
        // No fucking data to be seen here!

        public ThrowGrip()
        {
            // Do nothing!
        }

        public ThrowGrip(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading ThrowGrip AnimationAction...");
            // Nothing to see here, no data to be read, so do fuck all!
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing ThrowGrip AnimationAction...");
            // Nothing to see here, no data to be written, so do fuck all!
        }
    }
}
