using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagickaPUP.Utility.IO;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.MagickaClasses.Lightning;
using MagickaPUP.MagickaClasses.Liquids;
using MagickaPUP.MagickaClasses.Nav;
using MagickaPUP.MagickaClasses.Physics;
using MagickaPUP.MagickaClasses.Areas;
using MagickaPUP.XnaClasses;
using System.Security.Policy;
using MagickaPUP.MagickaClasses.Collision;

namespace MagickaPUP.MagickaClasses.Map
{
    public class LevelModel : XnaObject
    {
        #region Variables

        public BiTreeModel model { get; set; }

        public int numAnimatedParts { get; set; }
        public List<AnimatedLevelPart> animatedParts { get; set; }

        public int numLights { get; set; }
        public List<Light> lights { get; set; }

        public int numEffects { get; set; }
        public List<VisualEffectStorage> effects { get; set; }

        public int numPhysicsEntities { get; set; }
        public List<PhysicsEntityStorage> physicsEntities { get; set; }

        public int numLiquids { get; set; }
        public List<Liquid> liquids { get; set; }

        public int numForceFields { get; set; }
        public List<ForceField> forceFields { get; set; }

        public LevelModelCollisionData[] collisionDataLevel { get; set; }

        public LevelModelCollisionData collisionDataCamera { get; set; }

        public int numTriggers { get; set; }
        public List<Trigger> triggers { get; set; }

        public int numLocators { get; set; }
        public List<Locator> locators { get; set; }

        public NavMesh navMesh { get; set; }

        #endregion

        #region Constructor

        public LevelModel()
        {
            this.model = new BiTreeModel();

            this.numAnimatedParts = 0;
            this.animatedParts = new List<AnimatedLevelPart>();
            
            this.numLights = 0;
            this.lights = new List<Light>();

            this.numEffects = 0;
            this.effects = new List<VisualEffectStorage>();

            this.numPhysicsEntities = 0;
            this.physicsEntities = new List<PhysicsEntityStorage>();

            this.numLiquids = 0;
            this.liquids = new List<Liquid>();

            this.numForceFields = 0;
            this.forceFields = new List<ForceField>();

            this.collisionDataLevel = new LevelModelCollisionData[10];

            this.collisionDataCamera = new LevelModelCollisionData();

            this.numTriggers = 0;
            this.triggers = new List<Trigger>();

            this.numLocators = 0;
            this.locators = new List<Locator>();

            this.navMesh = new NavMesh();
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading LevelModel...");

            // Read the Binary Tree for the primary asset, which is the map mesh.
            // Is this a binary tree to replicate a very shitty version of Doom's binary space partition? could it be?
            this.model = XnaObject.ReadObject<BiTreeModel>(reader, logger);

            // Load animated parts in the level
            this.numAnimatedParts = reader.ReadInt32();
            for (int i = 0; i < this.numAnimatedParts; i++)
            {
                AnimatedLevelPart animatedPart = AnimatedLevelPart.Read(reader, logger);
                this.animatedParts.Add(animatedPart);
            }

            // Load lights
            this.numLights = reader.ReadInt32();
            logger?.Log(1, $"Num Lights : {this.numLights}");
            for (int i = 0; i < this.numLights; ++i)
            {
                Light currentLight = Light.Read(reader, logger);
                this.lights.Add(currentLight);
            }

            // Visual effects
            this.numEffects = reader.ReadInt32();
            logger?.Log(1, $"Num Visual Effects : {this.numEffects}");
            for (int i = 0; i < this.numEffects; ++i)
            {
                VisualEffectStorage effect = VisualEffectStorage.Read(reader, logger);
                this.effects.Add(effect);
            }

            // Number of physics entity storage
            this.numPhysicsEntities = reader.ReadInt32();
            logger?.Log(1, $"Num Physics Entities : {this.numPhysicsEntities}");
            for (int i = 0; i < this.numPhysicsEntities; ++i) // These are probably kinda like prefab props to be added to the world...? maybe?
            {
                PhysicsEntityStorage physEnt = PhysicsEntityStorage.Read(reader, logger);
                this.physicsEntities.Add(physEnt);
            }

            // Liquids
            this.numLiquids = reader.ReadInt32();
            logger?.Log(1, $"Num Liquids : {this.numLiquids}");
            for (int i = 0; i < this.numLiquids; ++i)
            {
                Liquid liquid = Liquid.Read(reader, logger);
                this.liquids.Add(liquid);
            }

            // Force Fields
            this.numForceFields = reader.ReadInt32();
            logger?.Log(1, $"Num Force Fields : {this.numForceFields}");
            for (int i = 0; i < this.numForceFields; ++i)
            {
                ForceField forceField = ForceField.Read(reader);
                this.forceFields.Add(forceField);
            }

            // Collision Mesh
            // Unlike AnimatedLevelPart, the collision seems to be read in 10 "layers", all of which have the exact same read operations as AnimatedLevelPart's collision reading.
            // All of the generated primitives on each loop are just added together into the same CollisionSkin mesh,
            // but each collision layer mesh corresponds to a different collision material, according to the 1:1 correspondence between the CollisionMaterial enum and the number of the collision layer index.
            // Note that you can choose the simplest route during map making, which is having all collisions within the same layer (Generic, layer 0),
            // with the entire collision mesh contained within the same layer, and then 9 bools with the value 0 (false) after it written in the file.
            // For more complex map making (extremely easy to do using MagickCow's interface in Blender), it is recommended to assign the corresponding collision layer
            // to each collision surface, according to its material type.
            for (int i = 0; i < 10; ++i)
            {
                logger?.Log(1, $"Collision Layer {i + 1}:");
                this.collisionDataLevel[i] = LevelModelCollisionData.Read(reader, logger);
            }

            // Form collision mesh triangles? or maybe add collision mesh for camera? it is not clear in the OG code.
            logger?.Log(1, "Collision Layer for Camera:");
            this.collisionDataCamera = LevelModelCollisionData.Read(reader, logger);

            // Trigger areas
            this.numTriggers = reader.ReadInt32();
            logger?.Log(1, $"Num Triggers : {this.numTriggers}");
            for (int i = 0; i < this.numTriggers; ++i)
            {
                Trigger trigger = Trigger.Read(reader, logger);
                this.triggers.Add(trigger);
            }

            // Locators
            this.numLocators = reader.ReadInt32();
            logger?.Log(1, $"Num Locators : {this.numLocators}");
            for (int i = 0; i < this.numLocators; ++i)
            {
                Locator locator = Locator.Read(reader, logger);
                this.locators.Add(locator);
            }

            // Create Nav Mesh
            this.navMesh = NavMesh.Read(reader, logger);
        }

