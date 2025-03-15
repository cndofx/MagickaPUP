using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses.Xnb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Core.Content.Pipeline.Import.Derived
{
    public class XnbImporter : ImportPipeline
    {
        private DebugLogger logger;

        public XnbImporter()
        {
            this.logger = null;
        }

        public XnbImporter(DebugLogger logger)
        {
            this.logger = logger;
        }

        public override XnbFile Import(string fileName)
        {
            throw new NotImplementedException();
        }

        public override XnbFile Import(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
