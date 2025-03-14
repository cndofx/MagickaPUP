using MagickaPUP.XnaClasses.Xnb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Core.Content.Pipeline.Import
{
    // The Import Pipeline for MagickaPUP is as follows:
    // External / in-file representation ---> Internal / in-memory representation
    // File.whatever (file) ---> XnbFile instance (C# class)

    // Note that obviously, the in-file representation can also be located fully in memory (that's what the Stream impl is for, so that we can have both
    // file stream and memory stream support out of the box) and be yet another intermediate in-memory representation that can be further processed by
    // yet another pipeline class for more complex pipelining.

    public abstract class ImportPipeline
    {
        public ImportPipeline()
        { }

        public abstract XnbFile Import(string fileName);
        public abstract XnbFile Import(Stream stream);
    }
}
