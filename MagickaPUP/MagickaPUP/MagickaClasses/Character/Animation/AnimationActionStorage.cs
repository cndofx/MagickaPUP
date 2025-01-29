using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            this.AnimationActionType = reader.ReadString();
            this.StartTime = reader.ReadSingle();
            this.EndTime = reader.ReadSingle();

            logger?.Log(2, $" - AnimationActionType : {this.AnimationActionType}");
            logger?.Log(2, $" - StartTime           : {this.StartTime}");
            logger?.Log(2, $" - EndTime             : {this.EndTime}");

            // TODO : Implement AnimationAction reading
            // this.AnimationAction
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
