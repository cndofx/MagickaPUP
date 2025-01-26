using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Data
{
    // This enum describes the order of an AI agent, as in, the action it is commanded to perform when a certain event takes place.
    // SpawnEvent contains one of these to command the AI to perform a specific behaviour on spawn.
    public enum Order : byte
    {
        None,
        Idle,
        Attack,
        Defend,
        Flee,
        Wander,
        Panic
    }
}
