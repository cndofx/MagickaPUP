using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    public class Footstep : AnimationAction
    {
        // Holds no data

        public Footstep()
        {
            // Does nothing
        }

        public Footstep(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Footstep AnimationAction...");
            // Reads no further data
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Footstep AnimationAction...");
            // Writes no further data
        }
    }
}
