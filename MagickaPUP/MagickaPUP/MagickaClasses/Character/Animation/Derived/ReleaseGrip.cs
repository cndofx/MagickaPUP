using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    public class ReleaseGrip : AnimationAction
    {
        // Holds no data

        public ReleaseGrip()
        {
            // Does nothing
        }

        public ReleaseGrip(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading ReleaseGrip AnimationAction...");
            // Reads no further data
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing ReleaseGrip AnimationAction...");
            // Writes no further data
        }
    }
}
