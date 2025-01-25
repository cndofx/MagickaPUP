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

        private static readonly int MAX_ANIMATIONS = 231; // The max number of animation clips that a CharacterTemplate's AnimationList can hold.

        #endregion

        #region Variables

        public AnimationActionClip[] animationClips { get; set; }

        #endregion

        #region Constructor

        public AnimationList()
        {
            this.animationClips = new AnimationActionClip[MAX_ANIMATIONS];
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading AnimationList...");
            throw new NotImplementedException("Read AnimationList is not implemented yet!");
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
