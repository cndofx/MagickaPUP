using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Utility.Exceptions
{
    public class MagickaWriteException : MagickaException
    {
        protected static readonly new string EXCEPTION_MSG_BASE = "Write Error";
        protected static readonly new string EXCEPTION_MSG_BODY = "An error occurred while writing output data";

        public MagickaWriteException()
        : base($"{EXCEPTION_MSG_BASE} : {EXCEPTION_MSG_BODY}")
        { }

        public MagickaWriteException(string message)
        : base($"{EXCEPTION_MSG_BASE} : {message}")
        { }

        public MagickaWriteException(string message, Exception inner)
        : base($"{EXCEPTION_MSG_BASE} : {message}", inner)
        { }
    }

    public class MagickaWriteExceptionPermissive : MagickaWriteException
    { }
}
