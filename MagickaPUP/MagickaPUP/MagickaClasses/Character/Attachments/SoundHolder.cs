using MagickaPUP.IO;
using MagickaPUP.MagickaClasses.Audio;
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
            throw new NotImplementedException("Read SoundHolder is not implemented yet!");
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
            throw new NotImplementedException("Write SoundHolder is not implemented yet!");
        }

        #endregion
    }
}
