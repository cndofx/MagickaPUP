using MagickaPUP.Core.Content.Pipeline.Export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Core.Content.Pipeline
{
    public interface IPackable
    {
        public abstract ExporterBase<IPackable> ExporterPack { get; }
        public abstract ExporterBase<IPackable> ImporterPack { get; }
    }
}
