using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Buffs.Derived
{
    public class BuffModifyHitPoints : Buff
    {
        public float HitPointsMultiplier { get; set; }
        public float HitPointsModifier { get; set; }

        public BuffModifyHitPoints()
        {
            this.HitPointsMultiplier = 1.0f;
            this.HitPointsModifier = 0.0f;
        }

        public BuffModifyHitPoints(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading BuffModifyHitPoints...");

            this.HitPointsMultiplier = reader.ReadSingle();
            this.HitPointsModifier = reader.ReadSingle();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing BuffModifyHitPoints...");

            writer.Write(this.HitPointsMultiplier);
            writer.Write(this.HitPointsModifier);
        }
    }
}
