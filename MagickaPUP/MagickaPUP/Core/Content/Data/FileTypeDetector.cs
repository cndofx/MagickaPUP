using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Core.Content.Data
{
    // Helper static class with utility functions to get the file type of an input stream of data
    public static class FileTypeDetector
    {
        private static readonly Dictionary<byte[], FileType> filesMagicBytes = new()
        {
            { new byte[] { 0x7B }, FileType.Json },
            { new byte[] { 0x58, 0x4E, 0x42 }, FileType.Xnb },
            { new byte[] { 0x89, 0x50, 0x4E, 0x47 }, FileType.Image } // TODO : Add support for all other types of formats
        };
        private static readonly int filesMagicBytesBufferLength = GetMagicBytesMaxLength(); // Store the largest length of all the magic bytes sequences stored in the dict. Wish we could make this constexpr tho since the dict is never going to be modified during runtime...

        private static int GetMagicBytesMaxLength()
        {
            int length = 0;
            foreach (var entry in filesMagicBytes)
                if (entry.Key.Length > length)
                    length = entry.Key.Length;
            return length;
        }

        // NOTE : An issue exists when reading streams that contain less bytes than the largest expected identifier sequence, and that is quite dangerous...
        public static FileType GetFileType(Stream stream)
        {
            FileType ans = FileType.Unknown;

            // Buffer to store the sequence of magic bytes for the stream we're reading to help detect the file type
            byte[] magicBytes = new byte[filesMagicBytesBufferLength];
            stream.Read(magicBytes, 0, magicBytes.Length);

            // Get the current position within the input stream so that we can restore it later
            long startPosition = stream.Position;

            // Find the corresponding magic bytes sequence for the current file data
            foreach (var entry in filesMagicBytes)
            {
                if (BuffersAreEqual(entry.Key, magicBytes, entry.Key.Length))
                {
                    ans = entry.Value;
                    break;
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
    }
}
