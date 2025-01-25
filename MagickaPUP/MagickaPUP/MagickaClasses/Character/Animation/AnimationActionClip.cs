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
    public class AnimationActionClip : XnaObject
    {
        #region Variables

        public string animationName { get; set; }
        public float animationSpeed { get; set; }
        public float blendTime { get; set; }
        public bool loopAnimation { get; set; }
        public int numActions { get; set; }
        public AnimationAction[] actions { get; set; }

        #endregion

        #region Constructor

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading AnimationActionClip...");
            throw new NotImplementedException("Read AnimationActionClip is not implemented yet!");
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing AnimationActionClip...");
            throw new NotImplementedException("Write AnimationActionClip is not implemented yet!");
        }

        #endregion
    }
}
