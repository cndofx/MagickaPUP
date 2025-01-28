using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Data.Aura
{
    // TODO : Figure out what this corresponds to. Is this the thing that is around you when you use wards? like, it seems like it affects entities around a given entity, so... could be it!
    public enum AuraTarget : byte
    {
        Friendly,
        FriendlyButSelf,
        Enemy,
        All,
        AllButSelf,
        Self,
        Type,
        TypeButSelf,
        Faction,
        FactionButSelf
    }
}
