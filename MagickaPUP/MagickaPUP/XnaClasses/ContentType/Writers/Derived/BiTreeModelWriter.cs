using MagickaPUP.MagickaClasses.Map;
using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class BiTreeModelWriter : TypeReader<BiTreeModel>
    {
        public BiTreeModelWriter()
        { }

        public override BiTreeModel Read(MBinaryReader reader, DebugLogger logger = null)
        {
            throw new NotImplementedException();
        }

        public override void Write(BiTreeModel instance, MBinaryWriter writer, DebugLogger logger = null)
        {
            throw new NotImplementedException();
        }
    }
}
