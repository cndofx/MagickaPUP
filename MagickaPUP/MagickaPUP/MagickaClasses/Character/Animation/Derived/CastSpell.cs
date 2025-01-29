using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Animation.Derived
{
    public class CastSpell : AnimationAction
    {
        public bool CastFromStaff { get; set; }
        public string CastSource { get; set; }

        public CastSpell()
        {
            this.CastFromStaff = true;
            this.CastSource = string.Empty;
        }

        public CastSpell(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading CastSpell AnimationAction...");

            this.CastFromStaff = reader.ReadBoolean();
            if (!this.CastFromStaff)
                this.CastSource = reader.ReadString();
        }

        public override void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing CastSpell AnimationAction...");

            writer.Write(this.CastFromStaff);
            if (!this.CastFromStaff)
                writer.Write(this.CastSource);
        }
    }
}
