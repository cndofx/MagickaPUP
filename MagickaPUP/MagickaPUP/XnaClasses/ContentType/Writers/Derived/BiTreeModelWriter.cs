using MagickaPUP.MagickaClasses.Map;
using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses.ContentType.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class BiTreeModelWriter : TypeWriter<BiTreeModel>
    {
        public BiTreeModelWriter()
        { }

        public override void Write(BiTreeModel instance, MBinaryWriter writer, DebugLogger logger = null)
        {
            instance.WriteInstance(writer, logger);
        }
    }
}
