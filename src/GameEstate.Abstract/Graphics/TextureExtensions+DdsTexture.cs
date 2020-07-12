﻿using GameEstate.Core;
using GameEstate.Graphics.DirectX;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace GameEstate.Graphics
{
    /// <summary>
    /// TextureExtensions
    /// </summary>
    static partial class TextureExtensions
    {
        public static TextureInfo LoadDxtTexture(this TextureInfo source, TextureUnityFormat format, byte[] data)
        {
            if (format != TextureUnityFormat.DXT1 && format != TextureUnityFormat.DXT5)
                throw new ArgumentOutOfRangeException(nameof(format), "Invalid TextureFormat. Only DXT1 and DXT5 formats are supported by this method.");
            source.UnityFormat = format;
            var ddsSize = data[4];
            if (ddsSize != DDS_HEADER.SizeOf)
                throw new ArgumentOutOfRangeException(nameof(data), "Invalid DDS DXTn texture. Unable to read");
            source.Height = (data[13] << 8) | data[12];
            source.Width = (data[17] << 8) | data[16];
            var fileData = new byte[data.Length - DDS_HEADER.SizeOf];
            Buffer.BlockCopy(fileData, DDS_HEADER.SizeOf, fileData, 0, data.Length - DDS_HEADER.SizeOf);
            source.Data = fileData;
            return source;
        }

        /// <summary>
        /// Loads a DDS texture from a file.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static TextureInfo LoadDdsTexture(this TextureInfo source, string path) => source.LoadDdsTexture(File.Open(path, FileMode.Open, FileAccess.Read));

        /// <summary>
        /// Loads the DDS texture.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static TextureInfo LoadDdsTexture(this TextureInfo source, byte[] data) => source.LoadDdsTexture(new MemoryStream(data));

        /// <summary>
        /// Loads a DDS texture from an input stream.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        /// <exception cref="FileFormatException">Invalid DDS file magic string: \"{Encoding.ASCII.GetString(magicString)}\".</exception>
        public unsafe static TextureInfo LoadDdsTexture(this TextureInfo source, Stream stream)
        {
            using (var r = new BinaryReader(stream))
            {
                // Check the magic string.
                var magicString = r.ReadBytes(4);
                if (!DDS_HEADER.Literal.DDS_.SequenceEqual(magicString))
                    throw new FileFormatException($"Invalid DDS file magic string: \"{Encoding.ASCII.GetString(magicString)}\".");
                // Deserialize the DDS file header.
                var header = r.ReadT<DDS_HEADER>(DDS_HEADER.SizeOf);
                header.Verify();
                if (DDS_HEADER.Literal.DX10.SequenceEqual(header.ddspf.dwFourCC))
                    r.ReadT<DDS_HEADER_DXT10>(DDS_HEADER_DXT10.SizeOf);
                // Figure out the texture format and load the texture data.
                header.DecodeAndRead(source, r);
                // Post-process the texture to generate missing mipmaps and possibly flip it vertically.
                //PostProcessDDSTexture((int)header.dwWidth, (int)header.dwHeight, bytesPerPixel, hasMipmaps, (int)ddsMipmapLevelCount, textureData, flipVertically);
                //return new TextureInfo((int)header.dwWidth, (int)header.dwHeight, textureFormat, hasMipmaps, textureData);
                return source;
            }
        }



        //        /// <summary>
        //        /// Decodes a DXT1-compressed 4x4 block of texels using a prebuilt 4-color color table.
        //        /// </summary>
        //        /// <remarks>See https://msdn.microsoft.com/en-us/library/windows/desktop/bb694531(v=vs.85).aspx#BC1 </remarks>
        //        static Color32[] DecodeDXT1TexelBlock(BinaryReader r, Color[] colorTable)
        //        {
        //            Assert(colorTable.Length == 4);
        //            // Read pixel color indices.
        //            var colorIndices = new uint[16];
        //            var colorIndexBytes = new byte[4];
        //            r.Read(colorIndexBytes, 0, colorIndexBytes.Length);
        //            const uint bitsPerColorIndex = 2;
        //            for (var rowIndex = 0U; rowIndex < 4; rowIndex++)
        //            {
        //                var rowBaseColorIndexIndex = 4 * rowIndex;
        //                var rowBaseBitOffset = 8 * rowIndex;
        //                for (var columnIndex = 0U; columnIndex < 4; columnIndex++)
        //                {
        //                    // Color indices are arranged from right to left.
        //                    var bitOffset = rowBaseBitOffset + (bitsPerColorIndex * (3 - columnIndex));
        //                    colorIndices[rowBaseColorIndexIndex + columnIndex] = (uint)Utils.GetBits(bitOffset, bitsPerColorIndex, colorIndexBytes);
        //                }
        //            }
        //            // Calculate pixel colors.
        //            var colors = new Color32[16];
        //            for (var i = 0; i < 16; i++)
        //                colors[i] = colorTable[colorIndices[i]];
        //            return colors;
        //        }

        //        /// <summary>
        //        /// Builds a 4-color color table for a DXT1-compressed 4x4 block of texels and then decodes the texels.
        //        /// </summary>
        //        /// <remarks>See https://msdn.microsoft.com/en-us/library/windows/desktop/bb694531(v=vs.85).aspx#BC1 </remarks>
        //        static Color32[] DecodeDXT1TexelBlock(BinaryReader r, bool containsAlpha)
        //        {
        //            // Create the color table.
        //            var colorTable = new Color[4];
        //            colorTable[0] = r.ReadUInt16().B565ToColor();
        //            colorTable[1] = r.ReadUInt16().B565ToColor();
        //            if (!containsAlpha)
        //            {
        //                colorTable[2] = Color.Lerp(colorTable[0], colorTable[1], 1.0f / 3);
        //                colorTable[3] = Color.Lerp(colorTable[0], colorTable[1], 2.0f / 3);
        //            }
        //            else
        //            {
        //                colorTable[2] = Color.Lerp(colorTable[0], colorTable[1], 1.0f / 2);
        //                colorTable[3] = new Color(0, 0, 0, 0);
        //            }
        //            // Calculate pixel colors.
        //            return DecodeDXT1TexelBlock(r, colorTable);
        //        }

        //        /// <summary>
        //        /// Decodes a DXT3-compressed 4x4 block of texels.
        //        /// </summary>
        //        /// <remarks>See https://msdn.microsoft.com/en-us/library/windows/desktop/bb694531(v=vs.85).aspx#BC2 </remarks>
        //        static Color32[] DecodeDXT3TexelBlock(BinaryReader r)
        //        {
        //            // Read compressed pixel alphas.
        //            var compressedAlphas = new byte[16];
        //            for (var rowIndex = 0; rowIndex < 4; rowIndex++)
        //            {
        //                var compressedAlphaRow = r.ReadUInt16();
        //                for (var columnIndex = 0; columnIndex < 4; columnIndex++)
        //                    // Each compressed alpha is 4 bits.
        //                    compressedAlphas[(4 * rowIndex) + columnIndex] = (byte)((compressedAlphaRow >> (columnIndex * 4)) & 0xF);
        //            }
        //            // Calculate pixel alphas.
        //            var alphas = new byte[16];
        //            for (var i = 0; i < 16; i++)
        //            {
        //                var alphaPercent = (float)compressedAlphas[i] / 15;
        //                alphas[i] = (byte)Math.Round(alphaPercent * 255);
        //            }
        //            // Create the color table.
        //            var colorTable = new Color[4];
        //            colorTable[0] = r.ReadUInt16().B565ToColor();
        //            colorTable[1] = r.ReadUInt16().B565ToColor();
        //            colorTable[2] = Color.Lerp(colorTable[0], colorTable[1], 1.0f / 3);
        //            colorTable[3] = Color.Lerp(colorTable[0], colorTable[1], 2.0f / 3);
        //            // Calculate pixel colors.
        //            var colors = DecodeDXT1TexelBlock(r, colorTable);
        //            for (var i = 0; i < 16; i++)
        //                colors[i].a = alphas[i];
        //            return colors;
        //        }

        //        /// <summary>
        //        /// Decodes a DXT5-compressed 4x4 block of texels.
        //        /// </summary>
        //        /// <remarks>See https://msdn.microsoft.com/en-us/library/windows/desktop/bb694531(v=vs.85).aspx#BC3 </remarks>
        //        static Color32[] DecodeDXT5TexelBlock(BinaryReader r)
        //        {
        //            // Create the alpha table.
        //            var alphaTable = new float[8];
        //            alphaTable[0] = r.ReadByte();
        //            alphaTable[1] = r.ReadByte();
        //            if (alphaTable[0] > alphaTable[1])
        //            {
        //                for (var i = 0; i < 6; i++)
        //                    alphaTable[2 + i] = Mathf.Lerp(alphaTable[0], alphaTable[1], (float)(1 + i) / 7);
        //            }
        //            else
        //            {
        //                for (var i = 0; i < 4; i++)
        //                    alphaTable[2 + i] = Mathf.Lerp(alphaTable[0], alphaTable[1], (float)(1 + i) / 5);
        //                alphaTable[6] = 0;
        //                alphaTable[7] = 255;
        //            }

        //            // Read pixel alpha indices.
        //            var alphaIndices = new uint[16];
        //            var alphaIndexBytesRow0 = new byte[3];
        //            r.Read(alphaIndexBytesRow0, 0, alphaIndexBytesRow0.Length); Array.Reverse(alphaIndexBytesRow0); // Take care of little-endianness.
        //            var alphaIndexBytesRow1 = new byte[3];
        //            r.Read(alphaIndexBytesRow1, 0, alphaIndexBytesRow1.Length); Array.Reverse(alphaIndexBytesRow1); // Take care of little-endianness.
        //            const uint bitsPerAlphaIndex = 3U;
        //            alphaIndices[0] = (uint)Utils.GetBits(21, bitsPerAlphaIndex, alphaIndexBytesRow0);
        //            alphaIndices[1] = (uint)Utils.GetBits(18, bitsPerAlphaIndex, alphaIndexBytesRow0);
        //            alphaIndices[2] = (uint)Utils.GetBits(15, bitsPerAlphaIndex, alphaIndexBytesRow0);
        //            alphaIndices[3] = (uint)Utils.GetBits(12, bitsPerAlphaIndex, alphaIndexBytesRow0);
        //            alphaIndices[4] = (uint)Utils.GetBits(9, bitsPerAlphaIndex, alphaIndexBytesRow0);
        //            alphaIndices[5] = (uint)Utils.GetBits(6, bitsPerAlphaIndex, alphaIndexBytesRow0);
        //            alphaIndices[6] = (uint)Utils.GetBits(3, bitsPerAlphaIndex, alphaIndexBytesRow0);
        //            alphaIndices[7] = (uint)Utils.GetBits(0, bitsPerAlphaIndex, alphaIndexBytesRow0);
        //            alphaIndices[8] = (uint)Utils.GetBits(21, bitsPerAlphaIndex, alphaIndexBytesRow1);
        //            alphaIndices[9] = (uint)Utils.GetBits(18, bitsPerAlphaIndex, alphaIndexBytesRow1);
        //            alphaIndices[10] = (uint)Utils.GetBits(15, bitsPerAlphaIndex, alphaIndexBytesRow1);
        //            alphaIndices[11] = (uint)Utils.GetBits(12, bitsPerAlphaIndex, alphaIndexBytesRow1);
        //            alphaIndices[12] = (uint)Utils.GetBits(9, bitsPerAlphaIndex, alphaIndexBytesRow1);
        //            alphaIndices[13] = (uint)Utils.GetBits(6, bitsPerAlphaIndex, alphaIndexBytesRow1);
        //            alphaIndices[14] = (uint)Utils.GetBits(3, bitsPerAlphaIndex, alphaIndexBytesRow1);
        //            alphaIndices[15] = (uint)Utils.GetBits(0, bitsPerAlphaIndex, alphaIndexBytesRow1);
        //            // Create the color table.
        //            var colorTable = new Color[4];
        //            colorTable[0] = r.ReadUInt16().B565ToColor();
        //            colorTable[1] = r.ReadUInt16().B565ToColor();
        //            colorTable[2] = Color.Lerp(colorTable[0], colorTable[1], 1.0f / 3);
        //            colorTable[3] = Color.Lerp(colorTable[0], colorTable[1], 2.0f / 3);
        //            // Calculate pixel colors.
        //            var colors = DecodeDXT1TexelBlock(r, colorTable);
        //            for (var i = 0; i < 16; i++)
        //                colors[i].a = (byte)Math.Round(alphaTable[alphaIndices[i]]);
        //            return colors;
        //        }

        //        /// <summary>
        //        /// Copies a decoded texel block to a texture's data buffer. Takes into account DDS mipmap padding.
        //        /// </summary>
        //        /// <param name="decodedTexels">The decoded DDS texels.</param>
        //        /// <param name="argb">The texture's data buffer.</param>
        //        /// <param name="baseARGBIndex">The desired offset into the texture's data buffer. Used for mipmaps.</param>
        //        /// <param name="baseRowIndex">The base row index in the texture where decoded texels are copied.</param>
        //        /// <param name="baseColumnIndex">The base column index in the texture where decoded texels are copied.</param>
        //        /// <param name="textureWidth">The width of the texture.</param>
        //        /// <param name="textureHeight">The height of the texture.</param>
        //        static void CopyDecodedTexelBlock(Color32[] decodedTexels, byte[] argb, int baseARGBIndex, int baseRowIndex, int baseColumnIndex, int textureWidth, int textureHeight)
        //        {
        //            for (var i = 0; i < 4; i++) // row
        //                for (var j = 0; j < 4; j++) // column
        //                {
        //                    var rowIndex = baseRowIndex + i;
        //                    var columnIndex = baseColumnIndex + j;
        //                    // Don't copy padding on mipmaps.
        //                    if (rowIndex < textureHeight && columnIndex < textureWidth)
        //                    {
        //                        var decodedTexelIndex = (4 * i) + j;
        //                        var color = decodedTexels[decodedTexelIndex];
        //                        var ARGBPixelOffset = (textureWidth * rowIndex) + columnIndex;
        //                        var basePixelARGBIndex = baseARGBIndex + (4 * ARGBPixelOffset);
        //                        argb[basePixelARGBIndex] = color.a;
        //                        argb[basePixelARGBIndex + 1] = color.r;
        //                        argb[basePixelARGBIndex + 2] = color.g;
        //                        argb[basePixelARGBIndex + 3] = color.b;
        //                    }
        //                }
        //        }

        //        /// <summary>
        //        /// Decodes DXT data to ARGB.
        //        /// </summary>
        //        static byte[] DecodeDXTToARGB(int DXTVersion, byte[] compressedData, uint width, uint height, DDS_PIXELFORMAT  pixelFormat, uint mipmapCount)
        //        {
        //            var alphaFlag = Utils.ContainsBitFlags((int)pixelFormat.dwFlags, (int)DDS_PIXELFORMATS.AlphaPixels);
        //            var containsAlpha = alphaFlag || (pixelFormat.dwRGBBitCount == 32 && pixelFormat.dwABitMask != 0);
        //            using (var r = new BinaryReader(new MemoryStream(compressedData)))
        //            {
        //                var argb = new byte[TextureInfo.GetMipMappedTextureDataSize((int)width, (int)height, 4)];
        //                var mipMapWidth = (int)width;
        //                var mipMapHeight = (int)height;
        //                var baseARGBIndex = 0;
        //                for (var mipMapIndex = 0; mipMapIndex < mipmapCount; mipMapIndex++)
        //                {
        //                    for (var rowIndex = 0; rowIndex < mipMapHeight; rowIndex += 4)
        //                        for (var columnIndex = 0; columnIndex < mipMapWidth; columnIndex += 4)
        //                        {
        //                            if (r.Position() == r.BaseStream.Length)
        //                                return argb;
        //                            Color32[] colors = null;
        //                            switch (DXTVersion) // Doing a switch instead of using a delegate for speed.
        //                            {
        //                                case 1: colors = DecodeDXT1TexelBlock(r, containsAlpha); break;
        //                                case 3: colors = DecodeDXT3TexelBlock(r); break;
        //                                case 5: colors = DecodeDXT5TexelBlock(r); break;
        //                                default: throw new NotImplementedException($"Tried decoding a DDS file using an unsupported DXT format: DXT {DXTVersion}");
        //                            }
        //                            CopyDecodedTexelBlock(colors, argb, baseARGBIndex, rowIndex, columnIndex, mipMapWidth, mipMapHeight);
        //                        }
        //                    baseARGBIndex += mipMapWidth * mipMapHeight * 4;
        //                    mipMapWidth /= 2;
        //                    mipMapHeight /= 2;
        //                }
        //                return argb;
        //            }
        //        }

        //        static byte[] DecodeDXT1ToARGB(byte[] compressedData, uint width, uint height, DDS_PIXELFORMAT  pixelFormat, uint mipmapCount) => DecodeDXTToARGB(1, compressedData, width, height, pixelFormat, mipmapCount);
        //        static byte[] DecodeDXT3ToARGB(byte[] compressedData, uint width, uint height, DDS_PIXELFORMAT  pixelFormat, uint mipmapCount) => DecodeDXTToARGB(3, compressedData, width, height, pixelFormat, mipmapCount);
        //        static byte[] DecodeDXT5ToARGB(byte[] compressedData, uint width, uint height, DDS_PIXELFORMAT  pixelFormat, uint mipmapCount) => DecodeDXTToARGB(5, compressedData, width, height, pixelFormat, mipmapCount);



        //        /// <summary>
        //        /// Generates missing mipmap levels for a DDS texture and optionally flips it.
        //        /// </summary>
        //        /// <param name="width">The width of the texture.</param>
        //        /// <param name="height">The height of the texture.</param>
        //        /// <param name="bytesPerPixel">The number of bytes per pixel.</param>
        //        /// <param name="hasMipmaps">Does the DDS texture have mipmaps?</param>
        //        /// <param name="DDSMipmapLevelCount">The number of mipmap levels in the DDS file. 1 if the DDS file doesn't have mipmaps.</param>
        //        /// <param name="data">The texture's data.</param>
        //        /// <param name="flipVertically">Should the texture be flipped vertically?</param>
        //        static void PostProcessDDSTexture(int width, int height, int bytesPerPixel, bool hasMipmaps, int DDSMipmapLevelCount, byte[] data, bool flipVertically)
        //        {
        //            Assert(width > 0 && height > 0 && bytesPerPixel > 0 && DDSMipmapLevelCount > 0 && data != null);
        //            // Flip mip-maps if necessary and generate missing mip-map levels.
        //            var mipMapLevelWidth = width;
        //            var mipMapLevelHeight = height;
        //            var mipMapLevelIndex = 0;
        //            var mipMapLevelDataOffset = 0;
        //            // While we haven't processed all of the mipmap levels we should process.
        //            while (mipMapLevelWidth > 1 || mipMapLevelHeight > 1)
        //            {
        //                var mipMapDataSize = mipMapLevelWidth * mipMapLevelHeight * bytesPerPixel;
        //                // If the DDS file contains the current mipmap level, flip it vertically if necessary.
        //                if (flipVertically && mipMapLevelIndex < DDSMipmapLevelCount)
        //                    data.Flip2DSubArrayVertically(mipMapLevelDataOffset, mipMapLevelHeight, mipMapLevelWidth * bytesPerPixel);
        //                // Break after optionally flipping the first mipmap level if the DDS texture doesn't have mipmaps.
        //                if (!hasMipmaps)
        //                    break;
        //                // Generate the next mipmap level's data if the DDS file doesn't contain it.
        //                if (mipMapLevelIndex + 1 >= DDSMipmapLevelCount)
        //                    TextureInfo.Downscale4Component32BitPixelsX2(data, mipMapLevelDataOffset, mipMapLevelHeight, mipMapLevelWidth, data, mipMapLevelDataOffset + mipMapDataSize);
        //                // Switch to the next mipmap level.
        //                mipMapLevelIndex++;
        //                mipMapLevelWidth = mipMapLevelWidth > 1 ? (mipMapLevelWidth / 2) : mipMapLevelWidth;
        //                mipMapLevelHeight = mipMapLevelHeight > 1 ? (mipMapLevelHeight / 2) : mipMapLevelHeight;
        //                mipMapLevelDataOffset += mipMapDataSize;
        //            }
        //        }
        //    }
    }
}