        public static LevelModel Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new LevelModel();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing ModelLevel...");

            // Write 7 bit integer index for BiTreeModel Reader and then write the BiTreeModel itself.
            XnaObject.WriteObject(this.model, writer, logger);

            // Write the number of animated parts in the model as i32
            writer.Write(this.numAnimatedParts);
            // Write the animated parts
            for (int i = 0; i < this.numAnimatedParts; ++i)
            {
                this.animatedParts[i].WriteInstance(writer, logger);
            }

            // Write number of lights
            writer.Write(this.numLights);
            // Write lights
            for (int i = 0; i < this.numLights; ++i)
            {
                this.lights[i].WriteInstance(writer, logger);
            }

            // write number of visual effects
            writer.Write(this.numEffects);
            // write visual effects
            for (int i = 0; i < this.numEffects; ++i)
            {
                this.effects[i].WriteInstance(writer, logger);
            }

            // write number of physics entities
            writer.Write(this.numPhysicsEntities);
            // write physics entities
            for (int i = 0; i < this.numPhysicsEntities; ++i)
            {
                this.physicsEntities[i].WriteInstance(writer, logger);
            }

            // write number of liquids
            writer.Write(this.numLiquids);
            // write liquids
            for (int i = 0; i < this.numLiquids; ++i)
            {
                this.liquids[i].WriteInstance(writer, logger);
            }

            // write number of force fields
            writer.Write(this.numForceFields);
            // write force fields
            for (int i = 0; i < this.numForceFields; ++i)
            {
                this.forceFields[i].WriteInstance(writer, logger);
            }

            // write all collision mesh layers
            for (int i = 0; i < 10; ++i)
            {
                logger?.Log(1, $"Writing Collision Layer {i + 1}...");
                this.collisionDataLevel[i].WriteInstance(writer, logger);
            }

            // wriute camera collision
            logger?.Log(1, $"Writing Collision Layer for Camera...");
            this.collisionDataCamera.WriteInstance(writer, logger);

            // write number of trigger areas
            writer.Write(this.numTriggers);
            // write trigger areas
            for (int i = 0; i < this.numTriggers; ++i)
            {
                this.triggers[i].WriteInstance(writer, logger);
            }

            // write number of locators
            writer.Write(this.numLocators);
            // write locators
            for (int i = 0; i < this.numLocators; ++i)
            {
                this.locators[i].WriteInstance(writer, logger);
            }

            // write nav mesh
            this.navMesh.WriteInstance(writer, logger);
        }

        public override string GetReaderName()
        {
            return "Magicka.ContentReaders.LevelModelReader, Magicka";
        }

        public override bool ShouldAppendNullObject()
        {
            return true;
        }

        #endregion
    }
}
