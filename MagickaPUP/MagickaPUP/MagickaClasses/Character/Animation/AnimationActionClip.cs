using MagickaPUP.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;

// NOTE : The animation data classes for characters are not to be confused with the XNA ones.
// These are implemented specifically for Magicka, while the Animation classes within the XnaClasses folder are based around reading animation data
// for XNA's Model class.
namespace MagickaPUP.MagickaClasses.Character.Animation
{
    // NOTE : The AnimationActionClip class corresponds to the AnimationClipAction class in Magicka's code.
    public class AnimationActionClip
    {
        #region Variables

        public string animationName { get; set; }
        public string animationKey { get; set; }
        public float animationSpeed { get; set; }
        public float blendTime { get; set; }
        public bool loopAnimation { get; set; }
        public int numActions { get; set; }
        public AnimationActionStorage[] actions { get; set; }

        #endregion

        #region Constructor

        public AnimationActionClip(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading AnimationActionClip...");

            this.animationName = reader.ReadString();
            this.animationKey = reader.ReadString();
            this.animationSpeed = reader.ReadSingle();
            this.blendTime = reader.ReadSingle();
            this.loopAnimation = reader.ReadBoolean();
            this.numActions = reader.ReadInt32();
            logger?.Log(2, $" - AnimationName  : {this.animationName}");
            logger?.Log(2, $" - AnimationKey   : {this.animationKey}");
            logger?.Log(2, $" - AnimationSpeed : {this.animationSpeed}");
            logger?.Log(2, $" - BlendTime      : {this.blendTime}");
            logger?.Log(2, $" - LoopAnimation  : {this.loopAnimation}");
            logger?.Log(1, $" - NumActions     : {this.numActions}");

            this.actions = new AnimationActionStorage[numActions];
            for (int i = 0; i < this.numActions; ++i)
            {
                this.actions[i] = new AnimationActionStorage(reader, logger);
            }
        }

        #endregion

        #region PublicMethods

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing AnimationActionClip...");
            
            writer.Write(this.animationName);
            writer.Write(this.animationSpeed);
            writer.Write(this.blendTime);
            writer.Write(this.loopAnimation);
            writer.Write(this.numActions);
            for (int i = 0; i < this.numActions; ++i)
            {
                this.actions[i].Write(writer, logger);
            }
        }

        #endregion
    }
}
