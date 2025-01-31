using MagickaPUP.Utility.IO;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character
{
    public class ModelProperties : XnaObject
    {
        #region Variables

        public string model { get; set; }
        public float scale { get; set; }
        public Vec3 tint { get; set; }

        #endregion

        #region Constructor

        public ModelProperties()
        {
            this.model = default;
            this.scale = 1.0f;
            this.tint = new Vec3(1.0f, 1.0f, 1.0f);
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading ModelProperties...");

            this.model = reader.ReadString();
            this.scale = reader.ReadSingle();
            this.tint = Vec3.Read(reader, logger);
        }

        public static ModelProperties Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new ModelProperties();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing ModelProperties...");

            writer.Write(this.model);
            writer.Write(this.scale);
            this.tint.WriteInstance(writer, logger);
        }

        #endregion
    }
}
