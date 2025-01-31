using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.PhysicsEntities
{
    public class PhysicsEntityTemplate : XnaObject
    {
        #region Variables

        public bool IsMovable { get; set; }
        public bool IsPushable { get; set; }
        public bool IsSolid { get; set; }
        public float Mass { get; set; }
        public int MaxHitPoints { get; set; }
        public bool CanHaveStatus { get; set; }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading PhysicsEntityTemplate...");
            throw new NotImplementedException("Read PhysicsEntityTemplate is not implemented yet!");
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing PhysicsEntityTemplate...");
            throw new NotImplementedException("Write PhysicsEntityTemplate is not implemented yet!");
        }

        #endregion

        #region PrivateMethods
        #endregion
    }
}
