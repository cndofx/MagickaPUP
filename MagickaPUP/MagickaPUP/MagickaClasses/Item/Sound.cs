using MagickaPUP.MagickaClasses.Data.Audio;
using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Item
{
    public struct Sound
    {
        public string Name { get; set; }
        public Banks Banks { get; set; }

        public Sound(string name = default, Banks banks = default)
        {
            this.Name = name;
            this.Banks = banks;
        }

        public Sound(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Item Sound...");

            this.Name = reader.ReadString();
            this.Banks = (Banks)reader.ReadInt32();
        }

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Item Sound...");

            writer.Write(this.Name);
            writer.Write((int)this.Banks);
        }
    }
}
