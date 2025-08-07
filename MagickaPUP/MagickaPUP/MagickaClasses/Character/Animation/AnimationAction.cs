using MagickaPUP.Utility.IO;
using MagickaPUP.MagickaClasses.Character.Animation.Derived;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

// NOTE : The animation data classes for characters are not to be confused with the XNA ones.
// These are implemented specifically for Magicka, while the Animation classes within the XnaClasses folder are based around reading animation data
// for XNA's Model class.
namespace MagickaPUP.MagickaClasses.Character.Animation
{
    [JsonDerivedType(typeof(Block), typeDiscriminator: "Block")]
    [JsonDerivedType(typeof(BreakFree), typeDiscriminator: "BreakFree")]
    [JsonDerivedType(typeof(CameraShake), typeDiscriminator: "CameraShake")]
    [JsonDerivedType(typeof(CastSpell), typeDiscriminator: "CastSpell")]
    [JsonDerivedType(typeof(Crouch), typeDiscriminator: "Crouch")]
    [JsonDerivedType(typeof(DamageGrip), typeDiscriminator: "DamageGrip")]
    [JsonDerivedType(typeof(DealDamage), typeDiscriminator: "DealDamage")]
    [JsonDerivedType(typeof(DetachItem), typeDiscriminator: "DetachItem")]
    [JsonDerivedType(typeof(Ethereal), typeDiscriminator: "Ethereal")]
    [JsonDerivedType(typeof(Footstep), typeDiscriminator: "Footstep")]
    [JsonDerivedType(typeof(Grip), typeDiscriminator: "Grip")]
    [JsonDerivedType(typeof(Gunfire), typeDiscriminator: "Gunfire")]
    [JsonDerivedType(typeof(Immortal), typeDiscriminator: "Immortal")]
    [JsonDerivedType(typeof(Invisible), typeDiscriminator: "Invisible")]
    [JsonDerivedType(typeof(Jump), typeDiscriminator: "Jump")]
    [JsonDerivedType(typeof(Move), typeDiscriminator: "Move")]
    [JsonDerivedType(typeof(OverkillGrip), typeDiscriminator: "OverkillGrip")]
    [JsonDerivedType(typeof(PlayEffect), typeDiscriminator: "PlayEffect")]
    [JsonDerivedType(typeof(PlaySound), typeDiscriminator: "PlaySound")]
    [JsonDerivedType(typeof(ReleaseGrip), typeDiscriminator: "ReleaseGrip")]
    [JsonDerivedType(typeof(RemoveStatus), typeDiscriminator: "RemoveStatus")]
    [JsonDerivedType(typeof(SetItemAttach), typeDiscriminator: "SetItemAttach")]
    [JsonDerivedType(typeof(SpawnMissile), typeDiscriminator: "SpawnMissile")]
    [JsonDerivedType(typeof(SpecialAbility), typeDiscriminator: "SpecialAbility")]
    [JsonDerivedType(typeof(Suicide), typeDiscriminator: "Suicide")]
    [JsonDerivedType(typeof(ThrowGrip), typeDiscriminator: "ThrowGrip")]
    [JsonDerivedType(typeof(Tongue), typeDiscriminator: "Tongue")]
    [JsonDerivedType(typeof(WeaponVisibility), typeDiscriminator: "WeaponVisibility")]
    public abstract class AnimationAction
    {
        public AnimationAction()
        { }

        public abstract void Write(MBinaryWriter writer, DebugLogger logger = null);
    }
}
