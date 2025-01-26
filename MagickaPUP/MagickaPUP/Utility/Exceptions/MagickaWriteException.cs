using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Utility.Exceptions
{
    public class MagickaWriteException : Exception
    {
        private static readonly string EXCEPTION_MSG_BASE = "Write Error";

        public MagickaWriteException()
        : base($"{EXCEPTION_MSG_BASE} : An error occurred while reading input data")
        { }

        public MagickaWriteException(string message)
        : base($"{EXCEPTION_MSG_BASE} : {message}")
        { }

        public MagickaWriteException(string message, Exception inner)
        : base($"{EXCEPTION_MSG_BASE} : {message}", inner)
        { }
    }
}
