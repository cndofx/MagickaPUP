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
    }
}
