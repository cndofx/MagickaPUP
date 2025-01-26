using MagickaPUP.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using MagickaPUP.MagickaClasses.Data;
using System.Runtime.Remoting.Messaging;

namespace MagickaPUP.MagickaClasses.Character.Events
{
    #region Commnets - MagickaEvent

    // NOTE : This is the base class for an Event, so it does not contain any code, it just exists as a polymorphic proxy of sorts to make working with
    // C#'s type system easier... kinda similar to how the Effects classes worked, only that those were also polymorphic through C#'s inheritance system
    // in Magicka, while this system simply has repeated structs in memory for each event type in Magicka, and I really don't wanna do it like that, cause
    // that would bloat the JSON files a fucking ton.
    
    // NOTE : A good use for this class is to act as a trap to trigger exceptions when reading invalid event types, so that's good and all I suppose.

    #endregion
    public class MagickaEvent : XnaObject
    {
    }

    // NOTE : All of the event types are implemented with their own classes here in this file.

    // TODO : Implement all of the specific Magicka Event types.
    //  - Implement reading
    //  - Implement writing

    public class DamageEvent : MagickaEvent
    {
        #region Variables

        public AttackProperties AttackProperties { get; set; }
        public Elements Element { get; set; }
        public float Amount { get; set; }
        public float Magnitude { get; set; }
        public bool IsVelocityBased { get; set; }

        #endregion

        #region Constructor

        public DamageEvent()
        {
            this.AttackProperties = default;
            this.Element = default;
            this.Amount = 0.0f;
            this.Magnitude = 0.0f;
            this.IsVelocityBased = false;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading DamageEvent...");

            this.AttackProperties = (AttackProperties)reader.ReadInt32();
            this.Element = (Elements)reader.ReadInt32();
            this.Amount = reader.ReadSingle();
            this.Magnitude = reader.ReadSingle();
            this.IsVelocityBased = reader.ReadBoolean();
        }

        public static DamageEvent Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new DamageEvent();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing DamageEvent...");

            writer.Write((int)this.AttackProperties);
            writer.Write((int)this.Element);
            writer.Write(this.Amount);
            writer.Write(this.Magnitude);
            writer.Write(this.IsVelocityBased);
        }

        #endregion
    }

    public class SplashEvent : MagickaEvent
    {
        public static SplashEvent Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new SplashEvent();
            ans.ReadInstance(reader, logger);
            return ans;
        }
    }

    public class PlaySoundEvent : MagickaEvent
    {
        public static PlaySoundEvent Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new PlaySoundEvent();
            ans.ReadInstance(reader, logger);
            return ans;
        }
    }

    public class PlayEffectEvent : MagickaEvent
    {
        public static PlayEffectEvent Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new PlayEffectEvent();
            ans.ReadInstance(reader, logger);
            return ans;
        }
    }

    public class RemoveEvent : MagickaEvent
    {
        public static RemoveEvent Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new RemoveEvent();
            ans.ReadInstance(reader, logger);
            return ans;
        }
    }

    public class CameraShakeEvent : MagickaEvent
    {
        public static CameraShakeEvent Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new CameraShakeEvent();
            ans.ReadInstance(reader, logger);
            return ans;
        }
    }

    public class SpawnDecalEvent : MagickaEvent
    {
        public static SpawnDecalEvent Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new SpawnDecalEvent();
            ans.ReadInstance(reader, logger);
            return ans;
        }
    }

    public class BlastEvent : MagickaEvent
    {
        public static BlastEvent Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new BlastEvent();
            ans.ReadInstance(reader, logger);
            return ans;
        }
    }

    public class SpawnEvent : MagickaEvent
    {
        public static SpawnEvent Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new SpawnEvent();
            ans.ReadInstance(reader, logger);
            return ans;
        }
    }

    public class OverKillEvent : MagickaEvent
    {
        public static OverKillEvent Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new OverKillEvent();
            ans.ReadInstance(reader, logger);
            return ans;
        }
    }

    public class SpawnGibsEvent : MagickaEvent
    {
        public static SpawnGibsEvent Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new SpawnGibsEvent();
            ans.ReadInstance(reader, logger);
            return ans;
        }
    }

    public class SpawnItemEvent : MagickaEvent
    {
        public static SpawnItemEvent Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new SpawnItemEvent();
            ans.ReadInstance(reader, logger);
            return ans;
        }
    }

    public class SpawnMagickEvent : MagickaEvent
    {
        public static SpawnMagickEvent Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new SpawnMagickEvent();
            ans.ReadInstance(reader, logger);
            return ans;
        }
    }

    public class SpawnMissileEvent : MagickaEvent
    {
        public static SpawnMissileEvent Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new SpawnMissileEvent();
            ans.ReadInstance(reader, logger);
            return ans;
        }
    }

    public class LightEvent : MagickaEvent
    {
        public static LightEvent Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new LightEvent();
            ans.ReadInstance(reader, logger);
            return ans;
        }
    }

    public class CastMagickEvent : MagickaEvent
    {
        public static CastMagickEvent Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new CastMagickEvent();
            ans.ReadInstance(reader, logger);
            return ans;
        }
    }

    public class DamageOwnerEvent : MagickaEvent
    {
        public static DamageOwnerEvent Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new DamageOwnerEvent();
            ans.ReadInstance(reader, logger);
            return ans;
        }
    }

    public class CallbackEvent : MagickaEvent
    {
        public static CallbackEvent Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new CallbackEvent();
            ans.ReadInstance(reader, logger);
            return ans;
        }
    }

}
