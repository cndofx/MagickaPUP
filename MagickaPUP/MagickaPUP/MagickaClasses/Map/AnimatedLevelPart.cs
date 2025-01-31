using MagickaPUP.Utility.IO;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagickaPUP.MagickaClasses.Liquids;
using MagickaPUP.MagickaClasses.Areas;
using MagickaPUP.MagickaClasses.Lightning;
using MagickaPUP.MagickaClasses.Collision;
using MagickaPUP.MagickaClasses.Nav;

namespace MagickaPUP.MagickaClasses.Map
{
    public class AnimatedLevelPart : XnaObject
    {
        #region Variables

        public string name { get; set; }
        
        public bool affectShields { get; set; }

        public Model model { get; set; } // This model is an animated model, from the XNA framework, with its own bone system.

        public int numMeshSettings { get; set; }
        public Dictionary<string, KeyValuePair<bool, bool>> meshSettings { get; set; }

        public int numLiquids { get; set; }
        public List<Liquid> liquids { get; set; }

        public int numLocators { get; set; }
        public List<Locator> locators { get; set; }

        public float animationDuration { get; set; }
        public Animation animation { get; set; }

        public int numEffects { get; set; }
        public List<VisualEffectStorage> effects { get; set; }

        public int numLights { get; set; }
        public List<LightStorage> lights { get; set; }

        public bool hasCollision { get; set; } // at least that's what I assume this bool means...
        public CollisionMaterial collisionMaterial { get; set; }
        public List<Vec3> collisionVertices { get; set; }
        public int numCollisionTriangles { get; set; }
        public List<CollisionTriangle> collisionTriangles { get; set; }

        public bool hasNavMesh { get; set; } // at least that's what I assume this bool means...
        public AnimatedNavMesh navMesh { get; set; }

        public int numChildren { get; set; }
        public List<AnimatedLevelPart> children { get; set; }

        #endregion

        #region Constructor

        public AnimatedLevelPart()
        {
            this.name = "__none__";
            
            this.affectShields = false;
            
            this.model = new Model();
            
            this.numMeshSettings = 0;
            this.meshSettings = new Dictionary<string, KeyValuePair<bool, bool>>();
            
            this.numLiquids = 0;
            this.liquids = new List<Liquid>();
            
            this.numLocators = 0;
            this.locators = new List<Locator>();

            this.animationDuration = 0.0f;
            this.animation = new Animation();

            this.numEffects = 0;
            this.effects = new List<VisualEffectStorage>();

            this.numLights = 0;
            this.lights = new List<LightStorage>();

            this.hasCollision = false;
            this.collisionMaterial = CollisionMaterial.Generic;
            this.collisionVertices = new List<Vec3>();
            this.numCollisionTriangles = 0;
            this.collisionTriangles = new List<CollisionTriangle>();

            this.hasNavMesh = false;
            this.navMesh = new AnimatedNavMesh();

            this.numChildren = 0;
            this.children = new List<AnimatedLevelPart>();
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading AnimatedLevelPart...");
            
            this.name = reader.ReadString();

            this.affectShields = reader.ReadBoolean();
            
            this.model = XnaObject.ReadObject<Model>(reader, logger); // This Model is an XNA model, not a model in Magicka's map model format. XNA models contain bone data and animation data.

            // Load Mesh Settings
            // TODO : we'll have to figure out what each setting is by reading the possible strings and what each does...
            this.numMeshSettings = reader.ReadInt32();
            for (int i = 0; i < this.numMeshSettings; ++i)
            {
                string key = reader.ReadString();
                bool key2 = reader.ReadBoolean();
                bool value = reader.ReadBoolean();
                this.meshSettings.Add(key, new KeyValuePair<bool, bool>(key2, value));
            }
            
            // Liquids
            this.numLiquids = reader.ReadInt32();
            for (int i = 0; i < this.numLiquids; ++i)
            {
                Liquid liquid = Liquid.Read(reader, logger);
                this.liquids.Add(liquid);
            }
            
            // Locators associated with the animated level part.
            this.numLocators = reader.ReadInt32();
            for (int i = 0; i < this.numLocators; ++i)
            {
                Locator locator = Locator.Read(reader, logger);
                this.locators.Add(locator);
            }
            
            // Read Animation Data
            this.animationDuration = reader.ReadSingle();
            this.animation = Animation.Read(reader, logger);
            
            
            // Effects
            this.numEffects = reader.ReadInt32();
            for (int i = 0; i < this.numEffects; ++i)
            {
                VisualEffectStorage effect = VisualEffectStorage.Read(reader, logger);
                this.effects.Add(effect);
            }
            
            // Lights
            this.numLights = reader.ReadInt32();
            for (int i = 0; i < this.numLights; ++i)
            {
                LightStorage light = LightStorage.Read(reader, logger);
                this.lights.Add(light);
            }
            
            // Collision Material and Collision Mesh
            this.hasCollision = reader.ReadBoolean();
            if (this.hasCollision)
            {
                this.collisionMaterial = (CollisionMaterial)reader.ReadByte(); // enum CollisionMaterials
                this.collisionVertices = XnaObject.ReadObject<List<Vec3>>(reader, logger); // read list of vertices of the collision mesh.
                this.numCollisionTriangles = reader.ReadInt32();
                for (int i = 0; i < this.numCollisionTriangles; ++i)
                {
                    CollisionTriangle tri = CollisionTriangle.Read(reader, logger);
                    this.collisionTriangles.Add(tri);
                }
            }
            
            // AnimatedNavMesh
            this.hasNavMesh = reader.ReadBoolean();
            if (this.hasNavMesh)
            {
                this.navMesh = AnimatedNavMesh.Read(reader, logger);
            }
            
            // Add children animated level parts
            this.numChildren = reader.ReadInt32();
            for (int i = 0; i < this.numChildren; ++i)
            {
                AnimatedLevelPart part = AnimatedLevelPart.Read(reader, logger);
                this.children.Add(part);
            }
        }

