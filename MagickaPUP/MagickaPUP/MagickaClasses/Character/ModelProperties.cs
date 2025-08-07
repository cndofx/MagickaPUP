using MagickaPUP.Utility.IO;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character
{
    public class ModelProperties
    {
        #region Variables

        public string Model { get; set; } // External Reference
        public float Scale { get; set; }
        public Vec3 Tint { get; set; }

        #endregion

        #region Constructor

        public ModelProperties()
        {
            this.Model = string.Empty;
            this.Scale = 1.0f;
            this.Tint = new Vec3(1.0f, 1.0f, 1.0f);
        }

        public ModelProperties(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading ModelProperties...");

            this.Model = reader.ReadString(); // ER
            this.Scale = reader.ReadSingle();
            this.Tint = Vec3.Read(reader, logger);
        }

        #endregion

        #region PublicMethods

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing ModelProperties...");

            writer.Write(this.Model);
            writer.Write(this.Scale);
            this.Tint.WriteInstance(writer, logger);
        }

        #endregion
    }
}
