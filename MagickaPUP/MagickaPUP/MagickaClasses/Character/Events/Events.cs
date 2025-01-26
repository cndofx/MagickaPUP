using MagickaPUP.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using MagickaPUP.MagickaClasses.Data;
using MagickaPUP.Utility.Exceptions;
using MagickaPUP.MagickaClasses.Audio;
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

        public AttackProperties AttackProperty { get; set; }
        public Elements Element { get; set; }
        public float Amount { get; set; }
        public float Magnitude { get; set; }
        public bool IsVelocityBased { get; set; }

        #endregion

        #region Constructor

        public DamageEvent()
        {
            this.AttackProperty = default;
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

            this.AttackProperty = (AttackProperties)reader.ReadInt32();
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

            writer.Write((int)this.AttackProperty);
            writer.Write((int)this.Element);
            writer.Write(this.Amount);
            writer.Write(this.Magnitude);
            writer.Write(this.IsVelocityBased);
        }

        #endregion
    }

    public class SplashEvent : MagickaEvent
    {
        #region Constants

        // This exception message is basically the same for both read and write operations, so we can just reuse it.
        private static readonly string EXCEPTION_MSG = $"Magicka does not support SplashEvents with a Radius value smaller than float.Epsilon ({float.Epsilon})";

        #endregion

        #region Variables

        public AttackProperties AttackProperty { get; set; }
        public Elements Element { get; set; }
        public int Amount { get; set; } // NOTE : Take a look at the read code for further context about the data type of this variable...
        public float Magnitude { get; set; }
        public float Radius { get; set; }

        #endregion

        #region Constructor

        public SplashEvent()
        {
            this.AttackProperty = default;
            this.Element = default;
            this.Amount = 0;
            this.Magnitude = 0.0f;
            this.Radius = 1.0f;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading SplashEvent...");

            this.AttackProperty = (AttackProperties)reader.ReadInt32();
            this.Element = (Elements)reader.ReadInt32();
            this.Amount = reader.ReadInt32(); // WTF, why does Magicka store this value as an i32 within XNB files for SplashEvents but not for DamageEvents???
            this.Magnitude = reader.ReadSingle();
            this.Radius = reader.ReadSingle();

            if (this.Radius <= float.Epsilon) // NOTE : Magicka hardcodes the value as 1E-45f, but we can just use float.Epsilon here. This makes sense tho, we can't have a radius that is 0 or less than 0, and the smartest comparison is against the smallest possible valid value for a floating point number.
                throw new MagickaLoadException(EXCEPTION_MSG);
        }

        public static SplashEvent Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new SplashEvent();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing SplashEvent...");

            // Rather than throwing an exception, we can make sure to always correct the Radius by setting it to a valid value before writing it.
            // Maybe it's not the best way of doing things and we should just error out, but I feel like compilation should work as long as no
            // critical failure takes place, so we can take a few liberties here and there and correct the user's invalid input just a tiny bit...
            if (this.Radius <= float.Epsilon)
                this.Radius = 0.01f;

            writer.Write((int)this.AttackProperty);
            writer.Write((int)this.Element);
            writer.Write(this.Amount);
            writer.Write(this.Magnitude);
            writer.Write(this.Radius);
        }

        #endregion
    }

    public class PlaySoundEvent : MagickaEvent
    {
        #region Variables

        public Banks SoundBank { get; set; }
        public string SoundName { get; set; }
        public float Magnitude { get; set; }
        public bool StopOnRemove { get; set; }

        #endregion

        #region Constructor

        public PlaySoundEvent()
        {
            this.SoundBank = default;
            this.SoundName = default;
            this.Magnitude = 1.0f;
            this.StopOnRemove = true;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading PlaySoundEvent...");

            this.SoundBank = (Banks)reader.ReadInt32();
            this.SoundName = reader.ReadString();
            this.Magnitude = reader.ReadSingle();
            this.StopOnRemove = reader.ReadBoolean();
        }

        public static PlaySoundEvent Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new PlaySoundEvent();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing PlaySoundEvent...");

            writer.Write((int)this.SoundBank);
            writer.Write(this.SoundName);
            writer.Write(this.Magnitude);
            writer.Write(this.StopOnRemove);
        }

        #endregion
    }

    public class PlayEffectEvent : MagickaEvent
    {
        #region Variables

        public bool FollowTarget { get; set; }
        public bool AlignWithWorld { get; set; }
        public string EffectName { get; set; }

        #endregion

        #region Constructor

        public PlayEffectEvent()
        {
            this.FollowTarget = false;
            this.AlignWithWorld = false;
            this.EffectName = default;
        }
        
        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading PlayEffectEvent...");

            this.FollowTarget = reader.ReadBoolean();
            this.AlignWithWorld = reader.ReadBoolean();
            this.EffectName = reader.ReadString();
        }

        public static PlayEffectEvent Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new PlayEffectEvent();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing PlayEffectEvent...");

            writer.Write(this.FollowTarget);
            writer.Write(this.AlignWithWorld);
            writer.Write(this.EffectName);
        }

        #endregion
    }

    public class RemoveEvent : MagickaEvent
    {
        #region Variables

        public int Bounce { get; set; }

        #endregion

        #region Constructor

        public RemoveEvent()
        {
            this.Bounce = 1;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading RemoveEvent...");

            this.Bounce = reader.ReadInt32();
        }

        public static RemoveEvent Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new RemoveEvent();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing RemoveEvent...");

            writer.Write(this.Bounce);
        }

        #endregion
    }

    public class CameraShakeEvent : MagickaEvent
    {
        #region Variables

        public float Duration { get; set; }
        public float Magnitude { get; set; }
        public bool FromPosition { get; set; }

        #endregion

        #region Constructor

        public CameraShakeEvent()
        {
            this.Duration = 1.0f;
            this.Magnitude = 1.0f;
            this.FromPosition = true;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading CameraShakeEvent...");

            this.Duration = reader.ReadSingle();
            this.Magnitude = reader.ReadSingle();
            this.FromPosition = reader.ReadBoolean();
        }

        public static CameraShakeEvent Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new CameraShakeEvent();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing CameraShakeEvent...");

            writer.Write(Duration);
            writer.Write(Magnitude);
            writer.Write(FromPosition);
        }

        #endregion
    }

    public class SpawnDecalEvent : MagickaEvent
    {
        #region Variables

        public int Num1 { get; set; }
        public int Num2 { get; set; }
        public int Scale { get; set; }

        #endregion

        #region Constructor

        public SpawnDecalEvent()
        {
            this.Num1 = 0;
            this.Num2 = 0;
            this.Scale = 0;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading SpawnDecalEvent...");

            this.Num1 = reader.ReadInt32();
            this.Num2 = reader.ReadInt32();
            this.Scale = reader.ReadInt32();
        }

        public static SpawnDecalEvent Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new SpawnDecalEvent();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing SpawnDecalEvent...");

            writer.Write(this.Num1);
            writer.Write(this.Num2);
            writer.Write(this.Scale);
        }

        #endregion
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
