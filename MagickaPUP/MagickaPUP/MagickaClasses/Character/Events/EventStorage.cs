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
    // Ok, maybe it wasn't THAT bad... lol...
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
                // NOTE : We could do this with an array and some function pointers, but we're going with the switch-case ladder that Magicka does for consistency.
                case EventType.Damage:
                    this.Event = DamageEvent.Read(reader, logger);
                    break;
                case EventType.Splash:
                    this.Event = SplashEvent.Read(reader, logger);
                    break;
                case EventType.Sound:
                    this.Event = PlaySoundEvent.Read(reader, logger);
                    break;
                case EventType.Effect:
                    this.Event = PlayEffectEvent.Read(reader, logger);
                    break;
                case EventType.Remove:
                    this.Event = RemoveEvent.Read(reader, logger);
                    break;
                case EventType.CameraShake:
                    this.Event = CameraShakeEvent.Read(reader, logger);
                    break;
                case EventType.Decal:
                    this.Event = SpawnDecalEvent.Read(reader, logger);
                    break;
                case EventType.Blast:
                    this.Event = BlastEvent.Read(reader, logger);
                    break;
                case EventType.Spawn:
                    this.Event = SpawnEvent.Read(reader, logger);
                    break;
                case EventType.Overkill:
                    this.Event = OverKillEvent.Read(reader, logger);
                    break;
                case EventType.SpawnGibs:
                    this.Event = SpawnGibsEvent.Read(reader, logger);
                    break;
                case EventType.SpawnItem:
                    this.Event = SpawnItemEvent.Read(reader, logger);
                    break;
                case EventType.SpawnMagick:
                    this.Event = SpawnMagickEvent.Read(reader, logger);
                    break;
                case EventType.SpawnMissile:
                    this.Event = SpawnMissileEvent.Read(reader, logger);
                    break;
                case EventType.Light:
                    this.Event = LightEvent.Read(reader, logger);
                    break;
                case EventType.CastMagick:
                    this.Event = CastMagickEvent.Read(reader, logger);
                    break;
                case EventType.DamageOwner:
                    this.Event = DamageOwnerEvent.Read(reader, logger);
                    break;
                case EventType.Callback:
                    this.Event = CallbackEvent.Read(reader, logger);
                    break;
                default:
                    throw new MagickaLoadException($"Event type \"{((byte)this.EventType)}\" not recognised as a valid Magicka Event!");
                    break; // lol
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
