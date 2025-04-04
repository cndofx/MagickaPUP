using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Data.Audio
{
    // This banks enum refers to audio banks, as in the types of an audio.
    [Flags]
    public enum Banks : ushort // Curiously, this enum is of type u16 (ushort) within magicka's code (just as it is here), but Magicka's readers read this value as an i32 instead, so that means it stores within the XNB files this data wasting 16 extra bits from the 32 bit integer that will never be used lol? Thankfully we can maybe use this for automatic type detection when trying to figure out what version the XNB for a CharacterTemplate is, maybe???
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
