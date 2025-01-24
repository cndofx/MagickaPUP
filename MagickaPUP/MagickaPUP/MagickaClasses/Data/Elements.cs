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

    // NOTE : The curious thing is that despite this being a bitmask, it is not used as such on the resistances array for characters.
    // WTF? Why? What were the Magicka devs thinking??? Am I misinterpreting something???
    // Just look at the following possible example:
    // Console.WriteLine($"{MagickaDefines.ElementIndex((Elements.Ice | Elements.Fire | Elements.Cold | Elements.Life | Elements.Shield | Elements.Steam))}");
    // It returns the element index 10, which corresponds to poison. Using | on all would obviously return the index for All, which is 11...
    // Also the larger "out of bounds" values for the Elements array DO return a valid index value... but it is not valid on CharacterTemplate's resistances array,
    // because those correspond to out of bounds indices, because the array is limited to only 11 positions... In short, wtf? This is so weirdly coded!

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

    /*
        using this simple snippet from Magicka's code:

        public static int ElementFromIndex(int iIndex)
        {
        if (iIndex == 11)
        {
	        return -1;
        }
        return (int)(Math.Pow(2.0, (double)iIndex) + 0.5);
        }

        and this:
        for(int i = 0; i < 12; ++i)
        Console.WriteLine($"{ElementFromIndex(i)}");

        the output lets us determine corresponding elements for each index:

        1
        2
        4
        8
        16
        32
        64
        128
        256
        512
        1024
        -1

        Physical / Earth : idx = 0,  val = 1
        Water            : idx = 1,  val = 2
        Cold             : idx = 2,  val = 4
        Fire             : idx = 3,  val = 8
        Lightning        : idx = 4,  val = 16
        Arcane           : idx = 5,  val = 32
        Life             : idx = 6,  val = 64
        Shield           : idx = 7,  val = 128
        Ice              : idx = 8,  val = 256
        Steam            : idx = 9,  val = 512
        Poison           : idx = 10, val = 1024

        The other "element" values correspond to bitfields / flags that let us know certain properties about a spell, for instance, whether it's a beam or not.

    */


}
