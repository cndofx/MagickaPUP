using MagickaPUP.Core.Content.Data;
using MagickaPUP.Core.Content.Pipeline.Export;
using MagickaPUP.Core.Content.Pipeline.Export.Derived;
using MagickaPUP.Core.Content.Pipeline.Import;
using MagickaPUP.Core.Content.Pipeline.Import.Derived;
using MagickaPUP.Core.Content.Pipeline.Import.Helper;
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

        /*
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

        public void Process(Stream inputStream, Stream outputStream, bool forceJsonOutput = false)
        {
            FileType inputType = FileType.Unknown;
            FileType outputType = FileType.Unknown;

            if (forceJsonOutput)
                outputType = FileType.Json;

            if(inputType == FileType.Unknown)
                inputType = FileTypeDetector.GetFileType(inputStream);

            var importer = GetImportPipeline(inputType);
            var xnbFile = importer.Import(inputStream);

            if(outputType == FileType.Unknown)
                outputType = xnbFile.GetExporter().Export(outputStream, xnbFile)
        }

        public void XnbToJson(Stream inputStream, Stream outputStream)
        {
            XnbImporter importer = new XnbImporter();
            JsonExporter exporter = new JsonExporter();

            XnbFile xnbFile = importer.Import(inputStream);
            exporter.Export(outputStream, xnbFile);
        }
        */

        /*
        public void Process(Stream inputStream, Stream outputStream, ImporterBase importer, ExporterBase exporter)
        {
            XnbFile xnbFile = importer.Import(inputStream);
            exporter.Export(outputStream, xnbFile);
        }

        public void Process(string inputFileName, string outputFileName, FileType inputType, FileType outputType)
        {
            ImporterBase importer = GetImportPipeline(inputType);
            ExporterBase exporter = GetExportPipeline(outputType);

            using (var inputStream = new FileStream(inputFileName, FileMode.Open, FileAccess.Read))
            using (var outputStream = new FileStream(outputFileName, FileMode.Create, FileAccess.Write))
            {
                Process(inputStream, outputStream, importer, exporter);
            }
        }

        public void Process(string inputFileName, string outputFileName)
        {
            FileType inputType = FileTypeDetector.GetFileType(inputFileName);
            FileType outputType = FileTypeDetector.GetFileType(outputFileName);

            Process(inputFileName, outputFileName, inputType, outputType);
        }
        */

        public void Process(string inputFileName)
        {
            // FileType inputType = FileTypeDetector.GetFileType(inputFileName);
            // 
            // // TODO : Get rid of this, implement system where we get the file type from the input stream after getting the xnb data, otherwise we just cannot know
            // // for sure whether we want a json or a png or whatever the fuck...
            // 
            // switch (inputType)
            // {
            //     
            // }
            // 
            // FileType outputType = FileTypeDetector.GetFileType(outputFileName);
            // 
            // string outputFileName = Path.GetFileName(inputFileName) + "." + FileTypeExtension.GetExtension(outputType);
            // 
            // Process(inputFileName, outputFileName, inputType, outputType);
        }

        // XNB -> JSON, PNG, etc...
        public void Unpack<T>(Stream inputStream, Stream outputStream)
        {
            var importer = new XnbImporter();
            XnbFile xnbFile = importer.Import(inputStream);

            var exporter = xnbFile.XnbFileData.PrimaryObject.GetUnpackExporter();
            exporter.Export(outputStream, xnbFile);
        }

        // public void Pack()

        /*
        public void Process(Stream inputStream, Stream outputStream)
        {
            FileType inputType;
            FileType outputType;

            inputType = FileTypeDetector.GetFileType(inputStream);
            switch (inputType)
            {
                case FileType.Xnb:
                    {
                        var importer = new XnbImporter();
                        XnbFile xnbFile = importer.Import(inputStream);

                        var exporter = xnbFile.GetExporter();
                        exporter.Export(xnbFile);
                    }
                    break;
                case FileType.Json:
                    importer = new JsonImporter();
                    break;
                case FileType.Image:
                    importer = new ImageImporter();
                    break;
                default:
                    importer = new UnknownTypeImporter();
                    break;
            }

        }
        */

        public void Unpack(Stream inputStream, Stream outputStream)
        {
            FileType inputType = FileTypeDetector.GetFileType(inputStream);
            switch (inputType)
            {
                case FileType.Xnb:
                    UnpackXnb(inputStream, outputStream);
                    break;
            }
        }

        public void UnpackXnb(Stream inputStream, Stream outputStream)
        {

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

        /*
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
        */

        #endregion

        #region PrivateMethods - Export Pipeline

        /*
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
        */

        #endregion
    }
}
