using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.MagickaClasses.Lightning;
using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Item
{
    // NOTE : This class is actually a different one from the character point light holder even within Magicka's code. So there's a bunch of duplicated stuff, but whatever, it's just a struct, so yeah...
    // If this were C, no runtime overhead would exist for having 2 structs that are the same with slight variations, as structs are just bumdles of data, but here? they have to have their runtime type information bundled into the program, so yeah...
    public struct PointLightHolder
    {
        public float Radius { get; set; }
        public Vec3 DiffuseColor { get; set; }
        public Vec3 AmbientColor { get; set; }
        public float SpecularAmount { get; set; }
        public LightVariationType VariationType { get; set; }
        public float VariationAmount { get; set; }
        public float VariationSpeed { get; set; }

        // Fucking hell, why can't we have parameterless constructors for structs in C#??
        // Just enforcing that all members are initialized is already enough to ensure that proper construction takes place...
        // Here's an idea, why don't you just fucking zero out the memory like every other sane language with value types does??
        // This is one fucking ugly constructor due to this stupid language's constraints... all to allow the fucking JSON serializer to work out of the box! WTF!
        public PointLightHolder
        (
            float radius = 1.0f,
            Vec3 diffuseColor = default,
            Vec3 ambientColor = default,
            float specularAmount = 1.0f,
            LightVariationType variationType = LightVariationType.None,
            float variationAmount = 0.0f,
            float variationSpeed = 0.0f
        )
        {
            this.Radius = radius;
            this.DiffuseColor = diffuseColor;
            this.AmbientColor = ambientColor;
            this.SpecularAmount = specularAmount;
            this.VariationType = variationType;
            this.VariationAmount = variationAmount;
            this.VariationSpeed = variationSpeed;
        }

        public PointLightHolder(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Item Point Light Holder...");

            this.Radius = reader.ReadSingle();
            this.DiffuseColor = Vec3.Read(reader, logger);
            this.AmbientColor = Vec3.Read(reader, logger);
            this.SpecularAmount = reader.ReadSingle();
            this.VariationType = (LightVariationType)reader.ReadByte();
            this.VariationAmount = reader.ReadSingle();
            this.VariationSpeed = reader.ReadSingle();
        }

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Item Point Light Holder...");

            writer.Write(this.Radius);
            this.DiffuseColor.WriteInstance(writer, logger);
            this.AmbientColor.WriteInstance(writer, logger);
            writer.Write(this.SpecularAmount);
            writer.Write((byte)this.VariationType);
            writer.Write(this.VariationAmount);
            writer.Write(this.VariationSpeed);
        }
    }
}
