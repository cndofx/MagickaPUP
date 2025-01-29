using MagickaPUP.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Events
{
    public class EventCollection
    {
        #region Variables

        public EventCondition EventCondition { get; set; }
        public int NumEvents { get; set; }
        public EventStorage[] Events { get; set; }

        #endregion

        #region Constructor

        public EventCollection()
        {
            this.EventCondition = new EventCondition();
            this.NumEvents = 0;
            this.Events = new EventStorage[0];
        }

        public EventCollection(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading EventCollection...");

            this.EventCondition = new EventCondition(reader, logger);
            this.NumEvents = reader.ReadInt32();
            this.Events = new EventStorage[this.NumEvents];
            for (int i = 0; i < this.NumEvents; ++i)
                this.Events[i] = new EventStorage(reader, logger);
        }

        #endregion

        #region PublicMethods

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing EventCollection...");

            this.EventCondition.Write(writer, logger);
            writer.Write(this.NumEvents);
            foreach (var currentEvent in this.Events)
                currentEvent.Write(writer, logger);
        }

        #endregion
    }
}
