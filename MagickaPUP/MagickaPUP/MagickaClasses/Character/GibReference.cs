using MagickaPUP.IO;
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
            logger?.Log(1, "Read GibReference not implemented yet!");
            this.model = reader.ReadString();
            this.mass = reader.ReadSingle();
            this.scale = reader.ReadSingle();
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing GibReference...");
            throw new NotImplementedException("Write GibReference not implemented yet!");
        }

        #endregion

        #region PrivateMethods
        #endregion
    }
}
