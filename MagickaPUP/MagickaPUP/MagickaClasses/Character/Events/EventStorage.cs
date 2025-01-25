using MagickaPUP.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagickaPUP.Utility.Exceptions;

namespace MagickaPUP.MagickaClasses.Character.Events
{
    // This class is a major pain in the fucking asshole and I will not miss having had to work on it.
    // Whoever programmed Magicka's event system should be in jail. That is all I can say.
    public class EventStorage : XnaObject
    {
        #region Variables

        public EventType EventType { get; set; }
        public MagickaEvent Event { get; set; }

        #endregion

        #region Constructor

        public EventStorage()
        {
            this.EventType = default;
            this.Event = default;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading EventStorage...");

            this.EventType = (EventType)reader.ReadByte();
            switch (this.EventType)
            {
                // TODO : Implement cases for each and every single type of MagickaEvent.
                default:
                    throw new MagickaLoadException($"Event type \"{((byte)this.EventType)}\" not recognised as a valid Magicka Event!");
                    break;
            }
        }

        public static EventStorage Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new EventStorage();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing EventStorage...");
            throw new NotImplementedException("Write EventStorage is not implemented yet!");
        }

        #endregion
    }
}
