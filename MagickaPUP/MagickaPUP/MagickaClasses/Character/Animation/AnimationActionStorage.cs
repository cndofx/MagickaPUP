using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using MagickaPUP.Utility.Exceptions;
using MagickaPUP.MagickaClasses.Data;

namespace MagickaPUP.MagickaClasses.Character.Animation
{
    public class AnimationActionStorage
    {
        #region Variables

        public string AnimationActionType { get; set; }
        public float StartTime { get; set; }
        public float EndTime { get; set; }

        public AnimationAction AnimationAction { get; set; }

        #endregion

        #region Constructor

        public AnimationActionStorage()
        { }

        public AnimationActionStorage(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading AnimationActionStorage...");

            this.AnimationActionType = reader.ReadString(); // TODO : Move this string out of this class or mark it as a non JSON serializable field, since it will be added again on the child AnimationAction's JSON object.
            this.StartTime = reader.ReadSingle();
            this.EndTime = reader.ReadSingle();

            logger?.Log(2, $" - AnimationActionType : {this.AnimationActionType}");
            logger?.Log(2, $" - StartTime           : {this.StartTime}");
            logger?.Log(2, $" - EndTime             : {this.EndTime}");

            AnimationActionType type;
            bool success = Enum.TryParse<AnimationActionType>(this.AnimationActionType, true, out type);
            if (!success)
                throw new MagickaLoadException($"Could not load the specified AnimationAction type. The type \"{this.AnimationActionType}\" is not a valid AnimationAction for Magicka.");

            switch (type)
            {
                // TODO : Implement...
            }
        }

        #endregion

        #region PublicMethods

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing AnimationActionStorage...");

            throw new NotImplementedException("Write AnimationActionStorage is not implemented yet!");
        }

        #endregion
    }
}
