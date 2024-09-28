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
    public class PupProgram
    {
        #region Structs and Classes

        public struct CmdEntry
        {
            public string cmd1;
            public string cmd2;
            public string desc1;
            public string desc2;
            public int args;
            public Func<string[], int, bool> fn;

            public CmdEntry(string cmd1, string cmd2, string desc1, string desc2, int args, Func<string[], int, bool> fn)
            {
                this.cmd1 = cmd1;
                this.cmd2 = cmd2;
                this.desc1 = desc1;
                this.desc2 = desc2;
                this.args = args;
                this.fn = fn;
            }
        }

        #endregion

        #region Variables

        private List<Packer> packers;
        private List<Unpacker> unpackers;
        private int debugLevel;

        private CmdEntry[] commands;

        #endregion

        #region Constructor

        public PupProgram()
        {
            this.packers = new List<Packer>();
            this.unpackers = new List<Unpacker>();
            this.debugLevel = 2; // lvl 2 by default.

            this.commands = new CmdEntry[] {
                new CmdEntry("-h", "--help", "", "Display the help message", 0, CmdHelp),
                new CmdEntry("-d", "--debug", "<lvl>", "Set the debug logging level (default = 2)", 1, CmdDebug),
                new CmdEntry("-p", "--pack", "<input> <output>", "Pack JSON files into XNB files", 2, CmdPack),
                new CmdEntry("-u", "--unpack", "<input> <output>", "Unpack XNB files into JSON files", 2, CmdUnpack)
            };

            // NOTE : The pack and unpack cmd functions internally check if the input strings correspond to a file or to a directory.
            // If it corresponds to a file, they process said file only.
            // If it corresponds to a directory, it recursively searches for all of the files and generates in the output folder the same folder structure with the processed files.
        }

        #endregion

        #region PublicMethods

        public void Run(string[] args)
        {
            bool success = TryParseCommands(args);
            if (!success)
            {
                // putln("Failed to parse program arguments.");
                return;
            }

            foreach (var packer in this.packers)
                packer.Pack();

            foreach (var unpacker in this.unpackers)
                unpacker.Unpack();
        }

        #endregion

        #region PrivateMethods - Print

        private void putln(string s = "", int indent = 0, string indentString = "  ")
        {
            for (int i = 0; i < indent; ++i)
                Console.Write($"{indentString}");
            Console.WriteLine(s);
        }

        #endregion

        #region PrivateMethods - Cmd

        private bool CmdHelp(string[] args, int current)
        {
            putln($"Usage : MagickaPup.exe --op <input file> <output file>");
            putln();
            putln("Commands:");
            
            foreach(var command in this.commands)
                putln($"{command.cmd1}, {command.cmd2} {command.desc1} : {command.desc2}", 1);

            putln();

            return true;
        }

        private bool CmdPack(string[] args, int current)
        {

            return true;
        }

        private bool CmdUnpack(string[] args, int current)
        {

            return true;
        }

        private void CmdPackFile(string iFilename, string oFilename, int debuglvl = 2)
        {
            putln($"Registered Packer : (\"{iFilename}\", \"{oFilename}\")");
            Packer p = new Packer(iFilename, oFilename, debuglvl);
            this.packers.Add(p);
        }

        private void CmdUnpackFile(string iFilename, string oFilename, int debuglvl = 2)
        {
            putln($"Registered Unpacker : (\"{iFilename}\", \"{oFilename}\")");
            Unpacker u = new Unpacker(iFilename, oFilename, debuglvl);
            this.unpackers.Add(u);
        }

        private void CmdPackPath(string iPath, string oPath)
        {
            throw new NotImplementedException("Pack Path is not implemented yet!");
        }

        private void CmdUnpackPath(string iPath, string oPath)
        {
            throw new NotImplementedException("Unpack Path is not implemented yet!");
        }

        private bool TryRunCommand(string[] args, int current)
        {
            string arg = args[current];
            foreach (var cmd in this.commands)
            {
                if (cmd.cmd1 == arg || cmd.cmd2 == arg)
                {
                    bool success = HasEnoughArgs(args.Length, current, cmd.args) ? cmd.fn(args, current) : false;
                    return success;
                }
            }
            putln($"Unknown or Unexpected argument detected : \"{arg}\"");
            return false;
        }

        private bool TryParseCommands(string[] args)
        {
            if (args.Length <= 0)
            {
                CmdHelp(args, 0);
                return false;
            }

            for (int i = 0; i < args.Length; ++i)
            {
                bool success = TryRunCommand(args, i);
                if (!success)
                {
                    return false;
                }
            }

            return true;
        }

        private bool CmdDebug(string[] args, int current)
        {
            int lvl = int.Parse(args[current + 1]);
            this.debugLevel = lvl;
            return true;
        }

        private bool HasEnoughArgs(int argc, int current, int requiredArgs)
        {
            return (current + 1) + requiredArgs <= argc;
        }

        #endregion
    }

    class Program
    {
        static void Main(string[] args)
        {
            PupProgram p = new PupProgram();
            p.Run(args);
        }
    }
}
