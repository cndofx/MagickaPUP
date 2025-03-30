using MagickaPUP.MagickaClasses.Item;
using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses.Specific;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.ContentType.Writers.Derived
{
    public class ItemWriter : TypeWriter<Item>
    {
        public ItemWriter()
        { }

        public override void Write(Item instance, MBinaryWriter writer, DebugLogger logger = null)
        {
            Item.Write(instance, writer, logger);
        }
    }
}
