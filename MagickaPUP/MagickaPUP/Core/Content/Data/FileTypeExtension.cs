using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Core.Content.Data
{
    public static class FileTypeExtension
    {
        private static readonly Dictionary<FileType, string> defaultFileTypeExtensions = new()
        {
            { FileType.Xnb, "xnb" },
            { FileType.Json, "json" },
            { FileType.Image, "png" },
        };

        public static string GetExtension(FileType fileType)
        {
            if (defaultFileTypeExtensions.ContainsKey(fileType))
                return defaultFileTypeExtensions[fileType];
            return "";
        }
    }
}
