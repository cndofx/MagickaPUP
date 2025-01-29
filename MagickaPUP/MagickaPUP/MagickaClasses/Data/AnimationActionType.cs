using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Data
{
    // NOTE : This does not exist in the base game, this is just an enum that I use to make parsing AnimationAction strings easier for myself.
    public enum AnimationActionType
    {
        Block,
        BreakFree,
        CameraShake,
        CastSpell,
        Crouch,
        DamageGrip,
        DealDamage,
        DetachItem,
        Ethereal,
        Footstep,
        Grip,
        Gunfire,
        Immortal,
        Invisible,
        Jump,
        Move,
        OverkillGrip,
        PlayEffect,
        PlaySound,
        ReleaseGrip,
        RemoveStatus,
        SetItemAttach,
        SpawnMissile,
        SpecialAbility,
        Suicide,
        ThrowGrip,
        Tongue,
        WeaponVisibility
    }
}
