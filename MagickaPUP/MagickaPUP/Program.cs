using MagickaPUP.MagickaClasses;
using MagickaPUP.MagickaClasses.Map;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.Json;
using MagickaPUP.XnaClasses;
using MagickaPUP.MagickaClasses.Areas;

// Fucking Magicka Packer-Unpacker baby!!!
// Main Program entry point

namespace MagickaPUP
{
    class Thing
    {
        private int x;
        public int Value { get { return x; } set { x = value; } }
        public Thing next { get; set; }

        public void Print(int indent = 0)
        {
            PrintIndent(0, "{");
            PrintIndent(indent + 1, $"Value = {Value}");
            PrintIndent(indent + 1, $"Next = ", false);
            if (next == null)
            {
                PrintIndent(0, "null");
            }
            else
            {
                next.Print(indent + 2);
            }
            PrintIndent(indent, "}");
        }

        private void PrintIndent(int indent, string str, bool nl = true)
        {
            for (int i = 0; i < indent; ++i)
                Console.Write(" ");
            Console.Write($"{str}{(nl ? "\n" : "")}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            /*
            VertexBuffer buffer = new VertexBuffer();
            
            Thing t1 = new Thing();
            Thing t2 = new Thing();
            Thing t3 = new Thing();
            t1.Value = 10;
            t1.next = t2;
            t2.Value = 20;
            t2.next = t3;
            t3.Value = 30;
            t3.next = null;

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string jstring = JsonSerializer.Serialize(t1, options);
            Console.WriteLine(jstring);

            Thing t = JsonSerializer.Deserialize<Thing>(jstring);
            t.Print();
            */

            if (false)
            {
                IndexBuffer buf = new IndexBuffer();
                buf.numBytes = 10;
                //buf.data = new byte[10];
                buf.data[0] = 255;
                buf.data[1] = 69;
                buf.data[5] = 34;
                string str = JsonSerializer.Serialize(buf, new JsonSerializerOptions() { WriteIndented = true });
                Console.WriteLine(str);
                IndexBuffer buf2 = JsonSerializer.Deserialize<IndexBuffer>(str);
                string str2 = JsonSerializer.Serialize(buf2, new JsonSerializerOptions() { WriteIndented = true });
                Console.WriteLine(str2);
            }

            if (false)
            {
                StreamReader stream = new StreamReader("C:\\Visual Studio Projects\\MagickaPUP\\MagickaPUP\\z_test_files\\grass_woot_0.json");
                string str = stream.ReadToEnd();
                Texture2D tex = JsonSerializer.Deserialize<Texture2D>(str);

                string outFilename = "C:\\Visual Studio Projects\\MagickaPUP\\MagickaPUP\\z_test_files\\grass_woot_0.dds";
                var writeFile = new FileStream(outFilename, FileMode.Create, FileAccess.Write);
                var writer = new BinaryWriter(writeFile);

                writer.Write(542327876);

            }

            if (false) // MAIN TESTING
            {
                string path = "C:\\Visual Studio Projects\\MagickaPUP\\MagickaPUP\\z_test_files\\";
                // string iFilename = "zzz_ch_swamp.xnb";
                // string oFilename = "zzz_ch_swamp.json";
                // string iFilename = "vs_watchtower.xnb";
                // string oFilename = "vs_watchtower.json";


                // string iFilename = "wc_s1.xnb";
                // string oFilename = "wc_s1.json";
                // string iFilename = "vs_boat.xnb";
                // string oFilename = "vs_boat.json";

                // string iFilename = "ru_s5.xnb";
                // string oFilename = "ru_s5.json";
                //string iFilename = "pages.xnb";
                //string oFilename = "pages.json";

                string iFilename = "tr_s8.xnb";
                string oFilename = iFilename + ".json";

                Unpacker u = new Unpacker(path + iFilename, path + oFilename, 2);
                u.Unpack();
            }

            if (false) // MAIN TESTING 2
            {
                string path = "C:\\Visual Studio Projects\\MagickaPUP\\MagickaPUP\\z_test_files\\";
                //string iFilename = "zzz_ch_swamp.json";
                //string oFilename = "zzz_ch_swamp2.xnb";
                //string iFilename = "wc_s1.json";
                //string oFilename = "wc_s1_2.xnb";
                string iFilename = "vs_boat.json";
                string oFilename = "vs_boat_2.xnb";
                Packer p = new Packer(path + iFilename, path + oFilename, 2);
                p.Pack();
            }

            if (false)
            {
                string path = "C:\\Visual Studio Projects\\MagickaPUP\\MagickaPUP\\z_test_files\\";
                string iFilename = "zzzzz.out";
                string oFilename = "zzzzz_test2.txt";
                Unpacker u = new Unpacker(path + iFilename, path + oFilename, 2);
                u.Unpack();
            }

            // FINAL TEST!!!
            if (true)
            {
                string path = "C:\\Visual Studio Projects\\MagickaPUP\\MagickaPUP\\z_test_files\\";
                string iFilename = "untitled.json";
                string oFilename = "zzz_untitled.xnb";
                Packer p = new Packer(path + iFilename, path + oFilename, 2);
                p.Pack();
            }

            if (false)
            {
                string path = "C:\\Visual Studio Projects\\MagickaPUP\\MagickaPUP\\z_test_files\\";
                string iFilename = "zzz_untitled.xnb";
                string oFilename = "zzz_untitled.json";
                Unpacker u = new Unpacker(path + iFilename, path + oFilename, 2);
                u.Unpack();
            }
        }
    }
}
