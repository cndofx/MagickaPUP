using MagickaPUP.Core;
using MagickaPUP.Core.Content.Data;
using MagickaPUP.Core.Content.Processor.Derived;
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
            bool testing = false;
            if (testing)
            {
                if (args.Length > 0)
                    foreach (string arg in args)
                    {
                        XnbContentProcessor processor = new XnbContentProcessor();
                        processor.Process(arg);
                    }
                        // Console.WriteLine(FileTypeDetector.GetFileType(arg));
            }
            else
            {
                PupProgram p = new PupProgram();
                p.Run(args);
            }
        }
    }
}
