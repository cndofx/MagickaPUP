using MagickaPUP.Utility.IO;
using MagickaPUP.MagickaClasses.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Effects
{
    public class EffectDeferred : Effect
    {
        #region Variables

        public float Alpha { get; set; }
        public float Sharpness { get; set; }
        public bool VertexColorEnabled { get; set; }
        public bool UseMaterialTextureForReflectiveness { get; set; }
        public string ReflectionMap { get; set; } /* ER */
        public bool DiffuseTexture0AlphaDisabled { get; set; }
        public bool AlphaMask0Enabled { get; set; }
        public Vec3 DiffuseColor0 { get; set; }
        public float SpecAmount0 { get; set; }
        public float SpecPower0 { get; set; }
        public float EmissiveAmount0 { get; set; }
        public float NormalPower0 { get; set; }
        public float Reflectiveness0 { get; set; }
        public string DiffuseTexture0 { get; set; } /* ER */
        public string MaterialTexture0 { get; set; } /* ER */
        public string NormalTexture0 { get; set; } /* ER */

        public bool HasSecondSet { get; set; } // determine if it has the secondary Effect data or not.

        public bool DiffuseTexture1AlphaDisabled { get; set; }
        public bool AlphaMask1Enabled { get; set; }
        public Vec3 DiffuseColor1 { get; set; }
        public float SpecAmount1 { get; set; }
        public float SpecPower1 { get; set; }
        public float EmissiveAmount1 { get; set; }
        public float NormalPower1 { get; set; }
        public float Reflectiveness1 { get; set; }
        public string DiffuseTexture1 { get; set; } /* ER */
        public string MaterialTexture1 { get; set; } /* ER */
        public string NormalTexture1 { get; set; } /* ER */

        #endregion

        #region Constructor

        public EffectDeferred() : base()
        {
            this.Alpha = default;
            this.Sharpness = default;
            this.VertexColorEnabled = default;
            this.UseMaterialTextureForReflectiveness = default;
            this.ReflectionMap = default;
            this.DiffuseTexture0AlphaDisabled = default;
            this.AlphaMask0Enabled = default;
            this.DiffuseColor0 = default;
            this.SpecAmount0 = default;
            this.SpecPower0 = default;
            this.EmissiveAmount0 = default;
            this.NormalPower0 = default;
            this.Reflectiveness0 = default;
            this.DiffuseTexture0 = default;
            this.MaterialTexture0 = default;
            this.NormalTexture0 = default;

            this.HasSecondSet = default;

            this.DiffuseTexture1AlphaDisabled = default;
            this.AlphaMask1Enabled = default;
            this.DiffuseColor1 = default;
            this.SpecAmount1 = default;
            this.SpecPower1 = default;
            this.EmissiveAmount1 = default;
            this.NormalPower1 = default;
            this.Reflectiveness1 = default;
            this.DiffuseTexture1 = default;
            this.MaterialTexture1 = default;
            this.NormalTexture1 = default;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading EffectDeferred...");

            this.Alpha = reader.ReadSingle();
            this.Sharpness = reader.ReadSingle();
            this.VertexColorEnabled = reader.ReadBoolean();
            this.UseMaterialTextureForReflectiveness = reader.ReadBoolean();
            this.ReflectionMap = reader.ReadString(); /* ER */
            this.DiffuseTexture0AlphaDisabled = reader.ReadBoolean();
            this.AlphaMask0Enabled = reader.ReadBoolean();
            this.DiffuseColor0 = Vec3.Read(reader, null);
            this.SpecAmount0 = reader.ReadSingle();
            this.SpecPower0 = reader.ReadSingle();
            this.EmissiveAmount0 = reader.ReadSingle();
            this.NormalPower0 = reader.ReadSingle();
            this.Reflectiveness0 = reader.ReadSingle();
            this.DiffuseTexture0 = reader.ReadString(); /* ER */
            this.MaterialTexture0 = reader.ReadString(); /* ER */
            this.NormalTexture0 = reader.ReadString(); /* ER */

            this.HasSecondSet = reader.ReadBoolean();

            if (this.HasSecondSet)
            {
                this.DiffuseTexture1AlphaDisabled = reader.ReadBoolean();
                this.AlphaMask1Enabled = reader.ReadBoolean();
                this.DiffuseColor1 = Vec3.Read(reader, null);
                this.SpecAmount1 = reader.ReadSingle();
                this.SpecPower1 = reader.ReadSingle();
                this.EmissiveAmount1 = reader.ReadSingle();
                this.NormalPower1 = reader.ReadSingle();
                this.Reflectiveness1 = reader.ReadSingle();
                this.DiffuseTexture1 = reader.ReadString(); /* ER */
                this.MaterialTexture1 = reader.ReadString(); /* ER */
                this.NormalTexture1 = reader.ReadString(); /* ER */
            }

            logger?.Log(2, $" - Reflection Map     : \"{this.ReflectionMap}\"");
            logger?.Log(2, $" - Diffuse  Texture 0 : \"{this.DiffuseTexture0}\"");
            logger?.Log(2, $" - Material Texture 0 : \"{this.MaterialTexture0}\"");
            logger?.Log(2, $" - Normal   Texture 0 : \"{this.NormalTexture0}\"");
            if (this.HasSecondSet)
            {
                logger?.Log(2, $" - Diffuse  Texture 1 : \"{this.DiffuseTexture1}\"");
                logger?.Log(2, $" - Material Texture 1 : \"{this.MaterialTexture1}\"");
                logger?.Log(2, $" - Normal   Texture 1 : \"{this.NormalTexture1}\"");
            }
        }

        public static new EffectDeferred Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new EffectDeferred();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing EffectDeferred...");

            writer.Write(this.Alpha);
            writer.Write(this.Sharpness);
            writer.Write(this.VertexColorEnabled);
            writer.Write(this.UseMaterialTextureForReflectiveness);
            writer.Write(this.ReflectionMap); /* ER */
            writer.Write(this.DiffuseTexture0AlphaDisabled);
            writer.Write(this.AlphaMask0Enabled);

            this.DiffuseColor0.WriteInstance(writer, null);

            writer.Write(this.SpecAmount0);
            writer.Write(this.SpecPower0);
            writer.Write(this.EmissiveAmount0);
            writer.Write(this.NormalPower0);
            writer.Write(this.Reflectiveness0);
            writer.Write(this.DiffuseTexture0); /* ER */
            writer.Write(this.MaterialTexture0); /* ER */
            writer.Write(this.NormalTexture0); /* ER */

            writer.Write(this.HasSecondSet);

            if (this.HasSecondSet)
            {
                writer.Write(this.DiffuseTexture1AlphaDisabled);
                writer.Write(this.AlphaMask1Enabled);

                this.DiffuseColor1.WriteInstance(writer, null);

                writer.Write(this.SpecAmount1);
                writer.Write(this.SpecPower1);
                writer.Write(this.EmissiveAmount1);
                writer.Write(this.NormalPower1);
                writer.Write(this.Reflectiveness1);
                writer.Write(this.DiffuseTexture1); /* ER */
                writer.Write(this.MaterialTexture1); /* ER */
                writer.Write(this.NormalTexture1); /* ER */
            }

            logger?.Log(2, $" - Reflection Map     : \"{this.ReflectionMap}\"");
            logger?.Log(2, $" - Diffuse  Texture 0 : \"{this.DiffuseTexture0}\"");
            logger?.Log(2, $" - Material Texture 0 : \"{this.MaterialTexture0}\"");
            logger?.Log(2, $" - Normal   Texture 0 : \"{this.NormalTexture0}\"");
            if (this.HasSecondSet)
            {
                logger?.Log(2, $" - Diffuse  Texture 1 : \"{this.DiffuseTexture1}\"");
                logger?.Log(2, $" - Material Texture 1 : \"{this.MaterialTexture1}\"");
                logger?.Log(2, $" - Normal   Texture 1 : \"{this.NormalTexture1}\"");
            }
        }

        public override string GetReaderName()
        {
            return "PolygonHead.Pipeline.RenderDeferredEffectReader, PolygonHead, Version=1.0.0.0, Culture=neutral";
        }

        #endregion
    }
}
