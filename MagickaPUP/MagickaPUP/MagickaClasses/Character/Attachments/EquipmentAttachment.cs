using MagickaPUP.Utility.IO;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Attachments
{
    public class EquipmentAttachment : XnaObject
    {
        #region Variables

        public int Slot { get; set; }
        public string BoneName { get; set; }
        public Vec3 Rotation { get; set; }
        public string ItemName { get; set; }

        #endregion

        #region Constructor

        public EquipmentAttachment()
        {
            this.Slot = 0;
            this.BoneName = default;
            this.Rotation = new Vec3();
            this.ItemName = default;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading EquipmentAttachment...");

            this.Slot = reader.ReadInt32();
            this.BoneName = reader.ReadString();
            this.Rotation = Vec3.Read(reader, null);
            this.ItemName = reader.ReadString();
        }

        public static EquipmentAttachment Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new EquipmentAttachment();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing EquipmentAttachment...");

            writer.Write(this.Slot);
            writer.Write(this.BoneName);
            this.Rotation.WriteInstance(writer, null);
            writer.Write(this.ItemName);
        }

        #endregion
    }
}
