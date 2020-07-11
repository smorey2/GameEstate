using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GameEstate.Graphics.Texture
{
    public static partial class TextureConverter
    {
        const short MipmapLevelToExtract = 0; // for debugging purposes

        public static SKBitmap GetBitmap(TextureInfo source)
        {
            var width = source.Width; var height = source.Height;

            var imageWidth = source.TryGet<int>("NonPow2Width", out var z) && z > 0 ? z : width;
            var imageHeight = source.TryGet<int>("NonPow2Height", out z) && z > 0 ? z : height;

            imageWidth >>= MipmapLevelToExtract;
            imageHeight >>= MipmapLevelToExtract;

            var imageInfo = new SKImageInfo(imageWidth, imageHeight, SKColorType.Bgra8888, SKAlphaType.Unpremul);
            var data = new byte[imageInfo.RowBytes * imageInfo.Height];

            var offset = source.GetDataOffsetForMip(MipmapLevelToExtract);
            switch (source.GLFormat)
            {
                case TextureGLFormat.DXT1: DXT1(imageInfo, source.GetDecompressedBuffer(offset), data, width >> MipmapLevelToExtract, height >> MipmapLevelToExtract); break;
                case TextureGLFormat.DXT5:
                    var yCoCg = false;
                    var normalize = false;
                    var invert = false;
                    var hemiOct = false;
                    //if (Resource.EditInfo.Structs.ContainsKey(ResourceEditInfo.REDIStruct.SpecialDependencies))
                    //{
                    //    var specialDeps = (SpecialDependencies)Resource.EditInfo.Structs[ResourceEditInfo.REDIStruct.SpecialDependencies];

                    //    yCoCg = specialDeps.List.Any(dependancy => dependancy.CompilerIdentifier == "CompileTexture" && dependancy.String == "Texture Compiler Version Image YCoCg Conversion");
                    //    normalize = specialDeps.List.Any(dependancy => dependancy.CompilerIdentifier == "CompileTexture" && dependancy.String == "Texture Compiler Version Image NormalizeNormals");
                    //    invert = specialDeps.List.Any(dependancy => dependancy.CompilerIdentifier == "CompileTexture" && dependancy.String == "Texture Compiler Version LegacySource1InvertNormals");
                    //    hemiOct = specialDeps.List.Any(dependancy => dependancy.CompilerIdentifier == "CompileTexture" && dependancy.String == "Texture Compiler Version Mip HemiOctAnisoRoughness");
                    //}
                    DXT5(imageInfo, source.GetDecompressedBuffer(offset), data, width, height, yCoCg, normalize, invert, hemiOct);
                    break;
                case TextureGLFormat.I8: return I8(source.GetDecompressedBuffer(offset), width, height);
                case TextureGLFormat.RGBA8888: return U32(source.GetDecompressedBuffer(offset), width, height, SKColorType.Rgba8888);
                case TextureGLFormat.R16: return R16(source.GetDecompressedBuffer(offset), width, height);
                case TextureGLFormat.RG1616: return RG1616(source.GetDecompressedBuffer(offset), width, height);
                case TextureGLFormat.RGBA16161616: RGBA16161616(imageInfo, source.GetDecompressedBuffer(offset), data); break;
                case TextureGLFormat.R16F: return R16F(source.GetDecompressedBuffer(offset), width, height);
                case TextureGLFormat.RG1616F: return RG1616F(source.GetDecompressedBuffer(offset), width, height);
                case TextureGLFormat.RGBA16161616F: RGBA16161616F(imageInfo, source.GetDecompressedBuffer(offset), data); break;
                case TextureGLFormat.R32F: return R32F(source.GetDecompressedBuffer(offset), width, height);
                case TextureGLFormat.RG3232F: return RG3232F(source.GetDecompressedBuffer(offset), width, height);
                case TextureGLFormat.RGB323232F: return RGB323232F(source.GetDecompressedBuffer(offset), width, height);
                case TextureGLFormat.RGBA32323232F: return RGBA32323232F(source.GetDecompressedBuffer(offset), width, height);
                case TextureGLFormat.BC6H: BptcDecoders.UncompressBC6H(imageInfo, source.GetDecompressedBuffer(offset), data, width, height); break;
                case TextureGLFormat.BC7:
                    bool hemiOctRB = false;
                    invert = false;
                    //if (Resource.EditInfo.Structs.ContainsKey(ResourceEditInfo.REDIStruct.SpecialDependencies))
                    //{
                    //    var specialDeps = (SpecialDependencies)Resource.EditInfo.Structs[ResourceEditInfo.REDIStruct.SpecialDependencies];
                    //    hemiOctRB = specialDeps.List.Any(dependancy => dependancy.CompilerIdentifier == "CompileTexture" && dependancy.String == "Texture Compiler Version Mip HemiOctIsoRoughness_RG_B");
                    //    invert = specialDeps.List.Any(dependancy => dependancy.CompilerIdentifier == "CompileTexture" && dependancy.String == "Texture Compiler Version LegacySource1InvertNormals");
                    //}
                    BptcDecoders.UncompressBC7(imageInfo, source.GetDecompressedBuffer(offset), data, width, height, hemiOctRB, invert);
                    break;
                case TextureGLFormat.ATI2N:
                    normalize = false;
                    //if (Resource.EditInfo.Structs.ContainsKey(ResourceEditInfo.REDIStruct.SpecialDependencies))
                    //{
                    //    var specialDeps = (SpecialDependencies)Resource.EditInfo.Structs[ResourceEditInfo.REDIStruct.SpecialDependencies];
                    //    normalize = specialDeps.List.Any(dependancy => dependancy.CompilerIdentifier == "CompileTexture" && dependancy.String == "Texture Compiler Version Image NormalizeNormals");
                    //}
                    ATI2N(source.GetDecompressedBuffer(offset), data, width, height, normalize);
                    break;
                case TextureGLFormat.IA88: return IA88(source.GetDecompressedBuffer(offset), width, height);
                case TextureGLFormat.ATI1N: ATI1N(source.GetDecompressedBuffer(offset), data, width, height); break;
                // TODO: Are we sure DXT5 and RGBA8888 are just raw buffers?
                case TextureGLFormat.JPEG_DXT5:
                case TextureGLFormat.JPEG_RGBA8888:
                case TextureGLFormat.PNG_DXT5:
                case TextureGLFormat.PNG_RGBA8888:
                    data = source.GetTexture(offset);
                    break;
                case TextureGLFormat.ETC2:
                    var etc = new EtcDecoder();
                    var rewriteMeProperlyPlease = new byte[data.Length]; // TODO
                    etc.DecompressETC2(source.GetDecompressedTextureAtMipLevel(offset, 0), imageWidth, imageHeight, rewriteMeProperlyPlease);
                    data = rewriteMeProperlyPlease;
                    break;
                case TextureGLFormat.ETC2_EAC:
                    var etc2 = new EtcDecoder();
                    var rewriteMeProperlyPlease2 = new byte[data.Length]; // TODO
                    etc2.DecompressETC2A8(source.GetDecompressedTextureAtMipLevel(offset, 0), imageWidth, imageHeight, rewriteMeProperlyPlease2);
                    data = rewriteMeProperlyPlease2;
                    break;
                case TextureGLFormat.BGRA8888: return U32(source.GetDecompressedBuffer(offset), width, height, SKColorType.Bgra8888);
                default: throw new ArgumentOutOfRangeException(nameof(source.GLFormat), $"Unhandled image type: {source.GLFormat}");
            }

            // pin the managed array so that the GC doesn't move it
            // TODO: There's probably a better way of handling this with Span<byte>
            var gcHandle = GCHandle.Alloc(data, GCHandleType.Pinned);

            // install the pixels with the color type of the pixel data
            var bitmap = new SKBitmap();
            bitmap.InstallPixels(imageInfo, gcHandle.AddrOfPinnedObject(), imageInfo.RowBytes, (address, context) => { gcHandle.Free(); }, null);

            return bitmap;
        }

        static void _8BitBlock(int bx, int w, int offset, ulong block, Span<byte> pixels, int stride)
        {
            var e0 = (byte)(block & 0xFF);
            var e1 = (byte)(block >> 8 & 0xFF);
            var code = block >> 16;

            for (var y = 0; y < 4; y++)
                for (var x = 0; x < 4; x++)
                {
                    var dataIndex = offset + (y * stride) + (x * 4);

                    var index = (byte)(code & 0x07);
                    code >>= 3;

                    if (bx + x > w || pixels.Length <= dataIndex)
                        continue;

                    if (index == 0)
                        pixels[dataIndex] = e0;
                    else if (index == 1)
                        pixels[dataIndex] = e1;
                    else
                    {
                        if (e0 > e1)
                            pixels[dataIndex] = (byte)((((8 - index) * e0) + ((index - 1) * e1)) / 7);
                        else
                        {
                            if (index == 6) pixels[dataIndex] = 0;
                            else if (index == 7) pixels[dataIndex] = 255;
                            else pixels[dataIndex] = (byte)((((6 - index) * e0) + ((index - 1) * e1)) / 5);
                        }
                    }
                }
        }

        static byte ClampColor(int a) => a > 255 ? (byte)255 : a < 0 ? (byte)0 : (byte)a;

        static void ConvertRgb565ToRgb888(ushort color, out byte r, out byte g, out byte b)
        {
            var temp = ((color >> 11) * 255) + 16;
            r = (byte)(((temp / 32) + temp) / 32);
            temp = (((color & 0x07E0) >> 5) * 255) + 32;
            g = (byte)(((temp / 64) + temp) / 64);
            temp = ((color & 0x001F) * 255) + 16;
            b = (byte)(((temp / 32) + temp) / 32);
        }
    }
}