        public static AnimatedLevelPart Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new AnimatedLevelPart();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing AnimatedLevelPart...");

            // Write Animated level part data
            writer.Write(this.name);
            writer.Write(this.affectShields);

            // Write Model
            XnaObject.WriteObject(this.model, writer, logger);

            // Write Mesh Settings
            writer.Write(this.numMeshSettings);
            foreach (var setting in this.meshSettings)
            {
                string key = setting.Key;
                bool value_key = setting.Value.Key;
                bool value_val = setting.Value.Value;

                writer.Write(key);
                writer.Write(value_key);
                writer.Write(value_val);
            }

            // Write Liquids
            writer.Write(this.numLiquids);
            foreach (var liquid in this.liquids)
            {
                liquid.WriteInstance(writer, logger);
            }

            // Write locators
            writer.Write(this.numLocators);
            foreach (var locator in this.locators)
            {
                locator.WriteInstance(writer, logger);
            }

            // Write Animation Data
            writer.Write(this.animationDuration);
            this.animation.WriteInstance(writer, logger);

            // Write Effects
            writer.Write(this.numEffects);
            foreach (var effect in this.effects)
            {
                effect.WriteInstance(writer, logger);
            }

            // Write Lights
            writer.Write(this.numLights);
            foreach (var light in this.lights)
            {
                light.WriteInstance(writer, logger);
            }

            // Write Collision Material and Collision Mesh
            writer.Write(this.hasCollision);
            if (this.hasCollision)
            {
                writer.Write((byte)this.collisionMaterial);
                XnaObject.WriteObject(this.collisionVertices, writer, logger);
                writer.Write(this.numCollisionTriangles);
                foreach (var tri in this.collisionTriangles)
                {
                    tri.WriteInstance(writer, logger);
                }
            }

            // Write AnimatedNavMesh
            writer.Write(this.hasNavMesh);
            if (this.hasNavMesh)
            {
                this.navMesh.WriteInstance(writer, logger);
            }

            // Write children animated level parts
            writer.Write(this.numChildren);
            foreach (var child in this.children)
            {
                child.WriteInstance(writer, logger);
            }
        }

        #endregion
    }
}
