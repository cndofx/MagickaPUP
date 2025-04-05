using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Utility.IO.Data
{
    // This enum is used to determine what version of the game we must treat the data as.
    // As of now, this only affects the way that character templates are written and read.
    // Every other type of data is not affected.
    // NOTE : Maybe rename to MagickaVersion?
    public enum GameVersion
    {
        Auto = 0,
        
        Old = 1,
        Legacy = 1,

        New = 2,
        Modern = 2
    }

    /*
        - Auto: -mv auto, --magicka-version auto
            Automatically select the version of the game. Tries to infer it from reading the XNB data on its own.
            May give wrong results or fail if the input data is malformed or contains valid data but not conclussive enough to automatically determine what version
            it corresponds to (edge cases yet to be found during testing).

        - Old / Legacy: -mv old / legacy, --magicka-version old / legacy
            Sets the Magicka version to "old Magicka". Any version prior to the fields "ExperienceValue", "RewardOnKill" and "RewardOnOverkill" being added.

        - New / Modern: -mv new / modern, --magicka-version new / modern
            Sets the Magicka version to "new Magicka". Any version after the fields "ExperienceValue", "RewardOnKill" and "RewardOnOverkill" were added.
    */
}
