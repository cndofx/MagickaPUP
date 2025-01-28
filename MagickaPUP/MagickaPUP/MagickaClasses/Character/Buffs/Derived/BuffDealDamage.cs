using MagickaPUP.IO;
using MagickaPUP.MagickaClasses.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Buffs.Derived
{
    public class BuffDealDamage : Buff
    {
        public Damage Damage { get; set; }

        public BuffDealDamage()
        {
            this.Damage = new Damage();
        }

        public BuffDealDamage(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading BuffDealDamage...");

            this.Damage = new Damage
            {
                AttackProperty = (AttackProperties)reader.ReadInt32(),
                Element = (Elements)reader.ReadInt32(),
                Amount = reader.ReadSingle(),
                Magnitude = reader.ReadSingle()
            };
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing BuffDealDamage...");

            writer.Write((int)this.Damage.AttackProperty);
            writer.Write((int)this.Damage.Element);
            writer.Write((float)this.Damage.Amount);
            writer.Write((float)this.Damage.Magnitude);
        }
    }
}
