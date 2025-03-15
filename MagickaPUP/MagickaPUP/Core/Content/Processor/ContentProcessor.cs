using MagickaPUP.Core.Content.Data;
using MagickaPUP.Core.Content.Pipeline.Export;
using MagickaPUP.Core.Content.Pipeline.Export.Derived;
using MagickaPUP.Core.Content.Pipeline.Import;
using MagickaPUP.Core.Content.Pipeline.Import.Derived;
using MagickaPUP.XnaClasses.Xnb;
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

        #region PublicMethods - Process

        public void Process(string inputFile, string outputFile)
        {
            FileType inputContentType = GetContentType(inputFile);
            FileType outputContentType = GetContentType(outputFile);
            Process(inputFile, inputContentType, outputFile, outputContentType);
        }

        public void Process(string inputFile, FileType inputType, string outputFile, FileType outputType)
        {
            using (var inputStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
            using (var outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
            {
                Process(inputStream, inputType, outputStream, outputType);
            }
        }

        public void Process(Stream inputStream, FileType inputType, Stream outputStream, FileType outputType)
        {
            ImportPipeline importer = GetImportPipeline(inputType);
            ExportPipeline exporter = GetExportPipeline(outputType);

            XnbFile xnbFile = importer.Import(inputStream);
            exporter.Export(outputStream, xnbFile);
        }

        public void Process(string fileName)
        {
            string extension = GetFileNameExtension(fileName);
        }

        #endregion

        #region PublicMethods - Process Specific Types

        #endregion

        #region PrivateMethods - ContentType and Extensions

        private string GetFileNameExtension(string fileName)
        {
            return Path.GetExtension(fileName).ToLower();
        }

        private FileType GetContentType(string fileName)
        {
            string extension = GetFileNameExtension(fileName);
            switch (extension) // NOTE : Maybe this would be better suited for a Dict<string, ContentType>, or something like that?
            {
                default:
                    return FileType.Unknown;
                case "xnb":
                    return FileType.Xnb;
                case "json":
                    return FileType.Json;
                case "png":
                case "jpg:":
                case "bmp":
                    return FileType.Image;
            }
        }

        /*
        private string GetOutputExtension(string inputExtension)
        {
            switch (inputExtension)
            {
                case "png"
            }
        }

        private string GetOutputFileName(string inputFileName)
        {
            string inputExtension = Path.GetExtension(inputFileName);
            string outputExtension = GetOutputExtension(inputExtension);


            string outputFileName = Path.GetFileName(inputFileName) + "." + outputExtension;
        }
        */
        #endregion

        #region PrivateMethods - Import Pipeline

        ImportPipeline GetImportPipeline(FileType contentType)
        {
            switch (contentType)
            {
                default:
                    throw new Exception("Unknown input content type");
                case FileType.Xnb:
                    return new XnbImporter();
                case FileType.Json:
                    return new JsonImporter();
                case FileType.Image:
                    return new ImageImporter();
            }
        }

        #endregion

        #region PrivateMethods - Export Pipeline

        ExportPipeline GetExportPipeline(FileType contentType)
        {
            switch (contentType)
            {
                default:
                    throw new Exception("Unknown output content type");
                case FileType.Xnb:
                    return new XnbExporter();
                case FileType.Json:
                    return new JsonExporter();
                case FileType.Image:
                    return new ImageExporter();
            }
        }

        #endregion
    }
}
