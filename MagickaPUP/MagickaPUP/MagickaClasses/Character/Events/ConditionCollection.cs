using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Events
{
    public class ConditionCollection
    {
        #region Variables

        public int numEvents { get; set; }
        public EventCollection[] eventCollection { get; set; }

        #endregion

        #region Constructor

        public ConditionCollection()
        {
            this.numEvents = 0;
            this.eventCollection = new EventCollection[0];
        }

        public ConditionCollection(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading ConditionCollection...");

            this.numEvents = reader.ReadInt32();
            this.eventCollection = new EventCollection[this.numEvents];
            for (int i = 0; i < this.numEvents; ++i)
                this.eventCollection[i] = new EventCollection(reader, logger);
        }

        #endregion

        #region PublicMethods

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing ConditionCollection...");

            writer.Write(this.numEvents);
            foreach (var eventCollection in this.eventCollection)
                eventCollection.Write(writer, logger);
        }

        #endregion
    }
}
