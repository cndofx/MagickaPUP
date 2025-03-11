using MagickaPUP.XnaClasses.Xnb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Core.Content.Controllers.Derived
{
    public class ContentControllerJson : ContentController
    {
        public ContentControllerJson() { }

        public override XnbFile ReadContent(string fileName)
        {
            throw new NotImplementedException();
        }

        public override void WriteContent(string fileName, XnbFile data)
        {
            throw new NotImplementedException();
        }
    }
}
