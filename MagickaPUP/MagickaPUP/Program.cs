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
            public Action<string[], int> fn;

            public CmdEntry(string cmd1, string cmd2, string desc1, string desc2, int args, Action<string[], int> fn)
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

        // The help command should ALWAYS override all other commands for safety.
        // This flags exists to allow quitting the program without running any actions when the help command was used.
        // Another trick could have been to say that help as -1 args, but rather than doing any dirty tricks, we're doing it cleanly because this way, there is no early
        // quitting, thus, the rest of the args are parsed, and if any errors exist, we can warn the user and exist the program.
        // This flag also prevents flooding the console with multiple calls to --help.
        private bool displayHelp;

        #endregion

        #region Constructor

        public PupProgram()
        {
            this.packers = new List<Packer>();
            this.unpackers = new List<Unpacker>();
            this.debugLevel = 2; // lvl 2 by default.

            this.displayHelp = false;

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

            if (this.displayHelp)
            {
                ExecHelp(); // Early quit if help was called. The help command should always take precedence and prevent any further code execution if called to prevent the user from mistakenly executing code that they did not mean to.
                return;
            }

            ExecPack();
            ExecUnpack();
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

        #region PrivateMethods - Arg parsing

        private int TryRunCommand(string[] args, int current)
        {
            string arg = args[current];
            foreach (var cmd in this.commands)
            {
                if (cmd.cmd1 == arg || cmd.cmd2 == arg)
                {
                    if (HasEnoughArgs(args.Length, current, cmd.args))
                    {
                        cmd.fn(args, current);
                        return cmd.args;
                    }
                    else
                    {
                        putln($"Not enough arguments for arg \"{args[current]}\", {cmd.args} {(cmd.args == 1 ? "argument was" : "arguments were")} expected.");
                        return -1;
                    }
                }
            }
            putln($"Unknown or Unexpected argument detected : \"{arg}\"");
            return -1;
        }

        private bool TryParseCommands(string[] args)
        {
            if (args.Length <= 0)
            {
                CmdHelp(args, 0);
                return true;
            }

            for (int i = 0; i < args.Length; ++i)
            {
                int count = TryRunCommand(args, i);
                i += count;
                if (count < 0)
                {
                    return false;
                }
            }

            return true;
        }

        private bool HasEnoughArgs(int argc, int current, int requiredArgs)
        {
            return (current + 1) + requiredArgs <= argc;
        }

        #endregion

        #region PrivateMethods - Cmd Registering

        private void CmdHelp(string[] args, int current)
        {
            this.displayHelp = true;
        }

        // The latest call to the debug command will be the one to determine the final debug level.
        private void CmdDebug(string[] args, int current)
        {
            this.debugLevel = int.Parse(args[current + 1]);
        }

        private void CmdPack(string[] args, int current)
        {

        }

        private void CmdUnpack(string[] args, int current)
        {

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

        #endregion

        #region PrivateMethods - Cmd Execution

        private void ExecHelp()
        {
            putln($"Usage : MagickaPup.exe --op <input file> <output file>");
            putln();
            putln("Commands:");
            foreach (var command in this.commands)
                putln($"{command.cmd1}, {command.cmd2} {command.desc1} : {command.desc2}", 1);
            putln();
        }

        private void ExecPack()
        {
            foreach (var packer in this.packers)
                packer.Pack();
        }

        private void ExecUnpack()
        {
            foreach (var unpacker in this.unpackers)
                unpacker.Unpack();
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
