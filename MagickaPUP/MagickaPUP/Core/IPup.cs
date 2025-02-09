using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Core
{
    // This interface's purpose is to standardise the functionality for the packer and unpacker classes, since they both take the same parameters and are operations of the same kind, where a file of a given input type is converted into a file of an output type.
    // eg: JSON <-> XNB
    // NOTE : Maybe rename to ICompiler since Pup is kind of a weird name...
    public interface IPup
    {
        public int Compile(string inputFile, string outputFile); // NOTE : The int return type is kind of "obsolete" but we still preserve it for now.
    }
}
