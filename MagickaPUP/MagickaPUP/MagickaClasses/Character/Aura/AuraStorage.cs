using MagickaPUP.IO;
using MagickaPUP.MagickaClasses.Data;
using MagickaPUP.MagickaClasses.Data.Aura;
using MagickaPUP.MagickaClasses.Generic;
using System;
using System.Collections.Generic;
using MagickaPUP.Utility.Exceptions;
using MagickaPUP.MagickaClasses.Character.Aura.Derived;

namespace MagickaPUP.MagickaClasses.Character.Aura
{
    // TODO : Implement
    public class AuraStorage
    {
        public AuraTarget AuraTarget { get; set; }
        public AuraType AuraType { get; set; }
        public VisualCategory VisualCategory { get; set; }
        public Vec3 Color { get; set; }
        public string Effect { get; set; }
        public float Time { get; set; } // NOTE : The Execute() method within the AuraStorage class subtracts every frame delta time from its variable TTL, which is what this Time variable corresponds to. This means that this would most likely correspond to some kind of "Duration" variable. Maybe TTL means "Time to Last"? or whatever the fuck? lol...
        public float Radius { get; set; }
        public string[] TargetTypes { get; set; } // NOTE : This is a single string within XNB files. It contains a comma separated list of elements. For easier JSON editting, I'm splitting them into an array, but you MUST REMEMBER this detail when implementing both the read and write logic for this class!!! otherwise, everything goes to SHIT!
        public Factions TargetFaction { get; set; }
        public Aura Aura { get; set; }

        public AuraStorage()
        { }

        public AuraStorage(MBinaryReader reader, DebugLogger logger = null)
        {
            this.AuraTarget = (AuraTarget)reader.ReadByte();
            this.AuraType = (AuraType)reader.ReadByte();
            this.VisualCategory = (VisualCategory)reader.ReadByte();
            this.Color = Vec3.Read(reader, logger);
            this.Effect = reader.ReadString();
            this.Time = reader.ReadSingle();
            this.Radius = reader.ReadSingle();

            // Read the comma separated target string and then split it and store it within our TargetTypes array.
            this.TargetTypes = reader.ReadString().Split(new char[] { ',' });

            this.TargetFaction = (Factions)reader.ReadInt32();

            // Get the correct aura type
            switch (this.AuraType)
            {
                case AuraType.Buff:
                    this.Aura = new AuraBuff(reader, logger);
                    break;
                case AuraType.Deflect:
                    this.Aura = new AuraDeflect(reader, logger);
                    break;
                case AuraType.Boost:
                    this.Aura = new AuraBoost(reader, logger);
                    break;
                case AuraType.LifeSteal:
                    this.Aura = new AuraLifeSteal(reader, logger);
                    break;
                case AuraType.Love:
                    this.Aura = new AuraLove(reader, logger);
                    break;
                default:
                    throw new MagickaLoadException("Unknown AuraType was read!");
                    break; // lol
            }
        }

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        { }
    }
}
