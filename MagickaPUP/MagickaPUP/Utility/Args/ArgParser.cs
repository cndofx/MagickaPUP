using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Utility.Args
{
    public class ArgParser
    {
        #region Variables

        private CmdEntry[] commands;

        #endregion

        #region Constructor

        public ArgParser()
        {
            this.commands = new CmdEntry[0];
        }

        public ArgParser(CmdEntry helpCommand, CmdEntry[] commandsArray)
        {
            this.commands = new CmdEntry[commandsArray.Length + 1];
            this.commands[0] = helpCommand;
            for(int i = 0; i < commandsArray.Length; ++i)
                this.commands[i + 1] = commandsArray[i];
        }

        #endregion

        #region PublicMethods
        #endregion

        #region PrivateMethods
        #endregion

        #region PrivateMethods - Arg Parsing and Registering

        private int TryRegisterCommand(string[] args, int current)
        {
            string arg = args[current];
            foreach (var cmd in this.commands)
            {
                if (cmd.cmd1 == arg || cmd.cmd2 == arg)
                {
                    if (HasEnoughArgs(args.Length, current, cmd.args))
                    {
                        cmd.register(args, current);
                        return cmd.args;
                    }
                    else
                    {
                        Console.WriteLine($"Not enough arguments for arg \"{args[current]}\", {cmd.args} {(cmd.args == 1 ? "argument was" : "arguments were")} expected.\nUsage : {args[current]} {cmd.desc1}");
                        return -1;
                    }
                }
            }
            Console.WriteLine($"Unknown or Unexpected argument detected : \"{arg}\"");
            return -1;
        }

        private bool TryParseCommands(string[] args)
        {
            if (args.Length <= 0)
            {
                // this.commands[0].execute(); // NOTE : commands[0] is the help command. We execute the help command by default when no arguments are given.
                return true;
            }

            for (int i = 0; i < args.Length; ++i)
            {
                int count = TryRegisterCommand(args, i);
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
    }
}
