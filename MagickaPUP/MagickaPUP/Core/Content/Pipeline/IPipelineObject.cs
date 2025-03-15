using MagickaPUP.Core.Content.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Core.Content.Pipeline
{
    public interface IPipelineObject
    {
        public FileType GetFileType();
    }
}
