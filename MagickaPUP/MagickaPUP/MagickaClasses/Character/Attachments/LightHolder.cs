using System;
using MagickaPUP.Utility.IO;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.XnaClasses;
using MagickaPUP.MagickaClasses.Lightning;

namespace MagickaPUP.MagickaClasses.Character.Attachments
{
    public class LightHolder : XnaObject
    {
        #region Variables

        public string jointName { get; set; }
        public float radius { get; set; }
        public Vec3 colorDiffuse { get; set; }
        public Vec3 colorAmbient { get; set; }
        public float specularAmount { get; set; }
        public LightVariationType lightVariationType { get; set; }
        public float lightVariationAmount { get; set; }
        public float lightVariationSpeed { get; set; }

        #endregion

        #region Constructor

        LightHolder()
        {
            this.jointName = default;
            this.radius = 0.0f;
            this.colorDiffuse = new Vec3();
            this.colorAmbient = new Vec3();
            this.specularAmount = 0.0f;
            this.lightVariationType = LightVariationType.None;
            this.lightVariationAmount = 0.0f;
            this.lightVariationSpeed = 0.0f;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading LightHolder...");

            this.jointName = reader.ReadString();
            this.radius = reader.ReadSingle();
            this.colorDiffuse = Vec3.Read(reader, logger);
            this.colorAmbient = Vec3.Read(reader, logger);
            this.specularAmount = reader.ReadSingle();
            this.lightVariationType = (LightVariationType)reader.ReadByte();
            this.lightVariationAmount = reader.ReadSingle();
            this.lightVariationSpeed = reader.ReadSingle();
        }

        public static LightHolder Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new LightHolder();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing LightHolder...");

            writer.Write(this.jointName);
            writer.Write(this.radius);
            this.colorDiffuse.WriteInstance(writer, logger);
            this.colorAmbient.WriteInstance(writer, logger);
            writer.Write(this.specularAmount);
            writer.Write((byte)this.lightVariationType);
            writer.Write(this.lightVariationAmount);
            writer.Write(this.lightVariationSpeed);
        }

        #endregion
    }
}
