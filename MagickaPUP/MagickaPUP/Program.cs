using System;
using System.Collections.Generic;
using System.IO;

// Fucking Magicka Packer-Unpacker baby!!!
// Main Program entry point

namespace MagickaPUP
{
    public class PupProgram
    {
        #region Constants

        // Aids in thinking about important stuff!
        private static readonly string STRING_THINK = "     \\ /     \n  O   X   O  \n__|__/ \\__|__\n  | /   \\ |  \n / \\     / \\ \n/   \\    \\  \\\nCompiling!";

        #endregion

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

        private bool displayThink;

        private List<string> pathsToCreate;

        #endregion

        #region Constructor

        public PupProgram()
        {
            this.packers = new List<Packer>();
            this.unpackers = new List<Unpacker>();
            this.pathsToCreate = new List<string>();
            
            this.debugLevel = 2; // lvl 2 by default.

            this.displayHelp = false;
            this.displayThink = false;

            this.commands = new CmdEntry[] {
                new CmdEntry("-h", "--help", "", "Display the help message", 0, CmdHelp),
                new CmdEntry("-d", "--debug", "<lvl>", "Set the debug logging level for all commands specified after this one (default = 2)", 1, CmdDebug),
                new CmdEntry("-p", "--pack", "<input> <output>", "Pack JSON files into XNB files", 2, CmdPack),
                new CmdEntry("-u", "--unpack", "<input> <output>", "Unpack XNB files into JSON files", 2, CmdUnpack),
                new CmdEntry("-t", "--think", "", "Aids in thinking about important stuff", 0, CmdThink),
                new CmdEntry("-v", "--version", "", "Display the current version of the program", 0, CmdVersion),
            };

            // NOTE : The pack and unpack cmd functions internally check if the input strings correspond to a file or to a directory.
            // If it corresponds to a file, they process said file only.
            // If it corresponds to a directory, it recursively searches for all of the files and generates in the output folder the same folder structure with the processed files.
        }

        // TODO : Rework the execution model of the program... to many fucking bool params.
        // We need some kind of queue / priority queue system that pushes back the operations to be performed. Obviously, the "help" command still takes priority
        // every everything else and will override all other actions, completely disabling them.
        // The idea should be to rework things to have:
        // CmdRegister_Whatever() / RegisterCmdWhatever() / CmdWhateverRegister() -> Registers the Whatever command for execution.
        // CmdExecute_Whatever() / ExecuteCmdWhatever() / CmdWhateverExecute() -> Executes the code for the command Whatever.

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
                // Early quit if help was called. The help command should always take precedence and prevent any further code execution if called to prevent the user from mistakenly executing code that they did not mean to.
                // For example, no directories or files will be created if the help command is invoked, preventing accidentally modifying the files and directory structure when an user who is experimenting with the tool enters a partially valid command.
                ExecHelp();
                return;
            }

            if (this.displayThink)
            {
                ExecThink();
            }

            // First create the required directories, if there are any that need to be created.
            ExecDirCreate();

            // Then, pack and unpack the files. If any of the pack operations relied on some folder being created, the prior step will have done that operation.
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
                        putln($"Not enough arguments for arg \"{args[current]}\", {cmd.args} {(cmd.args == 1 ? "argument was" : "arguments were")} expected.\nUsage : {args[current]} {cmd.desc1}");
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

        private void CmdVersion(string[] args, int current)
        {
            // Unlike the other commands, --version does no cmd registering, instead it immediately executes the code.
            // TODO : Rework the idea of having to use a command queue, maybe just execute the commands as they come and make a single early scan for the help cmd?
            Console.WriteLine("MagickaPUP version 1.0.0.0"); // Version format: (itr, major, minor, patch)
        }

        private void CmdThink(string[] args, int current)
        {
            this.displayThink = true;
        }

        private void CmdHelp(string[] args, int current)
        {
            this.displayHelp = true;
        }

        // The latest call to the debug command will be the one to determine the final debug level.
        // Every call to the debug command will set the debug level for all commands after it.
        private void CmdDebug(string[] args, int current)
        {
            this.debugLevel = int.Parse(args[current + 1]);
        }

        private void CmdPack(string[] args, int current)
        {
            CmdPup(CmdPackFile, "xnb", args, current);
        }

        private void CmdUnpack(string[] args, int current)
        {
            CmdPup(CmdUnpackFile, "json", args, current);
        }

        // TODO : Due to the fact that this can detect errors in the input commands before running them, it would be ideal to come back to using Func<string[], int, bool> for all of the Cmd_() functions. That way, we could do some message printing or whatever. Maybe return an error code or enum and then have a list with error messages... or just use exceptions, whatever.
        // NOTE : The variable outputExtension is only used for automatic file creation when dealing with the directory based functions. Per file functions allow the user to use any extension they want.
        private bool CmdPup(Action<string, string, int> pupFile, string outputExtension, string[] args, int current)
        {
            // Get cmd input data
            string iName = args[current + 1];
            string oName = args[current + 2];
            int lvl = this.debugLevel; // The pup functions take the debug level as arg to allow setting different debug levels for each commands issued on the same program execution.

            // If the specified path is a folder, then process the entire folder structure within it and add all of the files for packing / unpacking
            if (Directory.Exists(iName))
            {
                CmdPupPath(pupFile, outputExtension, iName, oName, lvl);
                return true;
            }

            // If the specified path is a file, then process the single specified file
            if (File.Exists(iName))
            {
                pupFile(iName, oName, this.debugLevel);
                return true;
            }

            // If it's neither, then it does not exist, so we don't process it, cause we can't... lol
            return false;
        }

        // The purpose of this function is to iterate over the entire folder structure and go adding files to be packed or unpacked.
        // The input path string determines what is the directory in which to look for the input files.
        // The output path string determines what is the directory in which the output files will be stored.
        // The input folder's subfolder structure is copied into the output folder.
        // On the Cmd stage, the paths are added to a string list.
        // On the Exec stage, the paths are finally used to create the directories.
        // This ensures that no directories will be created until we are sure that the input command is 100% correct.
        private void CmdPupPath(Action<string, string, int> pupFile, string outputExtension, string iName, string oName, int debuglvl = 2)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(iName);

            string iName2;
            string oName2;

            var files = directoryInfo.GetFiles();
            foreach (var file in files)
            {
                iName2 = Path.Combine(iName, file.Name);
                oName2 = Path.Combine(oName, file.Name) + "." + outputExtension;
                pupFile(iName2, oName2, debuglvl);
            }


            var directories = directoryInfo.GetDirectories();
            foreach (var dir in directories)
            {
                iName2 = Path.Combine(iName, dir.Name);
                oName2 = Path.Combine(oName, dir.Name);
                this.pathsToCreate.Add(oName2);
                CmdPupPath(pupFile, outputExtension, iName2, oName2, debuglvl);
            }
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

        private void ExecDirCreate()
        {
            foreach (var dir in this.pathsToCreate)
                if(!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
        }

        private void ExecThink()
        {
            // Gotta think about important stuff!!!
            Console.WriteLine(STRING_THINK);
            while (true)
            {
                // Thinking...
            }
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
