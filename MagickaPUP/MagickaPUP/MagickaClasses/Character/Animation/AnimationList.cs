using MagickaPUP.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Animation
{
    // NOTE : This class does NOT exist within Magicka's code (same with AnimationActionStorage...), this is an abstraction made to make things easier for me to handle and to make the serialized JSON look nicer.
    // This may be a very shitty idea tho, but whatever, fuck it.
    public class AnimationList
    {
        #region Constants

        // NOTE : This is the max number of animation clips that a CharacterTemplate's AnimationList can hold.
        // Within Magicka's code, this value is hard coded as 231. This used to be the case here as well, until I noticed that 231 is actually the
        // value of total animations that exist within the Animations enum. For correctness sake, this value is now modified to be taken
        // from the Animations enum instead, altough the result is exactly the same one as it was before...
        // private static readonly int TOTAL_ANIMATIONS = (int)Animations.totalanimations; /* 231 */

        #endregion

        #region Variables

        public int numAnimationClips { get; set; }
        public AnimationActionClip[] animationClips { get; set; }

        #endregion

        #region Constructor

        public AnimationList()
        {
            this.animationClips = new AnimationActionClip[ 0 /* TOTAL_ANIMATIONS */];
        }

        public AnimationList(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading AnimationList...");

            #region Comments

            // NOTE : An animation list (or animation "channel") can be empty and contain no amiation clips at all.
            // The most it can contain is all 231 base game animations.

            // NOTE : The animation clips array ALWAYS has a size of 231 animations, which is the total of animations that exist within the game.
            // All we're doing here is get the input animation string, and then setting the animation data of the corresponding animation index, if it is present.

            // NOTE : We don't need to do any of that tho, since that is what Magicka does to handle the data internally in a way that the game uses it.
            // For us, we just need to read and write this data, so we can just have arrays with a limited size rather than storing all 231 anims, since we don't need
            // to associate each animation to its correct index within the list, we just need to iterate all entries.

            #endregion

            this.numAnimationClips = reader.ReadInt32();
            logger?.Log(2, $" - NumAnimationClips : {this.numAnimationClips}");
            this.animationClips = new AnimationActionClip[this.numAnimationClips];
            for (int i = 0; i < this.numAnimationClips; ++i)
            {
                this.animationClips[i] = new AnimationActionClip(reader, logger);
            }
        }

        #endregion

        #region PublicMethods

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing AnimationList...");
            throw new NotImplementedException("Write AnimationList is not implemented yet!");
        }

        #endregion
    }
}
