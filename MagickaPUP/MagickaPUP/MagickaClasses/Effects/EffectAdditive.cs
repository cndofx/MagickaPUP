using MagickaPUP.Utility.IO;
using MagickaPUP.MagickaClasses.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Effects
{
    // TODO : Make sure that this is correctly implemented (need to implement animated model reading for that)
    public class EffectAdditive : Effect
    {
        #region Variables

        public Vec3 colorTint { get; set; } // Within Magicka's code, this get casted to a Vec4 where the last element is 1.0f always, but the reader reads a Vec3, so we don't care about that detail from this end for the most part.
        public bool vertexColorEnabled { get; set; }
        public bool textureEnabled { get; set; }
        public string texture { get; set; } /* ER */

        #endregion

        #region Constructor

        public EffectAdditive()
        {
            this.colorTint = new Vec3();
            this.vertexColorEnabled = false;
            this.textureEnabled = false;
            this.texture = "__none__";
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading EffectAdditive...");

            this.colorTint.ReadInstance(reader, null);
            this.vertexColorEnabled = reader.ReadBoolean();
            this.textureEnabled = reader.ReadBoolean();
            this.texture = reader.ReadString();

            logger?.Log(2, $" - Vertex Color Enabled : {this.vertexColorEnabled}");
            logger?.Log(2, $" - Vertex Color : < {this.colorTint.x}, {this.colorTint.y}, {this.colorTint.z} >");
            logger?.Log(2, $" - Texture Enabled : {this.textureEnabled}");
            logger?.Log(2, $" - Texture : \"{this.texture}\"");
        }

        public static new EffectAdditive Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new EffectAdditive();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing EffectAdditive...");

            this.colorTint.WriteInstance(writer, null);
            writer.Write(this.vertexColorEnabled);
            writer.Write(this.textureEnabled);
            writer.Write(this.texture);
        }

        public override string GetReaderName()
        {
            return "PolygonHead.Pipeline.AdditiveEffectReader, PolygonHead, Version=1.0.0.0, Culture=neutral";
        }

        #endregion
    }
}
