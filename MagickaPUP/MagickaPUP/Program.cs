using MagickaPUP.Core;
using System;
using System.Collections.Generic;
using System.IO;

// Fucking Magicka Packer-Unpacker baby!!!
// Main Program entry point

namespace MagickaPUP
{
    class Program
    {
        static void Main(string[] args)
        {
            PupProgram p = new PupProgram();
            p.Run(args);
        }
    }
}
