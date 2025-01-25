using MagickaPUP.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;

// NOTE : The animation data classes for characters are not to be confused with the XNA ones.
// These are implemented specifically for Magicka, while the Animation classes within the XnaClasses folder are based around reading animation data
// for XNA's Model class.
namespace MagickaPUP.MagickaClasses.Character.Animation
{

    // NOTE : This class does NOT exist within Magicka's code, this is an abstraction made to make things easier for me to handle and to make the serialized JSON look nicer.
    // This may be a very shitty idea tho, but whatever, fuck it.
    public class AnimationActionClipBuffer : XnaObject // NOTE : Maybe this should be renmaed to something simpler like CharacterAnimationsList or whatever the fuck...
    {
        #region Variables
        #endregion

        #region Constructor
        #endregion

        #region PublicMethods
        #endregion
    }

    // NOTE : The AnimationActionClip class corresponds to the AnimationClipAction class in Magicka's code.
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

            this.animationName = reader.ReadString();
            this.animationSpeed = reader.ReadSingle();
            this.blendTime = reader.ReadSingle();
            this.loopAnimation = reader.ReadBoolean();
            this.numActions = reader.ReadInt32();
            this.actions = new AnimationAction[numActions];
            for (int i = 0; i < this.numActions; ++i)
            {
                this.actions[i] = AnimationAction.Read(reader, logger);
            }
        }

        public static AnimationActionClip Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new AnimationActionClip();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing AnimationActionClip...");
            
            writer.Write(this.animationName);
            writer.Write(this.animationSpeed);
            writer.Write(this.blendTime);
            writer.Write(this.loopAnimation);
            writer.Write(this.numActions);
            for (int i = 0; i < this.numActions; ++i)
            {
                this.actions[i].WriteInstance(writer, logger);
            }
        }

        #endregion
    }
}
