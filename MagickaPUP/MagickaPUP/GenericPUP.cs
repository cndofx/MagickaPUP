using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP
{
    public class GenericPUP
    {
        protected DebugLogger logger;

        public GenericPUP(DebugLogger logger)
        {
            this.logger = logger;
        }
    }
}
