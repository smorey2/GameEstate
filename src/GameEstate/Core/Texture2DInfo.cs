using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using static GameEstate.Core.CoreDebug;

namespace GameEstate.Core
{
    /// <summary>
    /// Stores information about a 2D texture.
    /// </summary>
    public class Texture2DInfo
    {
        const int DDS_HEADER_SIZE = 128;
        public enum ByteFormat : byte { Img, Raw, Dxt }

        public ByteFormat DataFormat;
        public int Width, Height;
        public TextureFormat Format;
        public bool HasMipmaps;
        public byte[] Data;

        public Texture2DInfo(ByteFormat byteFormat, TextureFormat format, byte[] data)
        {
            DataFormat = byteFormat;
            switch (byteFormat)
            {
                case ByteFormat.Img: LoadImage(data); return;
                case ByteFormat.Dxt:
                    if (format != TextureFormat.DXT1 && format != TextureFormat.DXT5)
                        throw new ArgumentOutOfRangeException(nameof(format), "Invalid TextureFormat. Only DXT1 and DXT5 formats are supported by this method.");
                    Format = format;
                    var ddsSizeCheck = data[4];
                    if (ddsSizeCheck != 124)
                        throw new ArgumentOutOfRangeException(nameof(data), "Invalid DDS DXTn texture. Unable to read"); // this header byte should be 124 for DDS image files
                    Height = (data[13] << 8) | data[12];
                    Width = (data[17] << 8) | data[16];
                    var dxtBytes = new byte[data.Length - DDS_HEADER_SIZE];
                    Buffer.BlockCopy(dxtBytes, DDS_HEADER_SIZE, dxtBytes, 0, data.Length - DDS_HEADER_SIZE);
                    Data = dxtBytes;
                    return;
                case ByteFormat.Raw: Data = data; return;
                default: throw new ArgumentOutOfRangeException(nameof(byteFormat), $"{byteFormat}");
            }
        }
        public Texture2DInfo(int width, int height, TextureFormat format, bool hasMipmaps, byte[] data)
        {
            DataFormat = ByteFormat.Raw;
            Width = width;
            Height = height;
            Format = format;
            HasMipmaps = hasMipmaps;
            Data = data;
        }

        /// <summary>
        /// Creates a Unity Texture2D from this Texture2DInfo.
        /// </summary>
        public Texture2D ToTexture2D()
        {
            var tex = new Texture2D(Width, Height, Format, HasMipmaps);
            if (Data != null)
            {
                tex.LoadRawTextureData(Data);
                tex.Apply();
                tex.Compress(true);
            }
            return tex;
        }

        public void LoadImage(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var r = new BinaryReader(ms))
            {
                var magicString = r.ReadBytes(4);
                if (!"IMG ".Equals(magicString))
                    throw new FileFormatException($"Invalid IMG file magic string: \"{Encoding.ASCII.GetString(magicString)}\".");
                DataFormat = (ByteFormat)r.ReadByte();
                HasMipmaps = r.ReadByte() != 0;
                Format = (TextureFormat)r.ReadInt16();
                Width = r.ReadInt32();
                Height = r.ReadInt32();
                var rawDataSize = r.ReadInt32();
                Data = r.ReadBytes(rawDataSize);
            }
        }

        public byte[] GetImage()
        {
            using (var ms = new MemoryStream())
            using (var r = new BinaryWriter(ms))
            {
                r.Write(Encoding.ASCII.GetBytes("IMG "));
                r.Write((byte)DataFormat);
                r.Write((byte)DataFormat);
                r.Write((byte)(HasMipmaps ? 1 : 0));
                r.Write((short)Format);
                r.Write(Width);
                r.Write(Height);
                r.Write(Data.Length);
                r.Write(Data);
                ms.Position = 0;
                return ms.ToArray();
            }
        }

        #region Color

