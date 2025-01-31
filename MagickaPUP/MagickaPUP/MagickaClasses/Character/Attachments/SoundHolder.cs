using MagickaPUP.Utility.IO;
using MagickaPUP.MagickaClasses.Data.Audio;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Attachments
{
    public class SoundHolder : XnaObject
    {
        #region Variables

        public string SoundName { get; set; }
        public Banks SoundBanks { get; set; }

        #endregion

        #region Constructor

        public SoundHolder()
        {
            this.SoundName = default;
            this.SoundBanks = default;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading SoundHolder...");

            this.SoundName = reader.ReadString().ToLowerInvariant(); // ID string for the sound
            this.SoundBanks = (Banks)reader.ReadInt32(); // Sound banks for the sound

            // Within magicka's code, we compute the hash of the string and then we store wihtin the attached sounds array a pair of type
            // KeyValuePair<int, Banks>(hash,banks).
            // Here, we can just store a pair of string and banks and not give a fuck since we don't need to compute the hash for anything,
            // we're just moving bytes around.
        }

        public static SoundHolder Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new SoundHolder();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing SoundHolder...");

            writer.Write(this.SoundName);
            writer.Write((int)this.SoundBanks);
        }

        #endregion
    }
}
