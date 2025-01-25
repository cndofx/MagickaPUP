using MagickaPUP.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Events
{
    public class EventCollection : XnaObject
    {
        #region Variables

        public EventCondition EventCondition { get; set; }
        public int NumEvents { get; set; }
        public EventStorage[] Events { get; set; }

        #endregion

        #region Constructor

        public EventCollection()
        { }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading EventCollection...");

            this.EventCondition = EventCondition.Read(reader, logger);
            this.NumEvents = reader.ReadInt32();
            for (int i = 0; i < this.NumEvents; ++i)
                this.Events[i] = EventStorage.Read(reader, logger);
        }

        public static EventCollection Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new EventCollection();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing EventCollection...");
            throw new NotImplementedException("Write EventCollection not implemented yet!");
        }

        #endregion
    }
}
