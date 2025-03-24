using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses.ContentType.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific
{
    public abstract class TypeReader<T> : TypeReaderBase where T : class
    {
        public TypeReader()
        { }

        public abstract T Read(T instance, MBinaryReader reader, DebugLogger logger = null); // NOTE : Even in XNA and Monogame, this instance parameter is mostly fucking useless... for the most part, this works as a way to trick the fucking stupid compiler into knowing what the correct method to call is. A workaround to avoid having to use this shit would be to simply remove it in this case, since the base Read() method is already implemented taking an object param, but whatever... fuck this shit, for now it's ok, I guess...

        public override object Read(object instance, MBinaryReader reader, DebugLogger logger = null)
        {
            if (instance == null)
                instance = default(T);
            return Read((T)instance, reader, logger);
        }
    }
}
