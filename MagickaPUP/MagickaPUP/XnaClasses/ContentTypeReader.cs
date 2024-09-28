using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses
{
    public class ContentTypeReader : XnaObject
    {
        #region Variables

        public string Name { get; set; }
        public int Version { get; set; }

        #endregion

        #region Constructors

        public ContentTypeReader(string name = "__none__", int version = -1)
        {
            this.Name = name;
            this.Version = version;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            this.Name = reader.ReadString();
            this.Version = reader.ReadInt32();

            logger.Log(1, $"Reader : {{name = \"{Name}\", version = {Version}}}");
        }

        public static ContentTypeReader Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new ContentTypeReader();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing ContentTypeReader...");

            writer.Write(this.Name);
            writer.Write(this.Version);
        }

        #endregion
    }
}
