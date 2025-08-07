﻿using MagickaPUP.Core.Content.Data;
using MagickaPUP.Core.Content.Pipeline.Export.Derived;
using MagickaPUP.Core.Content.Pipeline.Export;
using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses.Xnb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using MagickaPUP.Utility.Compression.Dxt;
using MagickaPUP.MagickaClasses.Liquids;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Color = System.Drawing.Color;
using Rectangle = System.Drawing.Rectangle;

namespace MagickaPUP.XnaClasses
{
    // Surface format is an i32 used by XNA framework to determine the format type of the texture within the XNB file.
    // It determines how the RGB data is laid out within the file, and what type of data is stored within the file.
    // This information is crucial to be able to properly interpret the contents of the data buffer of the image.
    public enum SurfaceFormat
    {
        Color = 1,
        Bgr32,
        Bgra1010102,
        Rgba32,
        Rgb32,
        Rgba1010102,
        Rg32,
        Rgba64,
        Bgr565,
        Bgra5551,
        Bgr555,
        Bgra4444,
        Bgr444,
        Bgra2338,
        Alpha8,
        Bgr233,
        Bgr24,
        NormalizedByte2,
        NormalizedByte4,
        NormalizedShort2,
        NormalizedShort4,
        Single,
        Vector2,
        Vector4,
        HalfSingle,
        HalfVector2,
        HalfVector4,
        Dxt1,
        Dxt2,
        Dxt3,
        Dxt4,
        Dxt5, // format = 32, DXT5 compressed pixel format for DDS image texture files. The one used by Magicka's assets.
        Luminance8,
        Luminance16,
        LuminanceAlpha8,
        LuminanceAlpha16,
        Palette8,
        PaletteAlpha16,
        NormalizedLuminance16,
        NormalizedLuminance32,
        NormalizedAlpha1010102,
        NormalizedByte2Computed,
        VideoYuYv,
        VideoUyVy,
        VideoGrGb,
        VideoRgBg,
        Multi2Bgra32,
        Depth24Stencil8,
        Depth24Stencil8Single,
        Depth24Stencil4,
        Depth24,
        Depth32,
        Depth16 = 54,
        Depth15Stencil1 = 56,
        Unknown = -1
    }

    /*
        NOTES:
        
        Magicka's textures are stored within the XNB files with SurfaceFormat 32, which corresponds to DXT5.
        
        DXT5 is a format in which the textures are compressed to 8 bits per pixel, usually used to compress RGBA textures.
        This means that, for an RGBA texture, which has 4 channel for each pixel, 2 bits are dedicated to each channel, so each pixel is 1 byte.

        If this sounds like a low value to represent a large amount of colors, don't worry, that's because it is. The reason this works like that
        is that DXT5 is a compressed format, so in the file, each pixel takes up 1 byte in a compressed format. When decompressed, it obviously takes up more space.
        
        (note that DXT1 is mostly used for RGB images.)

        In short, DXT5 is just a compression format for images. The file format by which this texture file is most commonly known is DDS.
        DDS files (with extension ".dds", which means DirectDraw Surface) can have any kind of DXT compression.

        This means that Magicka's textures are actually DDS files where the DDS header is removed and only the compressed data is preserved, as well as the resolution
        of the texture's largest mip map, the total number of mip maps stored within the file and the format of the pixel data, specifically in this case, the compression format.

        As you may already be able to tell from the name DDS "DirectDraw Surface", yes, this is a format made by Microsoft to store texture data in a compressed way.
        DDS images can both contain compressed and uncompressed data.

        The good thing about this format is that, since it is supported out of the box by XNA's Textures (duh, it's MS as well lol), then we don't even need to make a tool
        to translate BMPs, JPEGs or PNGs or whatever to XNB files. We can just use any image editor capable of reading and writing DDS images.
    */

    public class Texture2DData : XnaObject
    {
        #region Variables

        public int dataSize { get; set; }
        // public uint[] imageData { get; set; }
        public byte[] imageData { get; set; }

        #endregion

        #region Constructor

