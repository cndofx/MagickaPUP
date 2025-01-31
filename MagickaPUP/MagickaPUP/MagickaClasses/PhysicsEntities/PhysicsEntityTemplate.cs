using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;

namespace MagickaPUP.MagickaClasses.PhysicsEntities
{
    public class PhysicsEntityTemplate : XnaObject
    {
        #region Variables

        // Physics Entity Config / Properties
        public bool IsMovable { get; set; }
        public bool IsPushable { get; set; }
        public bool IsSolid { get; set; }
        public float Mass { get; set; }
        public int MaxHitPoints { get; set; }
        public bool CanHaveStatus { get; set; }

        // Resistances
        public Resistance[] Resistances { get; set; }

        // Gibs and gib models
        public GibReference[] Gibs { get; set; }

        // Effects
        public string HitEffect { get; set; }

        // Sound Banks
        public string SoundBanks { get; set; } // TODO : In the future maybe this should be replaced with an enum flags Banks and then parsed, that way we could get some Banks input validation, as well as a more consistent writing system for enum flags through strings in JSON files...

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading PhysicsEntityTemplate...");

            // Physics Config
            this.IsMovable = reader.ReadBoolean();
            this.IsPushable = reader.ReadBoolean();
            this.IsSolid = reader.ReadBoolean();
            this.Mass = reader.ReadSingle();
            this.MaxHitPoints = reader.ReadInt32();
            this.CanHaveStatus = reader.ReadBoolean();

            // Resistances
            int numResistances = reader.ReadInt32();
            this.Resistances = new Resistance[numResistances];
            for (int i = 0; i < numResistances; ++i)
                this.Resistances[i] = new Resistance(reader, logger);

            // Gibs
            int numGibs = reader.ReadInt32();
            this.Gibs = new GibReference[numGibs];
            for(int i = 0; i < numGibs; ++i)
                this.Gibs[i] = new GibReference(reader, logger);

            // Effects
            this.HitEffect = reader.ReadString();

            // Sounds
            this.SoundBanks = reader.ReadString();

            throw new NotImplementedException("Read PhysicsEntityTemplate is not implemented yet!");
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing PhysicsEntityTemplate...");

            // Physics Config
            writer.Write(this.IsMovable);
            writer.Write(this.IsPushable);
            writer.Write(this.IsSolid);
            writer.Write(this.Mass);
            writer.Write(this.MaxHitPoints);
            writer.Write(this.CanHaveStatus);

            // Resistances
            writer.Write(this.Resistances.Length);
            foreach (var resistance in this.Resistances)
                resistance.Write(writer, logger);

            // Gibs
            writer.Write(this.Gibs.Length);
            foreach (var gib in this.Gibs)
                gib.Write(writer, logger);

            // Effects
            writer.Write(this.HitEffect);

            // Sounds
            writer.Write(this.SoundBanks);

            throw new NotImplementedException("Write PhysicsEntityTemplate is not implemented yet!");
        }

        #endregion

        #region PrivateMethods
        #endregion
    }
}
