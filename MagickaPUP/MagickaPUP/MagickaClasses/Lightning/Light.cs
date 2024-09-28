using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using MagickaPUP.IO;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.XnaClasses;

namespace MagickaPUP.MagickaClasses.Lightning
{
    public enum LightType
    {
        Point = 0,
        Directional,
        Spot,
        Custom = 10
    }

    public enum LightVariationType
    {
        None = 0,
        Sine,
        Flicker,
        Candle,
        Strobe
    }

    public class Light : XnaObject
    {
        #region Variables

        public string LightName { get; set; }
        public Vec3 Position { get; set; }
        public Vec3 Direction { get; set; } // this is the direction for directional lights and spotlights
        public LightType LightType { get; set; } // enum value that determines the type of light (point, spot, directional)
        public LightVariationType LightVariationType { get; set; } // enum value that determines the type of variation (none, sine, candle-like, etc...)
        public float Reach { get; set; } // this is the radius for point lights and the distance for spotlights.
        public bool UseAttenuation { get; set; }
        public float CutoffAngle { get; set; }
        public float Sharpness { get; set; }

        public Vec3 DiffuseColor { get; set; }
        public Vec3 AmbientColor { get; set; }
        public float SpecularAmount { get; set; }
        public float VariationSpeed { get; set; }
        public float VariationAmount { get; set; }
        public int ShadowMapSize { get; set; }
        public bool CastsShadows { get; set; }

        #endregion

        #region Constructor

        public Light()
        {

        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Light...");

            this.LightName = reader.ReadString();
            this.Position = Vec3.Read(reader);
            this.Direction = Vec3.Read(reader);
            this.LightType = (LightType)reader.ReadInt32();
            this.LightVariationType = (LightVariationType)reader.ReadInt32();
            this.Reach = reader.ReadSingle();
            this.UseAttenuation = reader.ReadBoolean();
            this.CutoffAngle = reader.ReadSingle();
            this.Sharpness = reader.ReadSingle();

            this.DiffuseColor = Vec3.Read(reader);
            this.AmbientColor = Vec3.Read(reader);
            this.SpecularAmount = reader.ReadSingle();
            this.VariationSpeed = reader.ReadSingle();
            this.VariationAmount = reader.ReadSingle();
            this.ShadowMapSize = reader.ReadInt32();
            this.CastsShadows = reader.ReadBoolean();
        }

        public static Light Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new Light();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Light...");

            writer.Write(this.LightName);
            this.Position.WriteInstance(writer, logger);
            this.Direction.WriteInstance(writer, logger);
            writer.Write((int)this.LightType);
            writer.Write((int)this.LightVariationType);
            writer.Write(this.Reach);
            writer.Write(this.UseAttenuation);
            writer.Write(this.CutoffAngle);
            writer.Write(this.Sharpness);

            this.DiffuseColor.WriteInstance(writer, logger);
            this.AmbientColor.WriteInstance(writer, logger);
            writer.Write(this.SpecularAmount);
            writer.Write(this.VariationSpeed);
            writer.Write(this.VariationAmount);
            writer.Write(this.ShadowMapSize);
            writer.Write(this.CastsShadows);
        }

        #endregion
    }
}
