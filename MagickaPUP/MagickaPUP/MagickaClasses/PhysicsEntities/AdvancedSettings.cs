using MagickaPUP.MagickaClasses.Character;
using MagickaPUP.MagickaClasses.Character.Animation;
using MagickaPUP.MagickaClasses.Character.Attachments;
using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;

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
        }

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Advanced Physics Entity Settings...");

            // Advanced Config
            writer.Write(this.Radius);

            // Model Properties
            writer.Write(this.ModelProperties.Length);
            foreach (var property in this.ModelProperties)
                property.Write(writer, logger);

            // Skinned Mesh
            writer.Write(this.HasSkinnedMesh);
            writer.Write(this.SkinnedMesh); // ER

            // Attached Effects
            writer.Write(this.Effects.Length);
            foreach (var effect in this.Effects)
                effect.WriteInstance(writer, logger);

            // Animations
            // TODO : This TODO applies to ALL of the places where we write the 27 animation "channels"... the input JSON could have been modified to not include all the other channels. We could also have more than 27 channels on the input JSON file!!! We should actually calculate the remaining channels if we have less than 27, write default values for the remaining values. If we have more, maybe just ignore the extra or throw an exception or whatever.
            for (int i = 0; i < this.Animations.Length; ++i)
                this.Animations[i].Write(writer, logger);
        }
    }
}
