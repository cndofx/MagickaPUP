using MagickaPUP.Utility.IO;
using MagickaPUP.MagickaClasses.Data.Audio;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Attachments
{
    public class EffectHolder : XnaObject
    {
        #region Variables

        public string BoneName { get; set; }
        public string EffectName { get; set; }

        #endregion

        #region Constructor

        public EffectHolder()
        {
            this.BoneName = default;
            this.EffectName = default;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading EffectHolder...");

            this.BoneName = reader.ReadString();
            this.EffectName = reader.ReadString();
        }

        public static EffectHolder Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new EffectHolder();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing EffectHolder...");

            writer.Write(this.BoneName);
            writer.Write(this.EffectName);
        }

        #endregion
    }
}
