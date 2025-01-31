using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Buffs.Derived
{
    // I still can't figure out why the Magicka devs added a reader for this fucker, but here it is! for consistency sake I suppose. Just as I'm doing now lol...
    public class BuffUndying : Buff
    {
        public BuffUndying()
        {
            // Do nothing since this class contains no data...
        }

        public BuffUndying(MBinaryReader reader, DebugLogger logger = null)
        {
            // Do nothing since this class contains no data...
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            // Do nothing since this class contains no data...
        }
    }
}
