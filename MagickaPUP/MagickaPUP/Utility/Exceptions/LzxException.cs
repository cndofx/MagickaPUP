using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Utility.Exceptions
{
    public class LzxException : Exception
    {
        protected static readonly string EXCEPTION_MSG_BASE = "LZX Exception";
        protected static readonly string EXCEPTION_MSG_BODY = "An error occurred while performing an LZX compression operation";

        public LzxException()
        : base($"{EXCEPTION_MSG_BASE} : {EXCEPTION_MSG_BODY}")
        { }

        public LzxException(string message)
        : base($"{EXCEPTION_MSG_BASE} : {message}")
        { }

        public LzxException(string message, Exception inner)
        : base($"{EXCEPTION_MSG_BASE} : {message}", inner)
        { }
    }
}
