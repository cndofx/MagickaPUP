using MagickaPUP.XnaClasses.Xnb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Core.Content.Pipeline.Export
{
    // The Export Pipeline for MagickaPUP is as follows:
    // Internal / in-memory representation ---> External / in-file representation
    // XnbFile instance (C# class) ---> File.whatever (file)

    // Note that obviously, the in-file representation can also be located fully in memory (that's what the Stream impl is for, so that we can have both
    // file stream and memory stream support out of the box) and be yet another intermediate in-memory representation that can be further processed by
    // yet another pipeline class for more complex pipelining.

    public abstract class ExportPipeline
    {
        public ExportPipeline()
        { }

        public abstract void Export(string fileName, XnbFile data);
        public abstract void Export(Stream stream, XnbFile data);
    }
}
