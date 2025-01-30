using MagickaPUP.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using MagickaPUP.Utility.Exceptions;
using MagickaPUP.MagickaClasses.Character.Abilities.Derived;
using MagickaPUP.MagickaClasses.Data;
using MagickaPUP.MagickaClasses.Character.Animation;
using System.Runtime.Remoting.Messaging;

namespace MagickaPUP.MagickaClasses.Character.Abilities
{
    // README : A lot of these comments are outdated. You need to go through them and clean them up.

    // TODO : I really need to rethink the whole XnaObject inheritance system.
    // It worked great for the whole map stuff, but here it's become an utter pain in the ass.
    // Altough, for the XNB writing side, inheritting from XnaObject again would make things easier... idk, maybe I just need to find a new interface to do things that doesn't require this much boilerplate...
    
    // TODO : Implement Writing logic too... and finish implementing the logic for read / write for all of the specific ability types.
    
    // NOTE : Maybe in the future it would make more sense to skip the whole polymorphic step and just make the Ability class contain all of the variables?
    // Similar to how things would be done on Blender's UI side of things where internally we reuse the same fields for certain objects that have repeated
    // characteristics / properties despite being of different types? Or maybe it's ok like this, idk. The point is that all of this stuff will eventually be
    // abstracted away from the user's point of view so it should not matter as long as the implementation is robust enough and fast af boiiii!!!!

    // NOTE : If you look at all of the internal parameters of each specific ability type, you may realize that most of the parameters collide...
    // we could have probably implemented this in an easier way by just having the base ability class contain all of the fields and change the data it reads
    // according to the type rather than holding it through polymorphism, since we don't implement the logic in the end...
    // Also maybe C unions would have made this orders of magnitude easier and less boilerplate-y, but whatever... "high level languages" or something...

    public abstract class Ability
    {
        public Ability()
        { }

        public abstract void Write(MBinaryWriter writer, DebugLogger logger = null);
    }
}
