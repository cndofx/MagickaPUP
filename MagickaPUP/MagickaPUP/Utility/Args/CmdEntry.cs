using System;

namespace MagickaPUP.Utility.Args
{
    public struct CmdEntry
    {
        public string cmd1; // short command name
        public string cmd2; // long command name
        public string desc1; // arguments
        public string desc2; // description
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
}
