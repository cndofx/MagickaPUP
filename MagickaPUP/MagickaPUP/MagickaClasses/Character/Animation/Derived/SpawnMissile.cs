using MagickaPUP.IO;
using MagickaPUP.MagickaClasses.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    public class SpawnMissile : AnimationAction
    {
        public int Weapon { get; set; }
        public Vec3 Velocity { get; set; }
        public bool ItemAligned { get; set; }

        public SpawnMissile()
        {
            this.Weapon = 0;
            this.Velocity = new Vec3();
            this.ItemAligned = false;
        }

        public SpawnMissile(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading SpawnMissile AnimationAction...");

            this.Weapon = reader.ReadInt32();
            this.Velocity = Vec3.Read(reader, logger);
            this.ItemAligned = reader.ReadBoolean();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing SpawnMissile AnimationAction...");

            writer.Write(this.Weapon);
            this.Velocity.WriteInstance(writer, logger);
            writer.Write(this.ItemAligned);
        }
    }
}
