using MagickaPUP.IO;
using MagickaPUP.MagickaClasses.Generic;
using System;
using System.Collections.Generic;
using MagickaPUP.MagickaClasses.Data;

namespace MagickaPUP.MagickaClasses.Character.Buffs
{
    // NOTE : Just like the Ability class, BuffStorage is a weird type of polymorphic class where rather than identifying the type like Effect's derived types
    // through a ContentTypeReader, we instead identify it with an enum and a switch, so yeah. 
    public class BuffStorage
    {
        public BuffType BuffType { get; set; }
        public VisualCategory VisualCategory { get; set; }
        public Vec3 Color { get; set; }
        public float Time { get; set; }
        public string Effect { get; set; }

        public BuffStorage()
        {

        }

        public BuffStorage(MBinaryReader reader, DebugLogger logger = null)
        {

        }
    }
}
