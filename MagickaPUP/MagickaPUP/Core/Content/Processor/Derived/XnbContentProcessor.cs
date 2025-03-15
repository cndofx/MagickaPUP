using MagickaPUP.Core.Content.Data;
using MagickaPUP.Core.Content.Pipeline.Export.Derived;
using MagickaPUP.Core.Content.Pipeline.Export;
using MagickaPUP.Core.Content.Pipeline.Import.Derived;
using MagickaPUP.Core.Content.Pipeline.Import;
using MagickaPUP.XnaClasses.Xnb;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Core.Content.Processor.Derived
{
    public class XnbContentProcessor : ContentProcessor<XnbFile>
    {
        public XnbContentProcessor()
        { }

        public override void Pack(Stream inputStream, Stream outputStream)
        {
            var importer = GetImporter(FileTypeDetector.GetFileType(inputStream));
            XnbFile xnbFile = importer.Import(inputStream);

            var exporter = new XnbExporter();
            exporter.Export(outputStream, xnbFile);
        }

        public override void Unpack(Stream inputStream, Stream outputStream)
        {
            var importer = GetImporter(FileTypeDetector.GetFileType(inputStream));
            XnbFile xnbFile = importer.Import(inputStream);

            var exporter = GetExporter(xnbFile);
            exporter.Export(outputStream, xnbFile);
        }

        public override void Process(Stream inputStream, Stream outputStream)
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

        public override void Process(string inputFileName)
        {
            using (var inputStream = new FileStream(inputFileName, FileMode.Open, FileAccess.Read))
            using (var outputStream = new MemoryStream())
            {
                Process(inputStream, outputStream);
                outputStream.Position = 0;
                string outputFile = Path.GetFileName(inputFileName) + "." + FileTypeExtension.GetExtension(FileTypeDetector.GetFileType(outputStream));
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
