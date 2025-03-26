using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Item
{
    // NOTE : With the new object implementation, even classes that can be primary objects (like this one) don't really need to inherit from XnaObject.
    // As a matter of fact, XnaObject doesn't really even need to exist anymore... so yeah...
    // TODO : In the future, get rid of this XnaObject inheritance for code cleanup...
    public class Item : XnaObject
    {
        public Item()
        { }

        // TODO : Implement

        public static Item Read(MBinaryReader reader, DebugLogger logger = null)
        {
            Item ans = new Item();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public static void Write(Item item, MBinaryWriter writer, DebugLogger logger = null)
        {
            if(item != null)
                item.WriteInstance(writer, logger);
        }
    }
}