        public unsafe Texture2DInfo FromABGR555()
        {
            var W = Width; var H = Height;
            var pixels = new byte[W * H * 4];
            fixed (byte* pPixels = pixels, pData = Data)
            {
                var rPixels = (uint*)pPixels;
                var rData = (ushort*)pData;
                for (var i = 0; i < W * H; ++i)
                {
                    var d555 = *rData++;
                    //var a = 0;// (byte)Math.Min(((d555 & 0x8000) >> 15) * 0x1F, byte.MaxValue);
                    //var r = (byte)Math.Min(((d555 & 0x7C00) >> 10) * 8, byte.MaxValue);
                    //var g = (byte)Math.Min(((d555 & 0x03E0) >> 5) * 8, byte.MaxValue);
                    //var b = (byte)Math.Min(((d555 & 0x001F) >> 0) * 8, byte.MaxValue);

                    var r = (byte)Math.Min(((d555 & 0xF800) >> 11) * 8, byte.MaxValue);     // 1111 1000 0000 0000 = F800
                    var g = (byte)Math.Min(((d555 & 0x07C0) >> 6) * 8, byte.MaxValue);      // 0000 0111 1100 0000 = 07C0
                    var b = (byte)Math.Min(((d555 & 0x003E) >> 1) * 8, byte.MaxValue);      // 0000 0000 0011 1110 = 003E
                    var a = (byte)Math.Min((d555 & 0x0001) * 0x1F, byte.MaxValue);          // 0000 0000 0000 0001 = 0001
                    uint color;
                    if (Format == TextureFormat.RGBA32)
                        color =
                            ((uint)(a << 24) & 0xFF000000) |
                            ((uint)(b << 16) & 0x00FF0000) |
                            ((uint)(g << 8) & 0x0000FF00) |
                            ((uint)(r << 0) & 0x000000FF);
                    else if (Format == TextureFormat.ARGB32)
                        color =
                            ((uint)(b << 24) & 0xFF000000) |
                            ((uint)(g << 16) & 0x00FF0000) |
                            ((uint)(r << 8) & 0x0000FF00) |
                            ((uint)(a << 0) & 0x000000FF);
                    else throw new ArgumentOutOfRangeException(nameof(Format), Format.ToString());
                    *rPixels++ = color;
                }
            }
            Data = pixels;
            return this;
        }

        public Texture2DInfo From8BitPallet(byte[][] pallet, TextureFormat palletFormat)
        {
            if (Format != palletFormat)
                throw new InvalidOperationException();
            var b = new MemoryStream();
            for (var y = 0; y < Data.Length; y++)
                b.Write(pallet[Data[y]], 0, 4);
            Data = b.ToArray();
            return this;
        }

        #endregion

        #region Bitmap

        public unsafe void SaveBitmap(string filePath)
        {
            var rawData = new byte[Width * Height * 4];
            if (Format == TextureFormat.BGRA32)
                Buffer.BlockCopy(Data, 0, rawData, 0, rawData.Length);
            else if (Format == TextureFormat.ARGB32)
                fixed (byte* pPixels = rawData, pData = Data)
                {
                    var rPixels = (uint*)pPixels;
                    var rData = (uint*)pData;
                    for (var i = 0; i < Width * Height; ++i)
                    {
                        var d = *rData++;
                        var b = (byte)(d >> 24);
                        var g = (byte)(d >> 16);
                        var r = (byte)(d >> 8);
                        var a = (byte)d;
                        var color =
                            ((uint)(a << 24) & 0xFF000000) |
                            ((uint)(r << 16) & 0x00FF0000) |
                            ((uint)(g << 8) & 0x0000FF00) |
                            ((uint)(b << 0) & 0x000000FF);
                        *rPixels++ = color;
                    }
                }
            else if (Format == TextureFormat.RGBA32)
                fixed (byte* pPixels = rawData, pData = Data)
                {
                    var rPixels = (uint*)pPixels;
                    var rData = (uint*)pData;
                    for (var i = 0; i < Width * Height; ++i)
                    {
                        var d = *rData++;
                        var a = (byte)(d >> 24);
                        var b = (byte)(d >> 16);
                        var g = (byte)(d >> 8);
                        var r = (byte)d;
                        var color =
                            ((uint)(a << 24) & 0xFF000000) |
                            ((uint)(r << 16) & 0x00FF0000) |
                            ((uint)(g << 8) & 0x0000FF00) |
                            ((uint)(b << 0) & 0x000000FF);
                        *rPixels++ = color;
                    }
                }
            else throw new ArgumentOutOfRangeException(nameof(Format), Format.ToString());
            using (var bmp = new Bitmap(Width, Height, Width * 4, PixelFormat.Format32bppRgb, Marshal.UnsafeAddrOfPinnedArrayElement(rawData, 0)))
                bmp.Save(filePath);
        }

