using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagickaPUP.Core.Args;
using MagickaPUP.Utility.IO.Data;

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
        private bool indentAllowed;

        private GameVersion inputVersion;
        private GameVersion outputVersion;

        private bool displayHelp;
        private bool displayThink;
        private bool displayVersion;
        private bool readMode;
        private bool pauseOnFinish;

        #endregion

        #region Constructor

        public PupProgram()
        {
            this.packers = new List<Packer>();
            this.unpackers = new List<Unpacker>();
            this.pathsToCreate = new List<string>();

            this.debugLevel = 2; // lvl 2 by default.
            this.indentAllowed = false; // false by default.

            // TODO : On the rest of the code, remove ALL of the default values for GameVersion parameters. The default values should only be set here,
            // at the top level of the entire codepath.
            this.inputVersion = GameVersion.Auto; // Auto by default.
            this.outputVersion = GameVersion.Auto; // Auto by default. Also, Auto is kind of useless for output ops, only useful for input ops since it helps determine automatically what version we were probably working with...

            this.displayHelp = false;
            this.displayThink = false;
            this.displayVersion = false;
            this.readMode = false;

            this.commands = new CmdEntry[] {
                new CmdEntry("-h", "--help", "", "Display the help message", 0, CmdHelp),
                new CmdEntry("-d", "--debug", "<lvl>", "Set the debug logging level for all commands specified after this one (default = 2)", 1, CmdDebug),
                new CmdEntry("-i", "--indent", "<indent>", "Set the indentation level for all commands specified after this one (default = false)", 1, CmdIndent), // TODO : Modify to use custom indentation amounts such as 4 spaces, 1 tab, whatever, etc...
                new CmdEntry("-p", "--pack", "<input> <output>", "Pack JSON files into XNB files", 2, CmdPack),
                new CmdEntry("-u", "--unpack", "<input> <output>", "Unpack XNB files into JSON files", 2, CmdUnpack),
                new CmdEntry("-t", "--think", "", "Aids in thinking about important stuff", 0, CmdThink),
                new CmdEntry("-v", "--version", "", "Display the current version of the program", 0, CmdVersion),
                new CmdEntry("-r", "--read", "", "Make the program run in a continuous read loop where further commands will be read from stdin", 0, CmdReadMode),
                new CmdEntry("-P", "--pause-on-finish", "", "Make the program pause and wait for user input when all operations are finished", 0, CmdPauseOnFinish),
                new CmdEntry("-I", "--input-version", "<version>", "Set the Magicka version for the input XNB files (default = latest)", 1, CmdSetVersionInput),
                new CmdEntry("-O", "--output-version", "<version>", "Set the Magicka version for the output XNB files. (default = latest)", 1, CmdSetVersionOutput),
            };
        }

        #endregion

        #region PublicMethods

        public void Run(string[] args)
        {
            bool success;

            ClearData();

            success = TryRegisterCommands(args);
            if (!success)
                return;
            
            success = TryExecuteCommands();
            if (!success)
                return; // Kinda pointless since we don't do anything afterwards...

            // Console.WriteLine("Successfully finished all operations!");
        }

        #endregion

        #region PrivateMethods

        private void ClearData()
        {
            this.packers.Clear();
            this.unpackers.Clear();
            this.pathsToCreate.Clear();
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
            if (this.displayVersion)
                ExecVersion();

            // Display the help menu
            if (this.displayHelp)
            {
                #region Comment
                // Early quit if help was called.
                // NOTE : The help command should always take precedence and prevent any further code execution if called to prevent the user from mistakenly
                // executing code that they did not mean to.
                // For example, no directories or files will be created if the help command is invoked, preventing accidentally modifying the files and directory
                // structure when an user who is experimenting with the tool enters a partially valid command.
                // The only exception to this rule is the --version command, since it doesn't really matter tbh...
                #endregion
                ExecHelp();
                return true;
            }

            // Think about important stuff
            if (this.displayThink)
                ExecThink();

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

            // Pause at end if required
            if (this.pauseOnFinish)
                ExecPauseOnFinish();

            return true; // TODO : Implement error handling on each specific cmd execute() call by adding an "if(!success) return false;" around each call...
        }

        #endregion

        #region PrivateMethods - Cmd Registering

        private void CmdPauseOnFinish(string[] args, int current)
        {
            this.pauseOnFinish = true;
        }

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
            this.debugLevel = ArgsUtility.ParseInt(args[current + 1]);
        }

        private void CmdIndent(string[] args, int current)
        {
            // Same as CmdDebug, the latest call to this command will be applied to all subsequent cmd for unpacking or packing that is called afterward.
            // Also note that as of writing this comment, this command only affects unpacking, not packing, since the JSON input files for packing can have any indent depth.
            // In short, this only affects when unpacking and writing output JSON files.
            this.indentAllowed = ArgsUtility.ParseBool(args[current + 1]);
        }

        private void CmdPack(string[] args, int current)
        {
            CmdPup(CmdPackFile, args, current);
        }

        private void CmdUnpack(string[] args, int current)
        {
            CmdPup(CmdUnpackFile, args, current);
        }

        // TODO : Replace the action with the Settings data structure stuff in the future... maybe even make a generic PupSettings struct that can be used by both classes,
        // and then maybe even change the classes to a single one and just change with a flag the operation to "compile" or "decompile" or whatever...
        private bool CmdPup(Action<string, string> pupFile, string[] args, int current)
        {
            #region Comment

            // TODO : Due to the fact that this can detect errors in the input commands before running them, it would be ideal to come back to using Func<string[], int, bool> for all of the Cmd_() functions. That way, we could do some message printing or whatever. Maybe return an error code or enum and then have a list with error messages... or just use exceptions, whatever.
            // NOTE : The variable outputExtension is only used for automatic file creation when dealing with the directory based functions. Per file functions allow the user to use any extension they want.

            #endregion

            // Get cmd input data
            string iName = args[current + 1];
            string oName = args[current + 2];

            // If the specified path is a folder, then process the entire folder structure within it and add all of the files for packing / unpacking
            if (Directory.Exists(iName))
            {
                CmdPupPath(pupFile, iName, oName);
                return true;
            }

            // If the specified path is a file, then process the single specified file
            if (File.Exists(iName))
            {
                pupFile(iName, oName);
                return true;
            }

            // If it's neither, then it does not exist, so we don't process it, cause we can't... lol
            return false;
        }

        private void CmdPupPath(Action<string, string> pupFile, string iName, string oName)
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
                oName2 = Path.Combine(oName, file.Name);
                pupFile(iName2, oName2);
            }


            var directories = directoryInfo.GetDirectories();
            foreach (var dir in directories)
            {
                iName2 = Path.Combine(iName, dir.Name);
                oName2 = Path.Combine(oName, dir.Name);
                this.pathsToCreate.Add(oName2);
                CmdPupPath(pupFile, iName2, oName2);
            }
        }

        private void CmdPackFile(string iFilename, string oFilename)
        {
            putln($"Registered Packer : (\"{iFilename}\", \"{oFilename}\")");
            Packer p = new Packer(iFilename, oFilename, this.debugLevel, this.outputVersion);
            this.packers.Add(p);
        }

        private void CmdUnpackFile(string iFilename, string oFilename)
        {
            putln($"Registered Unpacker : (\"{iFilename}\", \"{oFilename}\")");
            Unpacker u = new Unpacker(iFilename, oFilename, this.debugLevel, this.indentAllowed, this.inputVersion);
            this.unpackers.Add(u);
        }

        private void CmdSetVersionInput(string[] args, int current)
        {
            // TODO : Abstract away the switch into a separate function that lets us get the version enum from strings. That way, the logic gets less cluttered up,
            // and we get a singular access point where we can define how the version string parsing is handled... maybe even add in the future support for literal
            // version numbers, such as 1.10, 1.4, 1.5, etc...

            // TODO : Read the TODO on the CmdSetVersionOutput command...

            string versionStr = args[current + 1].ToLower();

            switch (versionStr)
            {
                case "auto":
                    this.inputVersion = GameVersion.Auto;
                    break;
                case "old":
                    this.inputVersion = GameVersion.Old;
                    break;
                case "new":
                    this.inputVersion = GameVersion.New;
                    break;
                default:
                    throw new Exception($"Unknown Magicka version specified for input: \"{versionStr}\"");
            }

            // throw new NotImplementedException("CmdSetVersionInput");
        }

        private void CmdSetVersionOutput(string[] args, int current)
        {
            // TODO : Change the version commands to be kinda like the future plans for the image type output format specifiers.
            // Something like a flag that lets me says "hey, I want this specific xnb data type to be compiled or decompiled into this
            // specific type of file or version, etc..."
            // This will make it more modular and also work nicely with the future plans for supporting custom user defined types through dlls, which will need
            // their own user defined versioning commands as well.

            string versionStr = args[current + 1].ToLower();

            switch (versionStr)
            {
                case "auto":
                    this.outputVersion = GameVersion.Auto;
                    break;
                case "old":
                    this.outputVersion = GameVersion.Old;
                    break;
                case "new":
                    this.outputVersion = GameVersion.New;
                    break;
                default:
                    throw new Exception($"Unknown Magicka version specified for output: \"{versionStr}\"");
            }

            // throw new NotImplementedException("CmdSetVersionOutput");
        }

        #endregion

        #region PrivateMethods - Cmd Execution

        private void ExecPauseOnFinish()
        {
            Console.WriteLine("Press <ENTER> to continue...");
            Console.ReadLine();
        }

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
            // NOTE : This is implemented in a very shitty way, but whatever... good enough for now!
            bool shouldQuit = false;
            Console.WriteLine("Read Mode Initialized. Write q to exit.");
            while (!shouldQuit)
            {
                string input = Console.ReadLine();
                string[] args = input.Trim().Split();
                
                if (args.Length == 1 && args[0] == "q")
                {
                    Console.WriteLine("Read Mode Terminated.");
                    return;
                }

                if (args.Length > 0 && args[0].Length > 0)
                {
                    PupProgram pup = new PupProgram();
                    pup.Run(args);
                }
            }
        }

        #endregion
    }

}
