using MagickaPUP.MagickaClasses.Item;
using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses.Specific;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.ContentType.Readers.Derived
{
    public class ItemReader : TypeReader<Item>
    {
        public ItemReader()
        { }

        public override Item Read(Item instance, MBinaryReader reader, DebugLogger logger = null)
        {
            return Item.Read(reader, logger);
        }
    }
}
