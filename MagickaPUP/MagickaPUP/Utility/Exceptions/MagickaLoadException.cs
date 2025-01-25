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
    public class MagickaLoadException : Exception
    {
        public MagickaLoadException()
        :base()
        { }

        public MagickaLoadException(string message)
        :base(message)
        { }

        public MagickaLoadException(string message, Exception inner)
        :base(message, inner)
        { }
    }
}
