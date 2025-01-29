using MagickaPUP.IO;
using System;
using System.Collections.Generic;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    public class SetItemAttach : AnimationAction
    {
        public int Item { get; set; }
        public string JointBone { get; set; }

        public SetItemAttach()
        {
            this.Item = 0;
            this.JointBone = string.Empty;
        }

        public SetItemAttach(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading SetItemAttach AnimationAction...");

            this.Item = reader.ReadInt32();
            this.JointBone = reader.ReadString();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing SetItemAttach AnimationAction...");

            writer.Write(this.Item);
            writer.Write(this.JointBone);
        }
    }
}
