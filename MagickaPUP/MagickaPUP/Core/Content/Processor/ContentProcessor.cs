using MagickaPUP.Core.Content.Data;
using MagickaPUP.Core.Content.Pipeline;
using MagickaPUP.Core.Content.Pipeline.Export;
using MagickaPUP.Core.Content.Pipeline.Export.Derived;
using MagickaPUP.Core.Content.Pipeline.Import;
using MagickaPUP.Core.Content.Pipeline.Import.Derived;
using MagickaPUP.Core.Content.Pipeline.Import.Helper;
using MagickaPUP.XnaClasses;
using MagickaPUP.XnaClasses.Xnb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Core.Content.Processor
{
    // Processes content of a given type T.
    // For example, specialises in a pipeline for content that translates data back and forth between different formats, but internally, the format is T
    // eg: T = XnbFile, T = XwbFile, etc... stuff like that.
    public class ContentProcessor
    {
        public ContentProcessor()
        { }

        public void Pack(Stream inputStream, Stream outputStream)
        {
            var importer = GetImporter(FileTypeDetector.GetFileType(inputStream));
            XnbFile xnbFile = importer.Import(inputStream);

            var exporter = new XnbExporter();
            exporter.Export(outputStream, xnbFile);
        }

        public void Unpack(Stream inputStream, Stream outputStream)
        {
            var importer = GetImporter(FileTypeDetector.GetFileType(inputStream));
            XnbFile xnbFile = importer.Import(inputStream);

            var exporter = GetExporter(xnbFile);
            exporter.Export(outputStream, xnbFile);
        }

        public void Process(Stream inputStream, Stream outputStream)
        {
            var inputType = FileTypeDetector.GetFileType(inputStream);
            if (inputType == FileType.Xnb)
            {
                Unpack(inputStream, outputStream);
            }
            else
            {
                Pack(inputStream, outputStream);
            }
        }

        public void Process(string inputFile)
        {
            using (var inputStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
            using (var outputStream = new MemoryStream())
            {
                Process(inputStream, outputStream);
                outputStream.Position = 0;
                string outputFile = Path.GetFileName(inputFile) + "." + FileTypeExtension.GetExtension(FileTypeDetector.GetFileType(outputStream));
                // File.WriteAllBytes(outputFile, outputStream.ToArray());
                using (var outputFileStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                {
                    outputStream.WriteTo(outputFileStream);
                }
            }
        }

        private ImporterBase<XnbFile> GetImporter(FileType fileType)
        {
            switch (fileType)
            {
                case FileType.Xnb:
                    return new XnbImporter();
                case FileType.Image:
                    return new ImageImporter();
                case FileType.Json:
                    return new JsonImporter();
            }
            return null;
        }

        private ExporterBase<XnbFile> GetExporter(XnbFile xnbFile)
        {
            if (xnbFile.XnbFileData.PrimaryObject is Texture2D)
                return new ImageExporter();
            return new JsonExporter();
        }

    }
}
