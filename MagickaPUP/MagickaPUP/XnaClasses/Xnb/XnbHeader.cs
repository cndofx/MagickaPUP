using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Xnb
{
    // TODO : Finish implementing (move the logic from the reader and writer classes into this one here!)
    public class XnbHeader : XnaObject
    {
        public XnbHeader()
        { }

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading XnbHeader...");
            throw new NotImplementedException("Read XnbHeader is not implemented yet!");
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing XnbHeader...");
            throw new NotImplementedException("Write XnbHeader is not implemented yet!");
        }
    }
}
