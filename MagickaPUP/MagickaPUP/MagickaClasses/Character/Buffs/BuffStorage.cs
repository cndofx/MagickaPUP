using MagickaPUP.IO;
using MagickaPUP.MagickaClasses.Generic;
using System;
using System.Collections.Generic;
using MagickaPUP.MagickaClasses.Data;

namespace MagickaPUP.MagickaClasses.Character.Buffs
{
    public class BuffStorage
    {
        public BuffType BuffType { get; set; }
        public VisualCategory VisualCategory { get; set; }
        public Vec3 Color { get; set; }
        public float Time { get; set; }
        public string Effect { get; set; }

        public BuffStorage()
        { }

        public BuffStorage(MBinaryReader reader, DebugLogger logger = null)
        {

        }
    }
}
