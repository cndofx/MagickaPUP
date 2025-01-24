using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Audio
{
    // This banks enum refers to audio banks, as in the types of an audio.
    [Flags]
    public enum Banks : ushort // Curiously, this is an u16 within magicka's code but also magicka reads this value as an i32, so that means it stores within the XNB files this data wasting 32 bits that will never be used lol?
    {
        WaveBank = 1,
        Music = 2,
        Ambience = 4,
        UI = 8,
        Spells = 16,
        Characters = 32,
        Footsteps = 64,
        Weapons = 128,
        Misc = 256,
        Additional = 512,
        AdditionalMusic = 1024
    }
}
