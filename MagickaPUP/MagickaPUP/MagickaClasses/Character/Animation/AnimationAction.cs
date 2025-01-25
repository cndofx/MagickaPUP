using MagickaPUP.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// NOTE : The animation data classes for characters are not to be confused with the XNA ones.
// These are implemented specifically for Magicka, while the Animation classes within the XnaClasses folder are based around reading animation data
// for XNA's Model class.
namespace MagickaPUP.MagickaClasses.Character.Animation
{
    public class AnimationAction : XnaObject
    {
        #region Variables

        public string animationName { get; set; }
        public float animationStartTime { get; set; }
        public float animationEndTime { get; set; }

        #endregion

        #region Constructor

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading AnimationAction...");
            throw new NotImplementedException("Read AnimationAction is not implemented yet!");
        }

        public static AnimationAction Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new AnimationAction();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing AnimationAction...");
            throw new NotImplementedException("Write AnimationAction is not implemented yet!");
        }

        #endregion
    }
}
