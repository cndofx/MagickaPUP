using MagickaPUP.MagickaClasses.Data;
using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Item.SpecialAbilities
{
    public class SpecialAbility
    {
        public string Type { get; set; }
        public string Animation { get; set; }
        public string Hash { get; set; }
        public Elements[] Elements { get; set; }

        public SpecialAbility()
        {
            this.Type = default;
            this.Animation = default;
            this.Hash = default;
        }

        public SpecialAbility(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Special Ability...");

            this.Type = reader.ReadString();
            this.Animation = reader.ReadString();
            this.Hash= reader.ReadString();

            int numElements = reader.ReadInt32();
            logger?.Log(1, $" - Num Elements : {numElements}");
            this.Elements = new Elements[numElements];
            for(int i = 0; i < numElements; ++i)
                this.Elements[i] = (Elements)reader.ReadInt32();
        }

        public static void Write(SpecialAbility instance, MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Special Ability...");

            throw new NotImplementedException("Write SpecialAbility is not implemented yet!");
        }
    }
}
