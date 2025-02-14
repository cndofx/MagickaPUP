using System;

namespace MagickaPUP.Utility.Exceptions
{
    // Exception class used to handle errors when loading data that would not be valid within Magicka when loaded but that we cannot filter out through JSON alone.
    public class MagickaReadException : MagickaException
    {
        protected static readonly new string EXCEPTION_MSG_BASE = "Read Error";
        protected static readonly new string EXCEPTION_MSG_BODY = "An error occurred while reading input data";

        public MagickaReadException()
        : base($"{EXCEPTION_MSG_BASE} : {EXCEPTION_MSG_BODY}")
        { }

        public MagickaReadException(string message)
        : base($"{EXCEPTION_MSG_BASE} : {message}")
        { }

        public MagickaReadException(string message, Exception inner)
        : base($"{EXCEPTION_MSG_BASE} : {message}", inner)
        { }
    }

    // This class simply exists so that we can catch this type of exception when we want to be permissive with errors and let the process continue by skipping
    // a given read operation.
    // An example of a situation where this is used is when adding multiple -u operations on a single mpup call, if one fails, we want the rest to continue working.
    public class MagickaReadExceptionPermissive : MagickaReadException
    {
        public MagickaReadExceptionPermissive()
        : base($"{EXCEPTION_MSG_BASE} : {EXCEPTION_MSG_BODY}")
        { }

        public MagickaReadExceptionPermissive(string message)
        : base($"{EXCEPTION_MSG_BASE} : {message}")
        { }

        public MagickaReadExceptionPermissive(string message, Exception inner)
        : base($"{EXCEPTION_MSG_BASE} : {message}", inner)
        { }
    }
}
