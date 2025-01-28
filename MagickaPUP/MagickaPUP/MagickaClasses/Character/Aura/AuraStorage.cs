using MagickaPUP.IO;
using MagickaPUP.MagickaClasses.Data;
using MagickaPUP.MagickaClasses.Data.Aura;
using MagickaPUP.MagickaClasses.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Aura
{
    // TODO : Implement
    public class AuraStorage
    {
        public AuraTarget AuraTarget { get; set; }
        public AuraType AuraType { get; set; }
        public VisualCategory VisualCategory { get; set; }
        public Vec3 Color { get; set; }
        public string Effect { get; set; }
        public float Time { get; set; } // NOTE : The Execute() method within the AuraStorage class subtracts every frame delta time from its variable TTL, which is what this Time variable corresponds to. This means that this would most likely correspond to some kind of "Duration" variable. Maybe TTL means "Time to Last"? or whatever the fuck? lol...
        public float Radius { get; set; }
        public string[] TargetTypes { get; set; } // NOTE : This is a single string within XNB files. It contains a comma separated list of elements. For easier JSON editting, I'm splitting them into an array, but you MUST REMEMBER this detail when implementing both the read and write logic for this class!!! otherwise, everything goes to SHIT!
        public Factions TargetFaction { get; set; }
        public Aura Aura { get; set; }

        public AuraStorage()
        { }

        public AuraStorage(MBinaryReader reader, DebugLogger logger = null)
        { }

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        { }
    }
}
