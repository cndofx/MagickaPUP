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

        private static long MagicBytesGetLongestKeyLength()
        {
            long length = 0;
            foreach (var entry in filesMagicBytes)
                if (entry.Key.Length > length)
                    length = entry.Key.Length;
            return length;
        }

        public static FileType GetFileType(Stream stream)
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
