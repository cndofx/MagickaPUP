using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Data
{
    // Judging by the values (powers of 2), this is most likely a bit mask, so a given entity can be of multiple factions at a time, maybe.
    // But, the reading code for character template reads a single value, so this means we're going to have to do a little bit of extra work for the JSON reading side of things.
    [Flags] // For now we'll trust that this is correct.
    public enum Factions : int
    {
        None = 0,
        Evil = 1,
        Wild = 2,
        Friendly = 4,
        Demon = 8,
        Undead = 16,
        Human = 32,
        Wizard = 64,
        Neutral = 255,
        Player0 = 256,
        Player1 = 512,
        Player2 = 1024,
        Player3 = 2048,
        TeamRed = 4096,
        TeamBlue = 8192,
        Players = 16128,
        COUNT = 15
    }

    // Notes about the factions bitmasks:
    /*
        None     = 0b00000000000000000000000000000000, // 0
        Evil     = 0b00000000000000000000000000000001, // 1
        Wild     = 0b00000000000000000000000000000010, // 2
        Friendly = 0b00000000000000000000000000000100, // 4
        Demon    = 0b00000000000000000000000000001000, // 8
        Undead   = 0b00000000000000000000000000010000, // 16
        Human    = 0b00000000000000000000000000100000, // 32
        Wizard   = 0b00000000000000000000000001000000, // 64
        ?????    = 0b00000000000000000000000010000000, // 128 // This does not correspond to any faction in game, so it is reserved for some reason? either it's used for some other system that we don't know of yet, or it's just unused and we can exploit it and use it for our own custom factions lol.
        Neutral  = 0b00000000000000000000000011111111, // 255 // bitmask that marks all of the factions below as true
        Player0  = 0b00000000000000000000000100000000, // 256
        Player1  = 0b00000000000000000000001000000000, // 512
        Player2  = 0b00000000000000000000010000000000, // 1024
        Player3  = 0b00000000000000000000100000000000, // 2048
        TeamRed  = 0b00000000000000000001000000000000, // 4096
        TeamBlue = 0b00000000000000000010000000000000, // 8192
        Players  = 0b00000000000000000011111100000000, // 16128 // another bitmask, which marks all of the player related factions as true, including team red and team blue
        COUNT    = 0b00000000000000000000000000001111, // 15 // This should not have any relevance as it is just the total number of factions that exist in the game within this enum, excluding the "unknwon" faction and the "none" faction, but it's still kinda funny that it coincidentally sets all of the values to 1 lol
    */
}
