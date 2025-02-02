using MagickaPUP.MagickaClasses.Character;
using MagickaPUP.MagickaClasses.Character.Events;
using MagickaPUP.MagickaClasses.Collision;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.Utility.Exceptions;
using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace MagickaPUP.MagickaClasses.PhysicsEntities
{
    public class PhysicsEntityTemplate : XnaObject
    {
        #region Constants

        public static readonly string EXCEPTION_MSG_LIGHTS = "Magicka does not support lights within Physics Entities!";

        #endregion

        #region Variables

        // ID Strings
        public string PhysicsEntityID { get; set; }

        // Physics Entity Config (1)
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

        // Bounding Boxes
        public BoundingBox[] BoundingBoxes { get; set; }

        // Lights
        // NOTE : Unused in Magicka. Probably a left over of an unimplemented feature.
        #region Comment
        // NOTE : Magicka does not support lights for PhysicsEntities AT ALL, but they were contemplated at some point during development,
        // so the XNB files still need to contain an i32 with value 0.
        // Within the game's code, a NotImplementedException is thrown when the number of lights found within the XNB file is greater than 0.
        // The read value is not used for anything else, but it still has to be there for the file to be valid.
        // Light[] lights or whatever would be the data type of this field if it were supported... Just keep that in mind in case you extend
        // the engine to add support in the future!
        // TODO : Modify this with game version if you ever get around making a modified version of the engine that actually supports this... tho you would need to
        // figure out what to even implement in the first place, cause there is literally 0 code related to lights in physics entities that we could work from...
        #endregion

        // Conditions and Events
        public ConditionCollection Events { get; set; }

        // Advanced Settings
        public bool HasAdvancedSettings { get; set; } // Stupid fucking flag implementation that sucks dick. Implemented weirdly, but anyway, determines whether the physics entity has "advanced settings" or not (stuff like model properties, skinned model, etc...)
        public AdvancedSettings AdvancedSettings { get; set; }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading PhysicsEntityTemplate...");

            // Physics Config (1)
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
            this.CollisionVertices = new List<Vec3>();
            this.CollisionTriangles = new List<CollisionTriangle>();
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
                    this.CollisionTriangles.Add(triangle);
                }
            }

            // Bounding Boxes
            // NOTE : Most of this data goes completely fucking unused, as no matter how many bounding boxes there are, within Magicka's code actually
            // only the first one is used, the rest are discarded in a loop that reads the data for the rest of the bounding boxes but doesn't store it.
            // For now, we'll keep the multi bounding box support, but it would make sense in the future to just drop it entirely and replace this with a single read.
            int numBoundingBoxes = reader.ReadInt32();
            this.BoundingBoxes = new BoundingBox[numBoundingBoxes];
            for (int i = 0; i < numBoundingBoxes; ++i)
                this.BoundingBoxes[i] = new BoundingBox(reader, logger);

            // Visual Effects
            int numVisualEffects = reader.ReadInt32();
            this.VisualEffects = new VisualEffectStorage[numVisualEffects];
            for (int i = 0; i < numVisualEffects; ++i)
                this.VisualEffects[i] = new VisualEffectStorage(reader, logger);

            // Lights
            int numLights = reader.ReadInt32();
            if (numLights > 0)
                throw new MagickaReadException(EXCEPTION_MSG_LIGHTS); // NOTE : Maybe this could be a bad idea considering how if an XNB file ever has this value it is either malformed or we were wrong when reverse engineering the code... or maybe the rest of the XNB file is correct and we could avoid decompilation problems by just being permissive here, but whatever, just for correctness we'll throw an exception here.

            // Conditions and Events
            this.Events = new ConditionCollection(reader, logger);

            // ID Strings
            this.PhysicsEntityID = reader.ReadString();

            // Advanced Settings
            #region Comment
            // Fucking piece of shit flag alert!!!
            // NOTE : WHO THE FUCK THOUGHT THIS WAS A GOOD IDEA? MAGICKA DEVS WERE ON CRACK OR WHAT???
            // This really kicks me in the balls, because now, any physics entity file that contains a NULL shared resource at the end will basically fail to load properly...
            // Notice how the ReadBoolean() call can only throw an exception if we're at the end of the file and we start reading out of bounds??? yeah...
            // The flag can either be present in the file and be false or be true, if it is there and it is true, we keep reading just fine. If it is there and it is false
            // we just finish reading just file.
            // If the flag is not there, we catch the exception when trying to read out of bounds in the file, and set it to false and finish reading.
            // The problem is... what happens if this file contains more data after the flag? for example, shared resources with the first byte being non 0 right after the
            // primary object of the XNB file??? basically, this means that PhysicsEntityTemplate XNB files CANNOT contain shared resources at all!!!
            #endregion
            try
            {
                this.HasAdvancedSettings = reader.ReadBoolean();
            }
            catch
            {
                this.HasAdvancedSettings = false;
            }

            if (this.HasAdvancedSettings) // If the flag is false, just return and don't keep reading anything else, cause the file has no more contents to deal with.
                this.AdvancedSettings = new AdvancedSettings(reader, logger);

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

            // BoundingBoxes
            writer.Write(this.BoundingBoxes.Length);
            foreach (var bb in this.BoundingBoxes)
                bb.Write(writer, logger);

            // Visual Effects
            writer.Write(this.VisualEffects.Length);
            foreach (var effect in this.VisualEffects)
                effect.Write(writer, logger);

            // Lights
            writer.Write(0); // NOTE : We hardcode writing always 0 lights since we can't even read it from the JSON file. We literally never store it there because Magicka just doesn't support lights in Physics Entities and making this value anything higher than 0 will throw an exception in game and crash.

            // Conditions and Events
            this.Events.Write(writer, logger);

            // ID Strings
            writer.Write(this.PhysicsEntityID);

            throw new NotImplementedException("Write PhysicsEntityTemplate is not implemented yet!");
        }

        public override string GetReaderName()
        {
            return "Magicka.ContentReaders.PhysicsEntityTemplateReader, Magicka";
        }

        public static PhysicsEntityTemplate Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new PhysicsEntityTemplate();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        #endregion

        #region PrivateMethods
        #endregion
    }
}
