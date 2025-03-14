using MagickaPUP.XnaClasses.Xnb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Core.Content.Pipeline.Export
{
    public abstract class ExportPipeline
    {
        public ExportPipeline()
        { }

        public abstract void Export(string fileName, XnbFile data);
        public abstract void Export(Stream stream, XnbFile data);
    }
}
