using MagickaPUP.XnaClasses.Xnb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Core.Content.Pipeline.Import
{
    public abstract class ImportPipeline
    {
        public ImportPipeline()
        { }

        public abstract XnbFile Import(string fileName);
        public abstract XnbFile Import(Stream stream);
    }
}
