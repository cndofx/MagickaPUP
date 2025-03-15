using MagickaPUP.Core;
using MagickaPUP.Core.Content.Data;
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
            bool testing = true;
            if (testing)
            {
                if(args.Length > 0)
                    foreach(string arg in args)
                        Console.WriteLine(FileTypeDetector.GetFileType(arg));
            }
            else
            {
                PupProgram p = new PupProgram();
                p.Run(args);
            }
        }
    }
}
