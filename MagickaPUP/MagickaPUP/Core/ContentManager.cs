using System;
using System.IO;
using System.Collections.Generic;
using MagickaPUP.XnaClasses.Xnb;
using System.Text.Json;
using MagickaPUP.Utility.IO;

namespace MagickaPUP.Core
{
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

                logger?.Log(1, $"Reading contents from input JSON file {settings.InputFileName}");
                string jsonText = File.ReadAllText(settings.InputFileName);

                logger?.Log(1, "Deserializing JSON file...");
                XnbFile xnbFile = JsonSerializer.Deserialize<XnbFile>(jsonText);

                logger?.Log(1, $"Writing data to output XNB file {settings.OutputFileName}");
                xnbFile.Write(writer, logger);
            }
        }

        public void UnpackContent(UnpackSettings settings)
        { }

        #endregion

        #region PrivateMethods - Packer (JSON to XNB)

        #endregion

        #region PrivateMethods - Unpacker (XNB to JSON)

        #endregion
    }
}
