using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Utility.IO
{
    public class MBinaryWriter : BinaryWriter/*, IMWriter*/
    {


        public MBinaryWriter(Stream stream) : base(stream)
        {

        }

        public new void Write7BitEncodedInt(int x)
        {
            base.Write7BitEncodedInt(x);
        }
    }

    public class MTextWriter : StreamWriter
    {
        public MTextWriter(Stream stream) : base(stream) { }

    }
}