        #endregion

        #region MipMap

        public static int CalculateMipMapCount(int baseTextureWidth, int baseTextureHeight)
        {
            Assert(baseTextureWidth > 0 && baseTextureHeight > 0);
            var longerLength = Mathf.Max(baseTextureWidth, baseTextureHeight);
            var mipMapCount = 0;
            var currentLongerLength = longerLength;
            while (currentLongerLength > 0)
            {
                mipMapCount++;
                currentLongerLength /= 2;
            }
            return mipMapCount;
        }

        public static int CalculateMipMappedTextureDataSize(int baseTextureWidth, int baseTextureHeight, int bytesPerPixel)
        {
            Assert(baseTextureWidth > 0 && baseTextureHeight > 0 && bytesPerPixel > 0);
            var dataSize = 0;
            var currentWidth = baseTextureWidth;
            var currentHeight = baseTextureHeight;
            while (true)
            {
                dataSize += currentWidth * currentHeight * bytesPerPixel;
                if (currentWidth == 1 && currentHeight == 1)
                    break;
                currentWidth = currentWidth > 1 ? (currentWidth / 2) : currentWidth;
                currentHeight = currentHeight > 1 ? (currentHeight / 2) : currentHeight;
            }
            return dataSize;
        }

        // TODO: Improve algorithm for images with odd dimensions.
        public static void Downscale4Component32BitPixelsX2(byte[] srcBytes, int srcStartIndex, int srcRowCount, int srcColumnCount, byte[] dstBytes, int dstStartIndex)
        {
            var bytesPerPixel = 4;
            var componentCount = 4;
            Assert(srcStartIndex >= 0 && srcRowCount >= 0 && srcColumnCount >= 0 && (srcStartIndex + (bytesPerPixel * srcRowCount * srcColumnCount)) <= srcBytes.Length);
            var dstRowCount = srcRowCount / 2;
            var dstColumnCount = srcColumnCount / 2;
            Assert(dstStartIndex >= 0 && (dstStartIndex + (bytesPerPixel * dstRowCount * dstColumnCount)) <= dstBytes.Length);
            for (var dstRowIndex = 0; dstRowIndex < dstRowCount; dstRowIndex++)
                for (var dstColumnIndex = 0; dstColumnIndex < dstColumnCount; dstColumnIndex++)
                {
                    var srcRowIndex0 = 2 * dstRowIndex;
                    var srcColumnIndex0 = 2 * dstColumnIndex;
                    var srcPixel0Index = (srcColumnCount * srcRowIndex0) + srcColumnIndex0;

                    var srcPixelStartIndices = new int[4];
                    srcPixelStartIndices[0] = srcStartIndex + (bytesPerPixel * srcPixel0Index); // top-left
                    srcPixelStartIndices[1] = srcPixelStartIndices[0] + bytesPerPixel; // top-right
                    srcPixelStartIndices[2] = srcPixelStartIndices[0] + (bytesPerPixel * srcColumnCount); // bottom-left
                    srcPixelStartIndices[3] = srcPixelStartIndices[2] + bytesPerPixel; // bottom-right

                    var dstPixelIndex = (dstColumnCount * dstRowIndex) + dstColumnIndex;
                    var dstPixelStartIndex = dstStartIndex + (bytesPerPixel * dstPixelIndex);
                    for (var componentIndex = 0; componentIndex < componentCount; componentIndex++)
                    {
                        var averageComponent = 0F;
                        for (var srcPixelIndex = 0; srcPixelIndex < srcPixelStartIndices.Length; srcPixelIndex++)
                            averageComponent += srcBytes[srcPixelStartIndices[srcPixelIndex] + componentIndex];
                        averageComponent /= srcPixelStartIndices.Length;
                        dstBytes[dstPixelStartIndex + componentIndex] = (byte)Mathf.RoundToInt(averageComponent);
                    }
                }
        }

        #endregion
    }
}