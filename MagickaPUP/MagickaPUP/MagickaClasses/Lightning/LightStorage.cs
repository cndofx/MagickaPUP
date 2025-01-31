using MagickaPUP.Utility.IO;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Lightning
{
    public class LightStorage : XnaObject
    {
        #region Variables

        public string name { get; set; }
        public Matrix position { get; set; }

        #endregion

        #region Constructor

        public LightStorage()
        {
            this.name = default;
            this.position = new Matrix();
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading LightStorage...");

            this.name = reader.ReadString();
            this.position = Matrix.Read(reader, null);
        }

        public static LightStorage Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new LightStorage();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing LightStorage...");

            writer.Write(this.name);
            this.position.WriteInstance(writer, null);
        }

        #endregion
    }
}
