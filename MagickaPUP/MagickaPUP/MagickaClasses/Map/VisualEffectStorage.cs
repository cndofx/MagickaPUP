using MagickaPUP.Utility.IO;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Map
{
    // This class stores information about visual effects that will be used on the map.
    // "Visual effects" are what is commonly referred to as "particle effects".
    public class VisualEffectStorage : XnaObject
    {
        #region Variables

        public string id { get; set; }
        public Vec3 vector1 { get; set; }
        public Vec3 vector2 { get; set; }
        public float range { get; set; }
        public string name { get; set; }

        #endregion

        #region Constructor

        public VisualEffectStorage()
        {
            this.id = default;
            this.vector1 = new Vec3();
            this.vector2 = new Vec3();
            this.range = 0.0f;
            this.name = default;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading VisualEffectStorage...");

            this.id = reader.ReadString();
            this.vector1 = Vec3.Read(reader, logger);
            this.vector2 = Vec3.Read(reader, logger);
            this.range = reader.ReadSingle();
            this.name = reader.ReadString();
        }

        public static VisualEffectStorage Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new VisualEffectStorage();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing VisualEffectStorage");

            writer.Write(this.id);
            this.vector1.WriteInstance(writer, null);
            this.vector2.WriteInstance(writer, null);
            writer.Write(this.range);
            writer.Write(this.name);
        }

        #endregion
    }
}
