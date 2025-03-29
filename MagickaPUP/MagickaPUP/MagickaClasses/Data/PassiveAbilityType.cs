using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Data
{
    // NOTE : This is named "PassiveAbilities" within Magicka's code.
    public enum PassiveAbilityType : byte
    {
        None,
        ShieldBoost,
        AreaLifeDrain,
        ZombieDeterrent,
        ReduceAggro,
        EnhanceAllyMelee,
        AreaRegeneration,
        InverseArcaneLife,
        Zap,
        BirchSteam,
        WetLightning,
        MoveSpeed,
        Glow,
        Mjolnr,
        Gungner,
        MasterSword,
        DragonSlayer,
        COUNT
    }
}