        public Texture2DData()
        {
            this.dataSize = 0;
            this.imageData = new byte[this.dataSize];
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Texture2DData...");

            this.dataSize = reader.ReadInt32();

            // Ugly hack to read byte array as numbers using C#'s built in Json deserializer without it using base 64 strings...
            this.imageData = new byte[this.dataSize];
            for (int i = 0; i < this.dataSize; ++i)
                this.imageData[i] = reader.ReadByte();
        }

        public static Texture2DData Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new Texture2DData();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Texture2DData...");

            writer.Write(this.dataSize);

            writer.Write(this.imageData);

            // Ugly hack to read byte array as numbers using C#'s built in Json serializer without it using base 64 strings...
            // for (int i = 0; i < this.dataSize; ++i)
            //     writer.Write((byte)this.imageData[i]);
        }

        #endregion
    }

    public class Texture2D : XnaObject
    {
        #region Variables

        public SurfaceFormat format { get; set; }
        
        public int width { get; set; }
        public int height { get; set; }

        public int mipCount { get; set; }

        public Texture2DData[] data { get; set; }

        #endregion

        #region Constructor

        public Texture2D()
        {
            this.format = SurfaceFormat.Unknown;

            this.width = 0;
            this.height = 0;

            this.mipCount = 0;
            this.data = new Texture2DData[this.mipCount];
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Texture2D...");

            this.format = (SurfaceFormat)reader.ReadInt32();

            this.width = reader.ReadInt32();
            this.height = reader.ReadInt32();

            this.mipCount = reader.ReadInt32();

            logger?.Log(2, $" - Texture2D contains {this.mipCount} mip maps.");

            this.data = new Texture2DData[this.mipCount];
            for (int i = 0; i < this.mipCount; ++i)
            {
                this.data[i] = Texture2DData.Read(reader, logger);
            }

        }

        public static Texture2D Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new Texture2D();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Texture2D...");

            writer.Write((int)this.format);

            writer.Write(this.width);
            writer.Write(this.height);

            writer.Write(this.mipCount);
            logger?.Log(2, $" - Writing {this.mipCount} mip maps.");
            for (int i = 0; i < this.mipCount; ++i)
                this.data[i].WriteInstance(writer, logger);
        }

        // Extract the internal bitmap data from the Texture2D
        // TODO : Maybe move this function to be part of the Texture2DData class, and then this one could be GetBitmap(mipIndex)
        public Image GetBitmap()
        {
            byte[] imageDataBuffer = this.data[0].imageData;
            bool mustSwapChannelsRB = false;

            Console.WriteLine($"The format is : \"{this.format}\"");

            switch (this.format)
            {
                // case SurfaceFormat.Color: // Color is OK, fully supported, no extra steps required
                //     break;
                case SurfaceFormat.Dxt1:
                    imageDataBuffer = Dxt1Decompressor.Decompress(imageDataBuffer, this.width, this.height);
                    mustSwapChannelsRB = true;
                    break;
                case SurfaceFormat.Dxt2:
                case SurfaceFormat.Dxt3: // DXT2 and DXT3 are the same algorithmically. The difference is that DXT2 assumes premultiplied alpha, so we need to handle that later on. For now, this is pretty meh, but ok enough. TODO : Implement proper DXT2 handling...
                    imageDataBuffer = Dxt3Decompressor.Decompress(imageDataBuffer, this.width, this.height);
                    mustSwapChannelsRB = true;
                    break;
                case SurfaceFormat.Dxt4:
                case SurfaceFormat.Dxt5: // Same as above...
                    imageDataBuffer = Dxt5Decompressor.Decompress(imageDataBuffer, this.width, this.height);
                    mustSwapChannelsRB = true;
                    break;
                default:
                    // For some reason BGR32 is the same as RGB32? wtf? shouldn't the channels be swapped around??
                    break;// throw new Exception($"Unsupported surface format: \"{this.format}\"");
            }

            // Swap the R and B channels, since the data is in BGR rather than RGB
            if (mustSwapChannelsRB)
            {
                for (int i = 0; i < width * height; ++i)
                {
                    byte temp = imageDataBuffer[i * 4 + 0];
                    imageDataBuffer[i * 4 + 0] = imageDataBuffer[i * 4 + 2];
                    imageDataBuffer[i * 4 + 2] = temp;
                }
            }

            // Bitmap bmp = new Bitmap(this.width, this.height, PixelFormat.Format32bppArgb);
            // BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, this.width, this.height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            // IntPtr data = bmpData.Scan0;
            // Marshal.Copy(imageDataBuffer, 0, data, imageDataBuffer.Length);
            // bmp.UnlockBits(bmpData);
            
            // code above uses Argb32, but Bgra32 is needed to get the correct output. why?
            // Image<Argb32> bmp = Image.LoadPixelData<Argb32>(imageDataBuffer, this.width, this.height);
            Image<Bgra32> bmp = Image.LoadPixelData<Bgra32>(imageDataBuffer, this.width, this.height);
            
            return bmp;
        }

        // Assign the internal bitmap data to the Texture2D
        // TODO : Maybe move this function to be part of the Texture2DData class, and then this one could be SetBitmap(bitmap, mipIndex)
        public void SetBitmap<TPixel>(Image<TPixel> bitmap)
            where TPixel : unmanaged, IPixel<TPixel>
        {
            this.format = SurfaceFormat.Color;

            this.width = bitmap.Width;
            this.height = bitmap.Height;

            this.mipCount = 1;

            this.data = new Texture2DData[1];
            this.data[0] = new Texture2DData();
            this.data[0].dataSize = this.width * this.height * 4; // For now we're using 32 bpp color to encode our RGBA data, so we're going to have a sizeof(pixel) = 4 bytes = 32 bits, that's where the size comes from. Same is assumed on the bitmap extract / get code above.
            this.data[0].imageData = new byte[this.data[0].dataSize];

            // int globalIndex = 0; // Lazy af, should use a x + y * N fix but I'm short on time rn and this shit is good enough.
            // for (int i = 0; i < this.height; ++i)
            // {
            //     for (int j = 0; j < this.width; ++j)
            //     {
            //         Color pixelColor = bitmap.GetPixel(j, i);
            //
            //         // Remember that the SurfaceFormat Color has BGRA ordering rather than RGBA ordering
            //         this.data[0].imageData[globalIndex + 0] = pixelColor.B;
            //         this.data[0].imageData[globalIndex + 1] = pixelColor.G;
            //         this.data[0].imageData[globalIndex + 2] = pixelColor.R;
            //         this.data[0].imageData[globalIndex + 3] = pixelColor.A;
            //
            //         globalIndex += 4;
            //     }
            // }
            
            bitmap.ProcessPixelRows(pixelAccessor =>
            {
                int globalIndex = 0; // Lazy af, should use a x + y * N fix but I'm short on time rn and this shit is good enough.
                for (int y = 0; y < this.height; ++y)
                {
                    Span<TPixel> row = pixelAccessor.GetRowSpan(y);
                    for (int x = 0; x < this.width; ++x)
                    {
                        TPixel pixel = row[x];
                        Rgba32 rgbaPixel = new();
                        pixel.ToRgba32(ref rgbaPixel);
                
                        // Remember that the SurfaceFormat Color has BGRA ordering rather than RGBA ordering
                        this.data[0].imageData[globalIndex + 0] = rgbaPixel.B;
                        this.data[0].imageData[globalIndex + 1] = rgbaPixel.G;
                        this.data[0].imageData[globalIndex + 2] = rgbaPixel.R;
                        this.data[0].imageData[globalIndex + 3] = rgbaPixel.A;
                
                        globalIndex += 4;
                    }
                }
            });
        }

        public override ContentTypeReader GetObjectContentTypeReader()
        {
            return new ContentTypeReader("Microsoft.Xna.Framework.Content.Texture2DReader", 0);
        }

        #endregion

        public override FileType GetFileTypeUnpacked()
        {
            return FileType.Image;
        }

        public override ExporterBase<XnbFile> GetUnpackExporter()
        {
            return new ImageExporter();
        }
    }
}
