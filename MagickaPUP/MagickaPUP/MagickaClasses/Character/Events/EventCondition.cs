using MagickaPUP.Utility.IO;
using MagickaPUP.MagickaClasses.Data;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Events
{
    public class EventCondition
    {
        #region Variables

        public EventConditionType EventConditionType { get; set; }
        public int HitPoints { get; set; }
        public Elements Elements { get; set; }
        public float Threshold { get; set; } // NOTE : Is this the activation / minimum utility value to switch to this action for some kind of utility system? maybe?
        public float Time{ get; set; } // NOTE : Is this the time that the action lasts or some cooldown until the event can take place again? or the repeat time?
        public bool Repeat { get; set; }

        #endregion

        #region Constructor

        public EventCondition()
        {
            this.EventConditionType = default;
            this.HitPoints = 100;
            this.Elements = default;
            this.Threshold = 0.0f;
            this.Time = 0.0f;
            this.Repeat = false;
        }

        public EventCondition(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading EventCondition...");

            this.EventConditionType = (EventConditionType)reader.ReadByte();
            this.HitPoints = reader.ReadInt32(); // NOTE : The HitPoints value casted to a float when loaded into memory in Magicka, but the value stored within the XNB file is an integer.
            this.Elements = (Elements)reader.ReadInt32();
            this.Threshold = reader.ReadSingle();
            this.Time = reader.ReadSingle();

            this.Repeat = reader.ReadBoolean(); // NOTE : Within Magicka's code, this value is read outside of this read method, right after calling the read method, so it's literally the exact same thing tbh... I just put it in here because it makes things easier for me lol.
        }

        #endregion

        #region PublicMethods

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing EventCondition...");

            writer.Write((byte)this.EventConditionType);
            writer.Write(this.HitPoints);
            writer.Write((int)this.Elements);
            writer.Write(this.Threshold);
            writer.Write(this.Time);

            writer.Write(this.Repeat);
        }

        #endregion
    }
}
