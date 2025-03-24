using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    // TODO : Implement
    // NOTE : Both the Read and Write methods should call in the future the WriteObject and ReadObject functions from the reader and writer.
    // Yes, that means that we need to move the logic for object writing and reading from XnaObject into the binary reader and writer classes.
    // And probably also rename those classes to ContentReader and ContentWriter or whatever the fuck...
    public class ListWriter<T> : TypeReader<List<T>>
    {
        public ListWriter()
        { }

        public override List<T> Read(MBinaryReader reader, DebugLogger logger = null)
        {
            throw new NotImplementedException();
        }

        public override void Write(List<T> instance, MBinaryWriter writer, DebugLogger logger = null)
        {
            throw new NotImplementedException();
        }
    }
}
