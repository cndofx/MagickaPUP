using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.IO
{
    public interface IMWriter
    {
        void Write7BitEncodedInt(int value);
    }
}
