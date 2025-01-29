using MagickaPUP.IO;
using MagickaPUP.MagickaClasses.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    public class DealDamage : AnimationAction
    {
        public int Weapon { get; set; }
        public DamageTargets Target { get; set; }

        public DealDamage()
        {
            this.Weapon = 0;
            this.Target = default(DamageTargets);
        }

        public DealDamage(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading DealDamage AnimationAction...");

            this.Weapon = reader.ReadInt32();
            this.Target = (DamageTargets)reader.ReadByte();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing DealDamage AnimationAction...");

            writer.Write((int)this.Weapon);
            writer.Write((byte)this.Target);
        }
    }
}
