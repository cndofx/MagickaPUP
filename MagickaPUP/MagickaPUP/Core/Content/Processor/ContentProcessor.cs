using MagickaPUP.Core.Content.Data;
using MagickaPUP.Core.Content.Pipeline.Export;
using MagickaPUP.Core.Content.Pipeline.Export.Derived;
using MagickaPUP.Core.Content.Pipeline.Import;
using MagickaPUP.Core.Content.Pipeline.Import.Derived;
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
            ContentType inputContentType = GetContentType(inputFile);
            ContentType outputContentType = GetContentType(outputFile);
            Process(inputFile, inputContentType, outputFile, outputContentType);
        }

        public void Process(string inputFile, ContentType inputType, string outputFile, ContentType outputType)
        {
            using (var inputStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
            using (var outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
            {
                Process(inputStream, inputType, outputStream, outputType);
            }
        }

        public void Process(Stream inputStream, ContentType inputType, Stream outputStream, ContentType outputType)
        {
            throw new NotImplementedException();
        }

        #region PrivateMethods - ContentType and Extensions

        private string GetFileNameExtension(string fileName)
        {
            return Path.GetExtension(fileName).ToLower();
        }

        private ContentType GetContentType(string fileName)
        {
            string extension = GetFileNameExtension(fileName);
            switch (extension) // NOTE : Maybe this would be better suited for a Dict<string, ContentType>, or something like that?
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

        #region PrivateMethods - Import Pipeline

        ImportPipeline GetImportPipeline(ContentType contentType)
        {
            switch (contentType)
            {
                default:
                    throw new Exception("Unknown input content type");
                case ContentType.Xnb:
                    return new XnbImporter();
                case ContentType.Json:
                    return new JsonImporter();
                case ContentType.Image:
                    return new ImageImporter();
            }
        }

        #endregion

        #region PrivateMethods - Export Pipeline

        ExportPipeline GetExportPipeline(ContentType contentType)
        {
            switch (contentType)
            {
                default:
                    throw new Exception("Unknown output content type");
                case ContentType.Xnb:
                    return new XnbExporter();
                case ContentType.Json:
                    return new JsonExporter();
                case ContentType.Image:
                    return new ImageExporter();
            }
        }

        #endregion
    }
}
