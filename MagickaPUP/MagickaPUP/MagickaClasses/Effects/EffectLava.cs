using MagickaPUP.IO;
using MagickaPUP.MagickaClasses.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Effects
{
    public class EffectLava : Effect
    {
        #region Variables

        public float MaskDistortion { get; set; }
        public Vec2 Speed0 { get; set; }
        public Vec2 Speed1 { get; set; }
        public float LavaHotEmissiveAmount { get; set; }
        public float LavaColdEmissiveAmount { get; set; }
        public float LavaSpecAmount { get; set; }
        public float LavaSpecPower { get; set; }
        public float TempFrequency { get; set; }
        public string ToneMap { get; set; } /* ER */
        public string TempMap { get; set; } /* ER */
        public string MaskMap { get; set; } /* ER */
        public Vec3 RockColor { get; set; }
        public float RockEmissiveAmount { get; set; }
        public float RockSpecAmount { get; set; }
        public float RockSpecPower { get; set; }
        public float RockNormalPower { get; set; }
        public string RockTexture { get; set; } /* ER */
        public string RockNormalMap { get; set; } /* ER */

        #endregion

        #region Constructor

        public EffectLava()
        {
            this.MaskDistortion = default;
            this.Speed0 = default;
            this.Speed1 = default;
            this.LavaHotEmissiveAmount = default;
            this.LavaColdEmissiveAmount = default;
            this.LavaSpecAmount = default;
            this.LavaSpecPower = default;
            this.TempFrequency = default;
            this.ToneMap = default; /* ER */
            this.TempMap = default; /* ER */
            this.MaskMap = default; /* ER */
            this.RockColor = default;
            this.RockEmissiveAmount = default;
            this.RockSpecAmount = default;
            this.RockSpecPower = default;
            this.RockNormalPower = default;
            this.RockTexture = default; /* ER */
            this.RockNormalMap = default; /* ER */
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading EffectLava...");

            this.MaskDistortion = reader.ReadSingle();
            this.Speed0 = Vec2.Read(reader, logger);
            this.Speed1 = Vec2.Read(reader, logger);
            this.LavaHotEmissiveAmount = reader.ReadSingle();
            this.LavaColdEmissiveAmount = reader.ReadSingle();
            this.LavaSpecAmount = reader.ReadSingle();
            this.LavaSpecPower = reader.ReadSingle();
            this.TempFrequency = reader.ReadSingle();
            this.ToneMap = reader.ReadString(); /* ER */
            this.TempMap = reader.ReadString(); /* ER */
            this.MaskMap = reader.ReadString(); /* ER */
            this.RockColor = Vec3.Read(reader, logger);
            this.RockEmissiveAmount = reader.ReadSingle();
            this.RockSpecAmount = reader.ReadSingle();
            this.RockSpecPower = reader.ReadSingle();
            this.RockNormalPower = reader.ReadSingle();
            this.RockTexture = reader.ReadString(); /* ER */
            this.RockNormalMap = reader.ReadString(); /* ER */

            logger?.Log(2, $" - Tone Map : {this.ToneMap}");
            logger?.Log(2, $" - Temp Map : {this.TempMap}");
            logger?.Log(2, $" - Mask Map : {this.MaskMap}");
            logger?.Log(2, $" - Rock Texture : {this.RockTexture}");
            logger?.Log(2, $" - Rock Normal  : {this.RockNormalMap}");
        }

        public static new EffectLava Read(MBinaryReader reader, DebugLogger logger = null)
        {
            EffectLava ans = new EffectLava();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing EffectLava...");

            writer.Write(MaskDistortion);
            Speed0.WriteInstance(writer, logger);
            Speed1.WriteInstance(writer, logger);
            writer.Write(LavaHotEmissiveAmount);
            writer.Write(LavaColdEmissiveAmount);
            writer.Write(LavaSpecAmount);
            writer.Write(LavaSpecPower);
            writer.Write(TempFrequency);
            writer.Write(ToneMap); /* ER */
            writer.Write(TempMap); /* ER */
            writer.Write(MaskMap); /* ER */
            RockColor.WriteInstance(writer, logger);
            writer.Write(RockEmissiveAmount);
            writer.Write(RockSpecAmount);
            writer.Write(RockSpecPower);
            writer.Write(RockNormalPower);
            writer.Write(RockTexture); /* ER */
            writer.Write(RockNormalMap); /* ER */
        }

        public override string GetReaderName()
        {
            return "PolygonHead.Pipeline.LavaEffectReader, PolygonHead, Version=1.0.0.0, Culture=neutral";
        }

        #endregion
    }
}
