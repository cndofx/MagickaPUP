using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class DamageEvent : XnaObject
    { }

    public class SplashEvent : XnaObject
    { }

    public class PlaySoundEvent : XnaObject
    { }

    public class PlayEffectEvent : XnaObject
    { }

    public class RemoveEvent : XnaObject
    { }

    public class CameraShakeEvent : XnaObject
    { }

    public class SpawnDecalEvent : XnaObject
    { }

    public class BlastEvent : XnaObject
    { }

    public class SpawnEvent : XnaObject
    { }

    public class OverKillEvent : XnaObject
    { }

    public class SpawnGibsEvent : XnaObject
    { }

    public class SpawnItemEvent : XnaObject
    { }

    public class SpawnMagickEvent : XnaObject
    { }

    public class SpawnMissileEvent : XnaObject
    { }

    public class LightEvent : XnaObject
    { }

    public class CastMagickEvent : XnaObject
    { }

    public class DamageOwnerEvent : XnaObject
    { }

    public class CallbackEvent : XnaObject
    { }

}
