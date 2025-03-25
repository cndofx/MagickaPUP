using MagickaPUP.MagickaClasses.Map;
using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class BiTreeModelReader : TypeReader<BiTreeModel>
    {
        public BiTreeModelReader()
        { }

        public override BiTreeModel Read(BiTreeModel instance, MBinaryReader reader, DebugLogger logger = null)
        {
            BiTreeModel ans = new BiTreeModel();
            ans.ReadInstance(reader, logger);
            return ans;
        }
    }
}
