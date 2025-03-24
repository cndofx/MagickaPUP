using MagickaPUP.MagickaClasses.PhysicsEntities;
using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class PhysicsEntityTemplateReader : TypeReader<PhysicsEntityTemplate>
    {
        public PhysicsEntityTemplateReader()
        { }

        public override PhysicsEntityTemplate Read(MBinaryReader reader, DebugLogger logger = null)
        {
            throw new NotImplementedException();
        }

        public override void Write(PhysicsEntityTemplate instance, MBinaryWriter writer, DebugLogger logger = null)
        {
            throw new NotImplementedException();
        }
    }
}
