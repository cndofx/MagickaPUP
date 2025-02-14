using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Utility.Exceptions
{
    public class MagickaException : Exception
    {
        protected static readonly string EXCEPTION_MSG_BASE = "Magicka Exception";
        protected static readonly string EXCEPTION_MSG_BODY = "An error occurred while performing an operation";

        public MagickaException()
        : base($"{EXCEPTION_MSG_BASE} : {EXCEPTION_MSG_BODY}")
        { }

        public MagickaException(string message)
        : base($"{EXCEPTION_MSG_BASE} : {message}")
        { }

        public MagickaException(string message, Exception inner)
        : base($"{EXCEPTION_MSG_BASE} : {message}", inner)
        { }
    }
}
