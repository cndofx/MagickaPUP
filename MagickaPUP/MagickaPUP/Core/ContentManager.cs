using System;
using System.IO;
using System.Collections.Generic;
using MagickaPUP.XnaClasses.Xnb;
using System.Text.Json;
using MagickaPUP.Utility.IO;

namespace MagickaPUP.Core
{
    // NOTE : In the past, this used to be separated into 2 different classes: "Packer" and "Unpacker". Merging their logic here made it easier to
    // make the write / read system consistent across the codebase (a read and write method for each type of object).
    // It also allowed to clean up the code and improve code correctness, as well as preventing having to store in memory temporary copies of Packers and Unpackers
    // with locally stored configuration on construction (yes, they used to store this data on construction and sit around idly waiting for the Pack() and Unpack()
    // methods to be called...). Now, this data is held only temporarily on PackContent() and UnpackContent() calls rather than associating it as data local to the class.
    
    // TODO : Get rid of the old Packer and Unpacker classes to clean up the code.

    // TODO : Maybe add some try-catch blocks for permissive exceptions with the old "Cancelling Pack / Unpack Operation..." message logged.
    
    public class ContentManager
    {
        #region Structs

        public struct PackSettings
        {
            public string InputFileName;
            public string OutputFileName;
            public bool CompressOutput;
            public int DebugLevel;
        }

        public struct UnpackSettings
        {
            public string InputFileName;
            public string OutputFileName;
            public int DebugLevel;
            public bool ShouldIndent;
        }

        #endregion

        #region PublicMethods

        public void PackContent(PackSettings settings)
        {
            DebugLogger logger = new DebugLogger("Packer", settings.DebugLevel);

            using (var stream = new FileStream(settings.OutputFileName, FileMode.Create, FileAccess.Write))
            using (var writer = new MBinaryWriter(stream))
            {
                logger?.Log(1, $"Reading contents from input JSON file \"{settings.InputFileName}\"");
                string jsonText = File.ReadAllText(settings.InputFileName);

                logger?.Log(1, "Deserializing JSON string to XNB data...");
                XnbFile xnbFile = JsonSerializer.Deserialize<XnbFile>(jsonText);

                logger?.Log(1, $"Writing data to output XNB file \"{settings.OutputFileName}\"");
                xnbFile.Write(writer, logger);
            }
        }

        public void UnpackContent(UnpackSettings settings)
        {
            DebugLogger logger = new DebugLogger("Unpacker", settings.DebugLevel);

            // using (var stream = new MemoryStream(File.ReadAllBytes(settings.InputFileName))) // NOTE : This other implementation loads the entire file into memory. If the file is compressed, we still need to load all of the decompressed data into memory after decompression, so this would double the amount of memory consumed, but it could speed up reading of non compressed XNB files. In the futture, maybe add a flag to mpup that allows determining whether you want to load all of the data into memory or not?
            using (var stream = new FileStream(settings.InputFileName, FileMode.Open, FileAccess.Read))
            using (var reader = new MBinaryReader(stream))
            {
                logger?.Log(1, $"Reading contents from input XNB file \"{settings.InputFileName}\"");
                XnbFile xnbFile = new XnbFile(reader, logger);

                logger?.Log(1, "Serializing XNB data to JSON string...");
                string jsonText = JsonSerializer.Serialize(xnbFile);

                logger?.Log(1, $"Writing data to output JSON file \"{settings.OutputFileName}\"");
                File.WriteAllText(settings.OutputFileName, jsonText);
            }
        }

        #endregion
    }
}
