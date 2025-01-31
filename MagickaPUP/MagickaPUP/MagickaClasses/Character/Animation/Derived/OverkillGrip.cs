using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    public class OverkillGrip : AnimationAction
    {
        // Holds no data

        public OverkillGrip()
        {
            // Does nothing
        }

        public OverkillGrip(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading OverkillGrip AnimationAction...");
            // Reads no further data
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing OverkillGrip AnimationAction...");
            // Writes no further data
        }
    }
}
