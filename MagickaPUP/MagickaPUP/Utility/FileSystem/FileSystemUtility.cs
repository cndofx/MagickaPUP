using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Utility.FileSystem
{
    // NOTE : Add other functionalities here in the future.
    // Remember stuff like the FileSystemUtility class from the install manager program (mcow-mm).
    public static class FSUtil
    {
        // Returns a cleaned up file path without extension
        public static string GetPathWithoutExtension(string name)
        {
            string nameBase = Path.GetFileNameWithoutExtension(name);
            string pathBase = Path.GetDirectoryName(name);

            name = Path.Combine(pathBase, nameBase);

            return name;
        }
    }
}
