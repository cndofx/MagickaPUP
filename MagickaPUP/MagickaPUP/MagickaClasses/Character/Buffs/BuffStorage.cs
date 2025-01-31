using MagickaPUP.IO;
using MagickaPUP.MagickaClasses.Generic;
using System;
using System.Collections.Generic;
using MagickaPUP.MagickaClasses.Data;
using MagickaPUP.Utility.Exceptions;
using MagickaPUP.MagickaClasses.Character.Buffs.Derived;

namespace MagickaPUP.MagickaClasses.Character.Buffs
{
    // NOTE : Just like the Ability class, BuffStorage is a weird type of polymorphic class where rather than identifying the type like Effect's derived types
    // through a ContentTypeReader, we instead identify it with an enum and a switch, so yeah.
    // The difference is that to make things easier this time around, we're separating the common part of BuffStorage into its own class and then everything else
    // into types derived from Buff.
    // TODO : Maybe rework the Ability type to use a similar system rather than whatever the fuck we cooked last time?
    public class BuffStorage
    {
        public BuffType BuffType { get; set; }
        public VisualCategory VisualCategory { get; set; }
        public Vec3 Color { get; set; }
        public float Time { get; set; }
        public string Effect { get; set; }
        public Buff Buff { get; set; }

        public BuffStorage()
        {
            this.BuffType = default(BuffType);
            this.VisualCategory = default(VisualCategory);
            this.Color = new Vec3(1.0f, 1.0f, 1.0f);
            this.Time = 1.0f;
            this.Effect = string.Empty;
            this.Buff = null;
        }

        public BuffStorage(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading BuffStorage...");

            this.BuffType = (BuffType)reader.ReadByte();
            this.VisualCategory = (VisualCategory)reader.ReadByte();
            this.Color = Vec3.Read(reader, logger);
            this.Time = reader.ReadSingle();
            this.Effect = reader.ReadString();

            switch (this.BuffType)
            {
                case BuffType.BoostDamage:
                    this.Buff = new BuffBoostDamage(reader, logger);
                    break;
                case BuffType.DealDamage:
                    this.Buff = new BuffDealDamage(reader, logger);
                    break;
                case BuffType.Resistance:
                    this.Buff = new BuffResistance(reader, logger);
                    break;
                case BuffType.Undying:
                    this.Buff = new BuffUndying(reader, logger);
                    break;
                case BuffType.Boost:
                    this.Buff = new BuffBoost(reader, logger);
                    break;
                case BuffType.ReduceAgro:
                    this.Buff = new BuffReduceAgro(reader, logger);
                    break;
                case BuffType.ModifyHitPoints:
                    this.Buff = new BuffModifyHitPoints(reader, logger);
                    break;
                case BuffType.ModifySpellTTL:
                    this.Buff = new BuffModifySpellTTL(reader, logger);
                    break;
                case BuffType.ModifySpellRange:
                    this.Buff = new BuffModifySpellRange(reader, logger);
                    break;
                default:
                    throw new MagickaReadException($"Specified BuffType \"{(int)this.BuffType}\" is unknown!");
                    break; // lol
            }
        }

        // TODO : Implement writing logic...
        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing BuffStorage...");
            throw new NotImplementedException("Write BuffStorage is not implemented yet!");
        }
    }
}
