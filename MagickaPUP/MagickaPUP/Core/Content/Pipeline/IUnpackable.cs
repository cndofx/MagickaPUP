using MagickaPUP.Core.Content.Pipeline.Export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Core.Content.Pipeline
{
    public interface IUnpackable
    {
        public abstract ExporterBase<IUnpackable> ExporterUnpack { get; }
        public abstract ExporterBase<IUnpackable> ImporterUnpack { get; }
    }
}
