using MagickaPUP.Core.Content.Data;
using MagickaPUP.Core.Content.Pipeline;
using MagickaPUP.Core.Content.Pipeline.Export;
using MagickaPUP.Core.Content.Pipeline.Export.Derived;
using MagickaPUP.Core.Content.Pipeline.Import;
using MagickaPUP.Core.Content.Pipeline.Import.Derived;
using MagickaPUP.Core.Content.Pipeline.Import.Helper;
using MagickaPUP.XnaClasses;
using MagickaPUP.XnaClasses.Xnb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Core.Content.Processor
{
    // NOTE : For now, this class is kinda pointless, but it will all make sense once the XWB support is added... if ever...
    public abstract class ContentProcessor<T>
    {
        public abstract void Pack(Stream inputStream, Stream outputStream);
        public abstract void Unpack(Stream inputStream, Stream outputStream);
        public abstract void Process(Stream inputStream, Stream outputStream);
        public abstract void Process(string inputFileName);

        // NOTE : Maybe in the future we need a Pack method that uses stuff like a Pack(list<stream> inputStreams, List<Stream> outputStreams); or whatever...
    }
}
