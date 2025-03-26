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
        #region Variables

        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }

        #endregion

        #region Constructor

        public Item()
        { }

        #endregion

        #region PublicMethods - Read and Write

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Item...");

            this.ItemID = reader.ReadString();
            this.ItemName = reader.ReadString();
            this.ItemDescription = reader.ReadString();

            throw new NotImplementedException("Read Item is not implemented yet!");
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Item...");

            throw new NotImplementedException("Write Item is not implemented yet!");
        }

        #endregion

        #region PublicMethods - Static

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

        #endregion
    }
}
