using MagickaPUP.Core.Content.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Core.Content.Processor
{
    public class ContentProcessor
    {
        public ContentProcessor()
        { }

        public void ProcessFromArgs(string[] args)
        {
            throw new NotImplementedException();
        }

        public void Process(string inputFile, string outputFile)
        {
            throw new NotImplementedException();
        }

        public void Process(string inputFile, ContentType inputType, string outputFile, ContentType outputType)
        {
            throw new NotImplementedException();
        }

        public void Process(Stream inputStream, ContentType inputType, Stream outputStream, ContentType outputType)
        {
            throw new NotImplementedException();
        }

        #region PrivateMethods

        private string GetFileNameExtension(string fileName)
        {
            return Path.GetExtension(fileName).ToLower();
        }

        private ContentType GetContentType(string fileName)
        {
            string extension = GetFileNameExtension(fileName);
            switch (extension)
            {
                default:
                    return ContentType.Unknown;
                case "xnb":
                    return ContentType.Xnb;
                case "json":
                    return ContentType.Json;
                case "png":
                case "jpg:":
                case "bmp":
                    return ContentType.Image;
            }
        }

        #endregion
    }
}
