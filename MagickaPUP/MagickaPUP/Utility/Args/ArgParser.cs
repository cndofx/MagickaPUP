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

        public static CmdEntry[] commands = new CmdEntry[] {
            new CmdEntry("-h", "--help", "", "Display the help message", 0, CmdHelp),
            new CmdEntry("-d", "--debug", "<lvl>", "Set the debug logging level for all commands specified after this one (default = 2)", 1, CmdDebug),
            new CmdEntry("-p", "--pack", "<input> <output>", "Pack JSON files into XNB files", 2, CmdPack),
            new CmdEntry("-u", "--unpack", "<input> <output>", "Unpack XNB files into JSON files", 2, CmdUnpack),
            new CmdEntry("-t", "--think", "", "Aids in thinking about important stuff", 0, CmdThink),
            new CmdEntry("-v", "--version", "", "Display the current version of the program", 0, CmdVersion),
        };

        #endregion

        #region Constructor

        public ArgParser()
        {

        }

        #endregion

        #region PublicMethods
        #endregion

        #region PrivateMethods
        #endregion
    }
}
