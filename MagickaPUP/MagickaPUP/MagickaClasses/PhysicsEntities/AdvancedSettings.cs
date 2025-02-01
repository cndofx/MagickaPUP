using MagickaPUP.MagickaClasses.Character;
using MagickaPUP.MagickaClasses.Character.Animation;
using MagickaPUP.MagickaClasses.Character.Attachments;
using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.PhysicsEntities
{
    public class AdvancedSettings
    {
        // Advanced Config
        public float Radius { get; set; }

        // Model Properties
        public ModelProperties[] ModelProperties { get; set; }

        // Skinned Mesh
        public bool HasSkinnedMesh { get; set; }
        public string SkinnedMesh { get; set; } // External Reference. Contains the skeleton / animations etc...

        // Effects
        public EffectHolder[] Effects { get; set; }

        // Animations
        public AnimationList[] Animations { get; set; }

        public AdvancedSettings()
        { }

        public AdvancedSettings(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Advanced Physics Entity Settings...");

            // Advanced Config
            this.Radius = reader.ReadSingle();

            // Model Properties
            int numModelProperties = reader.ReadInt32();
            this.ModelProperties = new ModelProperties[numModelProperties];
            for (int i = 0; i < numModelProperties; ++i)
                this.ModelProperties[i] = new ModelProperties(reader, logger);

            // Skinned Mesh
            this.HasSkinnedMesh = reader.ReadBoolean();
            this.SkinnedMesh = reader.ReadString(); // ER

            // Attached Effects
            int numAttachedEffects = reader.ReadInt32();
            this.Effects = new EffectHolder[numAttachedEffects];
            for (int i = 0; i < numAttachedEffects; ++i)
                this.Effects[i] = EffectHolder.Read(reader, logger);

            // Animations
            this.Animations = new AnimationList[27];
            for (int i = 0; i < this.Animations.Length; ++i)
                this.Animations[i] = new AnimationList(reader, logger);
            


            throw new NotImplementedException("Read Advanced Physics Entity Settings is not implemented yet!");
        }

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Advanced Physics Entity Settings...");

            throw new NotImplementedException("Write Advanced Physics Entity Settings is not implemented yet!");
        }
    }
}
