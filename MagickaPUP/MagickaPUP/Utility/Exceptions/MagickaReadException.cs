using System;

namespace MagickaPUP.Utility.Exceptions
{
    /*
        NOTE : The exception should have some kind of message, something like $"The following content cannot be loaded by Magicka: {whatever_thing_name}"
        and then the user defined message or whatever... something like that.
        For now, this is good enough. Heck, we could even just throw regular exceptions and call it a day, but doing this early makes things easier in the long run...
        For example, it can help identifying when an exception happens because of something that is always invalid when loaded into Magicka, or when something is
        valid only on some specific versions and on others it does not work, etc...
    */
    // Exception class used to handle errors when loading data that would not be valid within Magicka when loaded but that we cannot filter out through JSON alone.
    public class MagickaReadException : MagickaException
    {
        protected static readonly new string EXCEPTION_MSG_BASE = "Read Error";
        protected static readonly new string EXCEPTION_MSG_BODY = "An error occurred while reading input data";
    }

    // This class simply exists so that we can catch this type of exception when we want to be permissive with errors and let the process continue by skipping
    // a given read operation.
    // An example of a situation where this is used is when adding multiple -u operations on a single mpup call, if one fails, we want the rest to continue working.
    public class MagickaReadExceptionPermissive : MagickaReadException
    { }
}
