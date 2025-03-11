using MagickaPUP.Utility.IO;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Effects
{
    // Basically, this is the Effect class for water and all other liquids that are not lava, like wine, etc...
    internal class EffectDeferredLiquid : Effect
    {
        #region Variables

        public string ReflectionMap { get; set; }
        public float WaveHeight { get; set; }
        public Vec2 WaveSpeed0 { get; set; }
        public Vec2 WaveSpeed1 { get; set; }
        public float WaterReflectiveness { get; set; }
        public Vec3 BottomColor { get; set; }
        public Vec3 DeepBottomColor { get; set; }
        public float WaterEmissiveAmount { get; set; }
        public float WaterSpecAmount { get; set; }
        public float WaterSpecPower { get; set; }
        public string BottomTexture { get; set; }
        public string WaterNormalMap { get; set; }
        public float IceReflectiveness { get; set; }
        public Vec3 IceColor { get; set; }
        public float IceEmissiveAmount { get; set; }
        public float IceSpecAmount { get; set; }
        public float IceSpecPower { get; set; }
        public string IceDiffuseMap { get; set; }
        public string IceNormalMap { get; set; }

        #endregion

        #region Constructor

        public EffectDeferredLiquid()
        {
            this.ReflectionMap = default;
            this.WaveHeight = default;
            this.WaveSpeed0 = default;
            this.WaveSpeed1 = default;
            this.WaterReflectiveness = default;

        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading EffectDeferredLiquid");

            this.ReflectionMap = reader.ReadString(); /* ER */
            this.WaveHeight = reader.ReadSingle();
            this.WaveSpeed0 = Vec2.Read(reader, logger);
            this.WaveSpeed1 = Vec2.Read(reader, logger);
            this.WaterReflectiveness = reader.ReadSingle();
            this.BottomColor = Vec3.Read(reader, logger);
            this.DeepBottomColor = Vec3.Read(reader, logger);
            this.WaterEmissiveAmount = reader.ReadSingle();
            this.WaterSpecAmount = reader.ReadSingle();
            this.WaterSpecPower = reader.ReadSingle();
            this.BottomTexture = reader.ReadString(); /* ER */
            this.WaterNormalMap = reader.ReadString(); /* ER */
            this.IceReflectiveness = reader.ReadSingle();
            this.IceColor = Vec3.Read(reader, logger);
            this.IceEmissiveAmount = reader.ReadSingle();
            this.IceSpecAmount = reader.ReadSingle();
            this.IceSpecPower = reader.ReadSingle();
            this.IceDiffuseMap = reader.ReadString(); /* ER */
            this.IceNormalMap = reader.ReadString(); /* ER */

            logger?.Log(2, $" - Reflection Map  : {this.ReflectionMap}");
            logger?.Log(2, $" - Bottom Texture  : {this.BottomTexture}");
            logger?.Log(2, $" - Normal Map      : {this.WaterNormalMap}");
            logger?.Log(2, $" - Ice Diffuse Map : {this.IceDiffuseMap}");
            logger?.Log(2, $" - Ice Normal  Map : {this.IceNormalMap}");
        }

        public static new EffectDeferredLiquid Read(MBinaryReader reader, DebugLogger logger = null)
        {
            EffectDeferredLiquid ans = new EffectDeferredLiquid();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing EffectDeferredLiquid");

            writer.Write(this.ReflectionMap); /* ER */
            writer.Write(this.WaveHeight);
            this.WaveSpeed0.WriteInstance(writer, logger);
            this.WaveSpeed1.WriteInstance(writer, logger);
            writer.Write(this.WaterReflectiveness);
            this.BottomColor.WriteInstance(writer, logger);
            this.DeepBottomColor.WriteInstance(writer, logger);
            writer.Write(this.WaterEmissiveAmount);
            writer.Write(this.WaterSpecAmount);
            writer.Write(this.WaterSpecPower);
            writer.Write(this.BottomTexture); /* ER */
            writer.Write(this.WaterNormalMap); /* ER */
            writer.Write(this.IceReflectiveness);
            this.IceColor.WriteInstance(writer, logger);
            writer.Write(this.IceEmissiveAmount);
            writer.Write(this.IceSpecAmount);
            writer.Write(this.IceSpecPower);
            writer.Write(this.IceDiffuseMap); /* ER */
            writer.Write(this.IceNormalMap); /* ER */
        }

        public override ContentTypeReader GetObjectContentTypeReader()
        {
            return new ContentTypeReader("PolygonHead.Pipeline.RenderDeferredLiquidEffectReader, PolygonHead", 0);
        }

        #endregion
    }
}
