using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagickaPUP.Core.Args;

namespace MagickaPUP.Core
{
    // NOTE : Maybe would make sense to rename to ArgParser or something like that?
    public class PupProgram
    {
        #region Constants

        // Aids in thinking about important stuff!
        private static readonly string STRING_THINK = "     \\ /     \n  O   X   O  \n__|__/ \\__|__\n  | /   \\ |  \n / \\     / \\ \n/   \\    \\  \\\nCompiling!";
        private static readonly string STRING_THINK_CHARS = "\\|/-";

        #endregion

        #region Comments and Notes

        // NOTE : The help command should ALWAYS override all other commands for safety.
        // The displayHelp flag exists to allow quitting the program without running any actions when the help command was used.
        // Another trick could have been to say that help as -1 args, but rather than doing any dirty tricks, we're doing it cleanly because this way, there is no early
        // quitting, thus, the rest of the args are parsed, and if any errors exist, we can warn the user and exist the program.
        // This flag also prevents flooding the console with multiple calls to --help.

        // NOTE : The pack and unpack cmd functions internally check if the input strings correspond to a file or to a directory.
        // If it corresponds to a file, they process said file only.
        // If it corresponds to a directory, it recursively searches for all of the files and generates in the output folder the same folder structure with the processed files.

        #endregion

        #region Variables

        private CmdEntry[] commands;

        private List<Packer> packers;
        private List<Unpacker> unpackers;
        private List<string> pathsToCreate;

        private int debugLevel;

        private bool displayHelp;
        private bool displayThink;
        private bool displayVersion;
        private bool readMode;

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
            this.displayVersion = false;
            this.readMode = false;

            this.commands = new CmdEntry[] {
                new CmdEntry("-h", "--help", "", "Display the help message", 0, CmdHelp),
                new CmdEntry("-d", "--debug", "<lvl>", "Set the debug logging level for all commands specified after this one (default = 2)", 1, CmdDebug),
                new CmdEntry("-p", "--pack", "<input> <output>", "Pack JSON files into XNB files", 2, CmdPack),
                new CmdEntry("-u", "--unpack", "<input> <output>", "Unpack XNB files into JSON files", 2, CmdUnpack),
                new CmdEntry("-t", "--think", "", "Aids in thinking about important stuff", 0, CmdThink),
                new CmdEntry("-v", "--version", "", "Display the current version of the program", 0, CmdVersion),
                new CmdEntry("-r", "--read", "", "Make the program run in a continuous loop where the input data is read from stdin and interpreted as commands", 0, CmdReadMode),
            };
        }

        #endregion

        #region PublicMethods

        public void Run(string[] args)
        {
            bool success;

            success = TryRegisterCommands(args);
            if (!success)
                return;
            
            success = TryExecuteCommands();
            if (!success)
                return; // Kinda pointless since we don't do anything afterwards...

            Console.WriteLine("Successfully finished all operations!");
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

        #region PrivateMethods - Arg parsing and top level Command Functions

        private int RegisterCommand(string[] args, int current)
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

        private bool TryRegisterCommands(string[] args)
        {
            if (args.Length <= 0)
            {
                CmdHelp(args, 0);
                return true;
            }

            for (int i = 0; i < args.Length; ++i)
            {
                int count = RegisterCommand(args, i);
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

        private bool TryExecuteCommands()
        {
            // Display the version
            if (this.readMode)
            {
                ExecVersion();
                return true;
            }

            // Display the help menu
            if (this.displayHelp)
            {
                #region Comment
                // Early quit if help was called.
                // NOTE : The help command should always take precedence and prevent any further code execution if called to prevent the user from mistakenly
                // executing code that they did not mean to.
                // For example, no directories or files will be created if the help command is invoked, preventing accidentally modifying the files and directory
                // structure when an user who is experimenting with the tool enters a partially valid command.
                #endregion
                ExecHelp();
                return true;
            }

            // Think about important stuff
            if (this.displayThink)
            {
                ExecThink();
                return true;
            }

            // Enter looping mode where the input is parsed through stdin / stdout communication
            if (this.readMode)
            {
                ExecReadMode();
            }

            // Create the required directories.
            ExecDirCreate();

            // Perform all Pack operations
            ExecPack();

            // Perform all Unpack operations
            ExecUnpack();

            return true; // TODO : Implement error handling on each specific cmd execute() call by adding an "if(!success) return false;" around each call...
        }

        #endregion

        #region PrivateMethods - Cmd Registering

        private void CmdHelp(string[] args, int current)
        {
            this.displayHelp = true;
        }

        private void CmdVersion(string[] args, int current)
        {
            this.displayVersion = true;
        }

        private void CmdThink(string[] args, int current)
        {
            this.displayThink = true;
        }

        private void CmdReadMode(string[] args, int current)
        {
            this.readMode = true;
        }

        private void CmdDebug(string[] args, int current)
        {
            // The latest call to the debug command will be the one to determine the final debug level.
            // Every call to the debug command will set the debug level for all commands after it.
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

        private bool CmdPup(Action<string, string, int> pupFile, string outputExtension, string[] args, int current)
        {
            #region Comment

            // TODO : Due to the fact that this can detect errors in the input commands before running them, it would be ideal to come back to using Func<string[], int, bool> for all of the Cmd_() functions. That way, we could do some message printing or whatever. Maybe return an error code or enum and then have a list with error messages... or just use exceptions, whatever.
            // NOTE : The variable outputExtension is only used for automatic file creation when dealing with the directory based functions. Per file functions allow the user to use any extension they want.

            #endregion

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

        private void CmdPupPath(Action<string, string, int> pupFile, string outputExtension, string iName, string oName, int debuglvl = 2)
        {
            #region Comment
            // The purpose of this function is to iterate over the entire folder structure and go adding files to be packed or unpacked.
            // The input path string determines what is the directory in which to look for the input files.
            // The output path string determines what is the directory in which the output files will be stored.
            // The input folder's subfolder structure is copied into the output folder.
            // On the Cmd stage, the paths are added to a string list.
            // On the Exec stage, the paths are finally used to create the directories.
            // This ensures that no directories will be created until we are sure that the input command is 100% correct.
            #endregion

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

        private void ExecVersion()
        {
            Console.WriteLine("MagickaPUP version 1.0.0.0"); // Version format: (itr, major, minor, patch)
        }

        private void ExecThink()
        {
            // Gotta think about important stuff!!!
            Console.WriteLine(STRING_THINK);
            int i = 0;
            while (true)
            {
                // Thinking...
                Console.Write($"[{STRING_THINK_CHARS[(i / 10000) % STRING_THINK_CHARS.Length]}]\b\b\b");
                ++i;
            }
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
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
        }

        private void ExecReadMode()
        {

        }

        #endregion
    }

}
