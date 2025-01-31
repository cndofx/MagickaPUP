using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.XnaClasses;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Utility.IO
{
    public interface IMReader
    {
        int Read7BitEncodedInt();

        T ReadObject<T>();

        Vec3 ReadVec3();

        ContentTypeReader ReadContentTypeReader();
    }
}
