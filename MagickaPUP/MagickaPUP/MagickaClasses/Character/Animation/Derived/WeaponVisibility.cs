using System;
using System.Collections.Generic;
using MagickaPUP.IO;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    public class WeaponVisibility : AnimationAction
    {
        public int Weapon { get; set; }
        public bool Visible { get; set; }

        public WeaponVisibility()
        {
            this.Weapon = 0;
            this.Visible = false;
        }

        public WeaponVisibility(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing WeaponVisibility AnimationAction...");

            this.Weapon = reader.ReadInt32();
            this.Visible = reader.ReadBoolean();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing WeaponVisibility AnimationAction...");

            writer.Write(this.Weapon);
            writer.Write(this.Visible);
        }
    }
}
