using MagickaPUP.IO;
using MagickaPUP.MagickaClasses.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    public class Grip : AnimationAction
    {
        public GripType GripType { get; set; }
        public float Radius { get; set; }
        public float BreakFreeTolerance { get; set; }
        public string AttachmentBoneGrip { get; set; }
        public string AttachmentBoneTarget { get; set; }
        public bool FinishOnGrip { get; set; }

        public Grip()
        {
            this.GripType = default(GripType);
            this.Radius = 1.0f;
            this.BreakFreeTolerance = 1.0f;
            this.AttachmentBoneGrip = string.Empty;
            this.AttachmentBoneTarget = string.Empty;
            this.FinishOnGrip = false;
        }

        public Grip(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Grip AnimationAction...");

            this.GripType = (GripType)reader.ReadByte();
            this.Radius = reader.ReadSingle();
            this.BreakFreeTolerance = reader.ReadSingle();
            this.AttachmentBoneGrip = reader.ReadString();
            this.AttachmentBoneTarget = reader.ReadString();
            this.FinishOnGrip = reader.ReadBoolean();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Grip AnimationAction...");

            writer.Write((byte)this.GripType);
            writer.Write(this.Radius);
            writer.Write(this.BreakFreeTolerance);
            writer.Write(this.AttachmentBoneGrip);
            writer.Write(this.AttachmentBoneTarget);
            writer.Write(this.FinishOnGrip);
        }
    }
}
