using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Core.Content.Data
{
    // This enum is a custom enum that is used by MagickaPUP's ContentProcessor class to determine what type of content to use when generating the import and
    // export pipeline calls for each type of file and stream.
    public enum ContentType
    {
        Auto = 0,
        Unknown,
        Xnb,
        Json,
        Image // TODO : Maybe choose what type of image? as in explicitly saying png, jpg, bmp, etc...
    }
}
