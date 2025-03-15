using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Core.Content.Data
{
    // Helper static class with utility functions to get the file type of an input stream of data
    // We do this instead of trusting extensions so that we can ensure that we can load just fine even files with wrong extension or no extension or streams whose
    // origin we don't know (eg: users who generate data in memory and make calls to MagickaPUP as if it were a library rather than using the CLI program)
    public static class FileTypeDetector
    {
        private static readonly Dictionary<byte[], FileType> filesMagicBytes = new()
        {
            { new byte[] { 0x7B }, FileType.Json },
            { new byte[] { 0x58, 0x4E, 0x42 }, FileType.Xnb },
            { new byte[] { 0x89, 0x50, 0x4E, 0x47 }, FileType.Image } // TODO : Add support for all other types of formats
        };
        private static readonly long longestKeyLength = MagicBytesGetLongestKeyLength(); // Store the largest length of all the magic bytes sequences stored in the dict. Wish we could make this constexpr tho since the dict is never going to be modified during runtime...

        private static readonly Dictionary<string, FileType> fileExtensions = new()
        {
            { "json", FileType.Json},
            { "xnb", FileType.Xnb },
            { "png", FileType.Image }
        };

        private static long MagicBytesGetLongestKeyLength()
        {
            long length = 0;
            foreach (var entry in filesMagicBytes)
                if (entry.Key.Length > length)
                    length = entry.Key.Length;
            return length;
        }

        // We don't trust extensions here, so we check the data from the stream rather than following what the extension says...
        // NOTE : This function should never be used as it is pointless to open the stream, check the file type, close the stream, and then open it
        // again to process it once more... but it exists as a helper for simpler cases.
        public static FileType FileTypeFromStream(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                return GetFileType(stream);
            }
        }

        public static FileType FileTypeFromStream(Stream stream)
        {
            FileType ans = FileType.Unknown;

            long bufferLength = GetBufferLength(stream.Length);

            // Buffer to store the sequence of magic bytes for the stream we're reading to help detect the file type
            byte[] magicBytes = new byte[bufferLength];
            stream.Read(magicBytes, 0, magicBytes.Length);

            // Get the current position within the input stream so that we can restore it later
            long startPosition = stream.Position;

            // Find the corresponding magic bytes sequence for the current file data
            foreach (var entry in filesMagicBytes)
            {
                if (entry.Key.Length <= bufferLength) // Ensures that streams that are shorter in length than the length of a given file identifier are skipped (for example, we have a valid empty JSON file, that's 2 bytes "{}", but checking against the key "XNB" would read 1 byte out of bounds. This prevents that issue.
                {
                    if (BuffersAreEqual(entry.Key, magicBytes, entry.Key.Length))
                    {
                        ans = entry.Value;
                        break;
                    }
                }
            }

            // Restore the position from which we started reading
            stream.Position = startPosition;

            return ans;
        }

        // We don't trust extensions here, BUT... it can still be useful to use extensions to identify files instead of checking stream data in some cases.
        // The primary reason for adding support for this is the fact that:
        //  -> Linux-world type of tools usually check file types by reading magic bits. (eg: default behaviour of trying to open an sh file that is marked as executable is running the script inside)
        //  -> Windows-world type of tools usually check file types by reading the extension. (eg: default behaviour of trying to open a bat file that has a different extension is to open it as whatever the extension is rather than the file contents)
        // In the case of XNA and monogame, the behaviour is kind of inconsistent. Some things are extension based, others are contents based...
        public static FileType FileTypeFromExtension(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLower();
            FileType ans = FileType.Unknown;
            if (fileExtensions.ContainsKey(extension))
                return fileExtensions[extension];
            return ans;
        }

        private static bool BuffersAreEqual(byte[] buffer1, byte[] buffer2, int length)
        {
            for (int i = 0; i < length; ++i)
                if (buffer1[i] != buffer2[i])
                    return false;
            return true;
        }

        private static long GetBufferLength(long streamLength)
        {
            return Math.Min(longestKeyLength, streamLength);
        }
    }
}
