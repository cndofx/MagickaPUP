using MagickaPUP.Utility.IO;
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
    // Ok, maybe it wasn't THAT bad... lol...
    public class EventStorage
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

        public EventStorage(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading EventStorage...");

            this.EventType = (EventType)reader.ReadByte();
            switch (this.EventType)
            {
                // NOTE : We could do this with an array and some function pointers, but we're going with the switch-case ladder that Magicka does for consistency.
                case EventType.Damage:
                    this.Event = new DamageEvent(reader, logger);
                    break;
                case EventType.Splash:
                    this.Event = new SplashEvent(reader, logger);
                    break;
                case EventType.Sound:
                    this.Event = new PlaySoundEvent(reader, logger);
                    break;
                case EventType.Effect:
                    this.Event = new PlayEffectEvent(reader, logger);
                    break;
                case EventType.Remove:
                    this.Event = new RemoveEvent(reader, logger);
                    break;
                case EventType.CameraShake:
                    this.Event = new CameraShakeEvent(reader, logger);
                    break;
                case EventType.Decal:
                    this.Event = new SpawnDecalEvent(reader, logger);
                    break;
                case EventType.Blast:
                    this.Event = new BlastEvent(reader, logger);
                    break;
                case EventType.Spawn:
                    this.Event = new SpawnEvent(reader, logger);
                    break;
                case EventType.Overkill:
                    this.Event = new OverKillEvent(reader, logger);
                    break;
                case EventType.SpawnGibs:
                    this.Event = new SpawnGibsEvent(reader, logger);
                    break;
                case EventType.SpawnItem:
                    this.Event = new SpawnItemEvent(reader, logger);
                    break;
                case EventType.SpawnMagick:
                    this.Event = new SpawnMagickEvent(reader, logger);
                    break;
                case EventType.SpawnMissile:
                    this.Event = new SpawnMissileEvent(reader, logger);
                    break;
                case EventType.Light:
                    this.Event = new LightEvent(reader, logger);
                    break;
                case EventType.CastMagick:
                    this.Event = new CastMagickEvent(reader, logger);
                    break;
                case EventType.DamageOwner:
                    this.Event = new DamageOwnerEvent(reader, logger);
                    break;
                case EventType.Callback:
                    this.Event = new CallbackEvent(reader, logger);
                    break;
                default:
                    throw new MagickaReadException($"Event type \"{((byte)this.EventType)}\" not recognised as a valid Magicka Event!");
                    break; // lol
            }
        }

        #endregion

        #region PublicMethods

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing EventStorage...");

            writer.Write((byte)this.EventType);
            this.Event.Write(writer, logger);
        }

        #endregion
    }
}
