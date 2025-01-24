using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Data
{
    // This is yet another bitmask-type enum.
    // The values present hint at thisfitting within an u16, but for now we'll just mark it as an i32 enum since everywhere this value is read by Magicka, it is read as an i32 and then casted to the enum type.
    // To be completely honest, that doesn't really matter that much, since Magicka will truncate it anyways if we give it a data type longer than the one used internally for the game. We should adjust to whatever the reader require to at least fit all possible and valid values that the game may use.
    [Flags]
    public enum Elements : int
    {
        None = 0,
        Earth = 1,
        Physical = 1,
        Water = 2,
        Cold = 4,
        Fire = 8,
        Lightning = 16,
        Arcane = 32,
        Life = 64,
        Shield = 128,
        Ice = 256,
        Steam = 512,
        Poison = 1024,
        Offensive = 65343,
        Defensive = 176,
        All = 65535,
        Magick = 65535,
        Basic = 255,
        Instant = 881,
        InstantPhysical = 369,
        InstantNonPhysical = 624,
        StatusEffects = 1614,
        ShieldElements = 224,
        PhysicalElements = 257,
        Beams = 96
    }
    // TODO : Make a comment with all of the bitmask values listed in binary, like I did with the Factions one.
}
