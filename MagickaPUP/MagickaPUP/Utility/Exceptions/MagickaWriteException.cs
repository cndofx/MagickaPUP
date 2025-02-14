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
    }

    public class MagickaWriteExceptionPermissive : MagickaWriteException
    { }
}
