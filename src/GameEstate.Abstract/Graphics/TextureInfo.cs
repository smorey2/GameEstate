using System;
using System.Collections.Generic;
using System.IO;
using static GameEstate.Core.CoreDebug;

namespace GameEstate.Graphics
{
    /// <summary>
    /// Stores information about a texture.
    /// </summary>
    public class TextureInfo : Dictionary<string, object>
    {
        public int Width, Height, Depth;
        public TextureUnityFormat UnityFormat;
        public TextureGLFormat GLFormat;
        public TextureFlags Flags;
        public ushort Mipmaps;
        public byte BytesPerPixel;
        public byte[] Data;
        public Action Decompress;
        public int[] CompressedSizeForMipLevel;

        //public TextureInfo() { }
        //public TextureInfo(int width, int height, ushort mipmaps, byte[] data)
        //{
        //    Width = width;
        //    Height = height;
        //    Mipmaps = mipmaps;
        //    Data = data;
        //}

        public BinaryReader GetDecompressedBuffer(int offset)
        {
            throw new NotImplementedException();
        }

        //BinaryReader GetDecompressedBuffer()
        //{
        //    if (!IsActuallyCompressedMips)
        //    {
        //        return Reader;
        //    }

        //    var outStream = new MemoryStream(GetDecompressedTextureAtMipLevel(MipmapLevelToExtract), false);

        //    return new BinaryReader(outStream); // TODO: dispose
        //}

        //public byte[] GetDecompressedTextureAtMipLevel(int mipLevel)
        //{
        //    var uncompressedSize = CalculateBufferSizeForMipLevel(mipLevel);

        //    if (!IsActuallyCompressedMips)
        //    {
        //        return Reader.ReadBytes(uncompressedSize);
        //    }

        //    var compressedSize = CompressedMips[mipLevel];

        //    if (compressedSize >= uncompressedSize)
        //    {
        //        return Reader.ReadBytes(uncompressedSize);
        //    }

        //    var input = Reader.ReadBytes(compressedSize);
        //    var output = new Span<byte>(new byte[uncompressedSize]);

        //    LZ4Codec.Decode(input, output);

        //    return output.ToArray();
        //}

        #region MipMap

        public int GetDataOffsetForMip(int mipLevel)
        {
            if (Mipmaps < 2)
                return 0;

            var offset = 0;
            for (var j = Mipmaps - 1; j > mipLevel; j--)
                offset += CompressedSizeForMipLevel != null
                    ? CompressedSizeForMipLevel[j]
                    : GetMipmapDataSize(Width, Height, Depth, GLFormat, j) * (Flags.HasFlag(TextureFlags.CUBE_TEXTURE) ? 6 : 1);
            return offset;
        }

        public Span<byte> GetDataSpanForMip(int mipLevel)
        {
            return null;
            //var offset = GetDataOffsetForMip(mipLevel);
            //var dataSize = GetMipmapDataSize(Width, Height, Depth, GLFormat, mipLevel);

            //if (CompressedSizeForMipLevel == null)
            //{
            //    return new Span<byte>(Data, 10, 10);
            //}

            //var compressedSize = CompressedSizeForMipLevel[mipLevel];
            //if (compressedSize >= dataSize)
            //{
            //    return Reader.ReadBytes(dataSize);
            //}

            //var input = Reader.ReadBytes(compressedSize);
            //var output = new Span<byte>(new byte[dataSize]);

            //LZ4Codec.Decode(input, output);

            //return output.ToArray();
        }

        public static int GetMipmapCount(int width, int height)
        {
            Assert(width > 0 && height > 0);
            var longerLength = Math.Max(width, height);
            var mipMapCount = 0;
            var currentLongerLength = longerLength;
            while (currentLongerLength > 0)
            {
                mipMapCount++;
                currentLongerLength /= 2;
            }
            return mipMapCount;
        }

        public int GetMipmapDataSize()
        {
            Assert(Width > 0 && Height > 0 && BytesPerPixel > 0);
            var dataSize = 0;
            var currentWidth = Width;
            var currentHeight = Height;
            while (true)
            {
                dataSize += currentWidth * currentHeight * BytesPerPixel;
                if (currentWidth == 1 && currentHeight == 1)
                    break;
                currentWidth = currentWidth > 1 ? (currentWidth / 2) : currentWidth;
                currentHeight = currentHeight > 1 ? (currentHeight / 2) : currentHeight;
            }
            return dataSize;
        }

        public static int GetMipmapDataSize(int width, int height, int depth, TextureGLFormat format, int mipLevel)
        {
            var bytesPerPixel = format.GetBlockSize();
            var currentWidth = width >> mipLevel;
            var currentHeight = height >> mipLevel;
            var currentDepth = depth >> mipLevel;

            if (currentDepth < 1)
                currentDepth = 1;

            if (format == TextureGLFormat.DXT1
            || format == TextureGLFormat.DXT5
            || format == TextureGLFormat.BC6H
            || format == TextureGLFormat.BC7
            || format == TextureGLFormat.ETC2
            || format == TextureGLFormat.ETC2_EAC
            || format == TextureGLFormat.ATI1N)
            {
                var misalign = currentWidth % 4;

                if (misalign > 0)
                    currentWidth += 4 - misalign;

                misalign = currentHeight % 4;

                if (misalign > 0)
                    currentHeight += 4 - misalign;

                if (currentWidth < 4 && currentWidth > 0)
                    currentWidth = 4;

                if (currentHeight < 4 && currentHeight > 0)
                    currentHeight = 4;

                if (currentDepth < 4 && currentDepth > 1)
                    currentDepth = 4;

                var numBlocks = (currentWidth * currentHeight) >> 4;
                numBlocks *= currentDepth;

                return numBlocks * bytesPerPixel;
            }

            return currentWidth * currentHeight * currentDepth * bytesPerPixel;
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
                        dstBytes[dstPixelStartIndex + componentIndex] = (byte)Math.Round(averageComponent);
                    }
                }
        }

        public byte[] GetTexture(int offset)
        {
            throw new NotImplementedException();
        }

        public byte[] GetDecompressedTextureAtMipLevel(int offset, int v)
        {
            throw new NotImplementedException();
        }

        internal static int GetMipmapDataSize(int dwWidth, int dwHeight, int v, object bytesPerPixel)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}