using MagickaPUP.MagickaClasses.PhysicsEntities;
using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses.ContentType.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class PhysicsEntityTemplateWriter : TypeWriter<PhysicsEntityTemplate>
    {
        public PhysicsEntityTemplateWriter()
        { }

        public override void Write(PhysicsEntityTemplate instance, MBinaryWriter writer, DebugLogger logger = null)
        {
            instance.WriteInstance(writer, logger);
        }
    }
}
