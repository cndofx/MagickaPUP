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
    public class AnimationClip : XnaObject
    {
        #region Variables

        #endregion

        #region Constructor

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading AnimationClip...");
            throw new NotImplementedException("Read AnimationClip is not implemented yet!");
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing AnimationClip...");
            throw new NotImplementedException("Write AnimationClip is not implemented yet!");
        }

        #endregion
    }
}
