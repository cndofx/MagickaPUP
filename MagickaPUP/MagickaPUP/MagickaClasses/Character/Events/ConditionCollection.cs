using MagickaPUP.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Events
{
    public class ConditionCollection : XnaObject
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

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading ConditionCollection...");

            this.numEvents = reader.ReadInt32();
            this.eventCollection = new EventCollection[this.numEvents];
            for (int i = 0; i < this.numEvents; ++i)
                this.eventCollection[i] = EventCollection.Read(reader, logger);
        }

        public static ConditionCollection Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new ConditionCollection();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing ConditionCollection...");
            throw new NotImplementedException("Write ConditionCollection not implemented yet!");
        }

        #endregion
    }
}
