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

            this.IsMovable = reader.ReadBoolean();
            this.IsPushable = reader.ReadBoolean();
            this.IsSolid = reader.ReadBoolean();
            this.Mass = reader.ReadSingle();
            this.MaxHitPoints = reader.ReadInt32();
            this.CanHaveStatus = reader.ReadBoolean();

            throw new NotImplementedException("Read PhysicsEntityTemplate is not implemented yet!");
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing PhysicsEntityTemplate...");

            writer.Write(this.IsMovable);
            writer.Write(this.IsPushable);
            writer.Write(this.IsSolid);
            writer.Write(this.Mass);
            writer.Write(this.MaxHitPoints);
            writer.Write(this.CanHaveStatus);

            throw new NotImplementedException("Write PhysicsEntityTemplate is not implemented yet!");
        }

        #endregion

        #region PrivateMethods
        #endregion
    }
}
