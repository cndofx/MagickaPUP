using MagickaPUP.MagickaClasses.Collision;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

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

        // Gibs and gib config
        public GibReference[] Gibs { get; set; }
        public string GibTrailEffect { get; set; }

        // Effects
        public string HitEffect { get; set; }
        public VisualEffectStorage[] VisualEffects { get; set; }

        // Sound Banks
        public string SoundBanks { get; set; } // TODO : In the future maybe this should be replaced with an enum flags Banks and then parsed, that way we could get some Banks input validation, as well as a more consistent writing system for enum flags through strings in JSON files...

        // Model
        public Model Model { get; set; }

        // Collision
        public bool HasCollision { get; set; }
        public List<Vec3> CollisionVertices { get; set; }
        public List<CollisionTriangle> CollisionTriangles { get; set; }

        // Bounding box maybe?
        // TODO : Figure out what the fuck this is
        public int NumBoundingBoxes { get; set; }
        public string[] SomeStrings { get; set; }
        public Vec3[] Positions { get; set; }
        public Vec3[] Sides { get; set; }
        public Quaternion[] Orientations { get; set; }

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

            // Hit Effect
            this.HitEffect = reader.ReadString();

            // Sounds
            this.SoundBanks = reader.ReadString();

            // Gib Trails
            this.GibTrailEffect = reader.ReadString();

            // Model
            this.Model = XnaObject.ReadObject<Model>(reader, logger);

            // Collision mesh
            this.HasCollision = reader.ReadBoolean();
            if (this.HasCollision)
            {
                this.CollisionVertices = XnaObject.ReadObject<List<Vec3>>(reader, logger);
                int numTriangles = reader.ReadInt32();
                for (int i = 0; i < numTriangles; ++i)
                {
                    CollisionTriangle triangle = new CollisionTriangle();
                    triangle.index0 = reader.ReadInt32();
                    triangle.index1 = reader.ReadInt32();
                    triangle.index2 = reader.ReadInt32();
                }
            }

            // What the fuck
            this.NumBoundingBoxes = reader.ReadInt32();
            this.SomeStrings = new string[this.NumBoundingBoxes];
            this.Positions = new Vec3[this.NumBoundingBoxes];
            this.Sides = new Vec3[this.NumBoundingBoxes];
            this.Orientations = new Quaternion[this.NumBoundingBoxes];
            for (int i = 0; i < NumBoundingBoxes; ++i) // NOTE : Most of this data goes completely fucking unused so I have no idea what the fuck its purpose is...
            {
                this.SomeStrings[i] = reader.ReadString();
                this.Positions[i].ReadInstance(reader, logger);
                this.Sides[i].ReadInstance(reader, logger);
                this.Orientations[i].ReadInstance(reader, logger);
            }

            // Visual Effects
            int numVisualEffects = reader.ReadInt32();
            this.VisualEffects = new VisualEffectStorage[numVisualEffects];
            for (int i = 0; i < numVisualEffects; ++i)
                this.VisualEffects[i] = new VisualEffectStorage(reader, logger);

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

            // Gib Trails
            writer.Write(this.GibTrailEffect);

            // Model
            XnaObject.WriteObject(this.Model, writer, logger);

            // Collision mesh
            writer.Write(this.HasCollision);
            if (this.HasCollision)
            {
                writer.Write(this.CollisionVertices.Count);
                foreach (var triangle in this.CollisionTriangles)
                {
                    writer.Write(triangle.index0);
                    writer.Write(triangle.index1);
                    writer.Write(triangle.index2);
                }
            }

            // TODO : Continue implementing later on...

            throw new NotImplementedException("Write PhysicsEntityTemplate is not implemented yet!");
        }

        #endregion

        #region PrivateMethods
        #endregion
    }
}
