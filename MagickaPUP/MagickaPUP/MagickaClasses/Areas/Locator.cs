using MagickaPUP.IO;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Areas
{
    public class Locator : XnaObject
    {
        #region Variables

        public string Name { get; set; }
        public Matrix Transform { get; set; }
        public float Radius { get; set; }

        #endregion

        #region Constructor

        public Locator()
        {
            this.Name = "__none__";
            this.Transform = new Matrix();
            this.Radius = 0.0f;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Locator...");

            this.Name = reader.ReadString();
            this.Transform = Matrix.Read(reader);
            this.Radius = reader.ReadSingle();

            logger?.Log(2, $" - Name   : \"{this.Name}\"");
            logger?.Log(2, $" - Radius : {this.Radius}");
        }

        public static Locator Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new Locator();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Locator...");

            writer.Write(this.Name);
            this.Transform.WriteInstance(writer, logger);
            writer.Write(this.Radius);

            logger?.Log(2, $" - Name   : \"{this.Name}\"");
            logger?.Log(2, $" - Radius : {this.Radius}");
        }

        #endregion
    }
}
