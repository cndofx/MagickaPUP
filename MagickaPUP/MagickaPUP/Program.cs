using MagickaPUP.Core;
using MagickaPUP.Core.Content.Data;
using MagickaPUP.Core.Content.Processor.Derived;
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using MagickaPUP.XnaClasses.Xnb;
using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses;
using System.Drawing.Imaging;
using SixLabors.ImageSharp;

// Fucking Magicka Packer-Unpacker baby!!!
// Main Program entry point

namespace MagickaPUP
{
    class Program
    {
        static void Main(string[] args)
        {
            int testing = -1;
            if (testing == 0)
            {
                if (args.Length > 0)
                {
                    foreach (string arg in args)
                    {
                        XnbContentProcessor processor = new XnbContentProcessor();
                        processor.Process(arg);
                    }
                    // Console.WriteLine(FileTypeDetector.GetFileType(arg));
                }
            }
            else
            if (testing == 1)
            {
                if (args.Length <= 0)
                    return;

                DebugLogger logger = new DebugLogger("logger", 1);
                using (var stream = new FileStream(args[0], FileMode.Open, FileAccess.Read))
                using (var reader = new MBinaryReader(stream))
                {
                    XnbFile xnbFile = new XnbFile(reader, logger);
                    if (xnbFile.XnbFileData.PrimaryObject is Texture2D)
                    {
                        Console.WriteLine("It is a texture!");
                        Texture2D texture = xnbFile.XnbFileData.PrimaryObject as Texture2D;
                        Image bmp = texture.GetBitmap();
                        bmp.Save(args[0] + "_OUTPUT.png");
                    }
                    else
                    {
                        Console.WriteLine("It is not a texture...");
                    }
                }
            }
            else
            {
                PupProgram p = new PupProgram();
                p.Run(args);
            }
        }
    }
}
