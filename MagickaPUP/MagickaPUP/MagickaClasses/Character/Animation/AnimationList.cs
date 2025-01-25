using MagickaPUP.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Animation
{
    // NOTE : This class does NOT exist within Magicka's code, this is an abstraction made to make things easier for me to handle and to make the serialized JSON look nicer.
    // This may be a very shitty idea tho, but whatever, fuck it.
    public class AnimationList : XnaObject
    {
        #region Constants

        // NOTE : This is the max number of animation clips that a CharacterTemplate's AnimationList can hold.
        // Within Magicka's code, this value is hard coded as 231. This used to be the case here as well, until I noticed that 231 is actually the
        // value of total animations that exist within the Animations enum. For correctness sake, this value is now modified to be taken
        // from the Animations enum instead, altough the result is exactly the same one as it was before...
        private static readonly int TOTAL_ANIMATIONS = (int)Animations.totalanimations; /* 231 */

        #endregion

        #region Variables

        public int numAnimationClips { get; set; }
        public AnimationActionClip[] animationClips { get; set; }

        #endregion

        #region Constructor

        public AnimationList()
        {
            this.animationClips = new AnimationActionClip[TOTAL_ANIMATIONS];
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading AnimationList...");

            this.numAnimationClips = reader.ReadInt32();
            this.animationClips = new AnimationActionClip[this.numAnimationClips];
            for (int i = 0; i < this.numAnimationClips; ++i)
            {

            }
        }

        public static AnimationList Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new AnimationList();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing AnimationList...");
            throw new NotImplementedException("Write AnimationList is not implemented yet!");
        }

        #endregion
    }
}
