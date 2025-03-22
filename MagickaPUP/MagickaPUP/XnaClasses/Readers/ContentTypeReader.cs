using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Readers
{
    public struct ContentTypeReader
    {
        public string Name { get; set; }
        public int Version { get; set; }

        public ContentTypeReader(string name = "", int version = 0)
        {
            this.Name = name;
            this.Version = version;
        }
    }
}
