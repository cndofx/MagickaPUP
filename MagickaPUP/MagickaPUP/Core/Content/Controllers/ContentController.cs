using MagickaPUP.XnaClasses.Xnb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Core.Content.Controllers
{
    // TODO : In the future, add a method to directly read content from a stream maybe? That way we can integrate this program in larger programs as a library of sorts.
    public abstract class ContentController
    {
        public ContentController() { }

        public abstract XnbFile ReadContent(string fileName); // Specific File Format -> Internal C# XNB Data

        public abstract void WriteContent(string fileName, XnbFile data); // Internal C# XNB Data -> Specific File Format
    }
}
