using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Readers
{
    public struct ContentTypeReader
    {
        public string Name { get; set; }
        public int Version { get; set; }

        public ContentTypeReader(string name = "", int version = 0)
        {
            this.Name = name;
            this.Version = version;
        }

        public ContentTypeReader(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading ContentTypeReader..."); // TODO : Maybe get rid of this? The other logging is enough, I think...

            this.Name = reader.ReadString();
            this.Version = reader.ReadInt32();

            Log(logger);
        }

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing ContentTypeReader..."); // TODO : Maybe get rid of this? The other logging is enough, I think...

            writer.Write(this.Name);
            writer.Write(this.Version);

            Log(logger);
        }

        private void Log(DebugLogger logger = null)
        {
            // NOTE : This is only encapsulated here because the message is identical on both places, once variable logging is standardised on the logger, we can get
            // rid of this function and clean things up...
            // TODO : Change logging level to something more adequate later on, maybe?
            logger?.Log(2, $"ContentTypeReader : {{\"Name\" : \"{this.Name}\", \"Version\" : {this.Version}}}");
        }
    }
}
