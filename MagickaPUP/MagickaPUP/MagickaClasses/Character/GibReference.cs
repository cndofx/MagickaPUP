using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character
{
    public class GibReference : XnaObject
    {
        #region Variables

        public string model { get; set; } /* ER (External Reference) */
        public float mass { get; set; }
        public float scale { get; set; }

        #endregion

        #region Constructor

        public GibReference()
        {
            this.model = default;
            this.mass = 1.0f;
            this.scale = 1.0f;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading GibReference...");
            this.model = reader.ReadString();
            this.mass = reader.ReadSingle();
            this.scale = reader.ReadSingle();
        }

        public static GibReference Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new GibReference();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing GibReference...");
            writer.Write(this.model);
            writer.Write(this.mass);
            writer.Write(this.scale);
        }

        #endregion

        #region PrivateMethods
        #endregion
    }
}
