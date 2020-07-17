using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using GameEstate.Core;
using K4os.Compression.LZ4;
using SkiaSharp;

namespace GameEstate.Formats.Valve.Blocks
{
    public class DATATexture : DATA
    {
        //const short MipmapLevelToExtract = 0; // for debugging purposes

        //public class SpritesheetData
        //{
        //    public class Sequence
        //    {
        //        public class Frame
        //        {
        //            public Vector2 StartMins { get; set; }
        //            public Vector2 StartMaxs { get; set; }

        //            public Vector2 EndMins { get; set; }
        //            public Vector2 EndMaxs { get; set; }
        //        }

        //        public Frame[] Frames { get; set; }

        //        public float FramesPerSecond { get; set; }
        //    }

        //    public Sequence[] Sequences { get; set; }
        //}

        //BinaryReader Reader;
        //long DataOffset;
        //BinaryPak Resource;

        //public ushort Version { get; private set; }

        //public ushort Width { get; private set; }

        //public ushort Height { get; private set; }

        //public ushort Depth { get; private set; }

        //public float[] Reflectivity { get; private set; }

        //public VTexFlags Flags { get; private set; }

        //public VTexFormat Format { get; private set; }

        //public byte NumMipLevels { get; private set; }

        //public uint Picmip0Res { get; private set; }

        //public Dictionary<VTexExtraData, byte[]> ExtraData { get; private set; } = new Dictionary<VTexExtraData, byte[]>();

        //public ushort NonPow2Width { get; private set; }

        //public ushort NonPow2Height { get; private set; }

        //int[] CompressedMips;
        //bool IsActuallyCompressedMips;


        //public override void Read(BinaryReader r, BinaryPak resource)
        //{
        //    Reader = r;
        //    Resource = resource;
        //    r.BaseStream.Position = Offset;
        //    Version = r.ReadUInt16();
        //    if (Version != 1)
        //        throw new InvalidDataException(string.Format("Unknown vtex version. ({0} != expected 1)", Version));
        //    Flags = (VTexFlags)r.ReadUInt16();
        //    Reflectivity = new[]
        //    {
        //        r.ReadSingle(),
        //        r.ReadSingle(),
        //        r.ReadSingle(),
        //        r.ReadSingle(),
        //    };
        //    Width = r.ReadUInt16();
        //    Height = r.ReadUInt16();
        //    Depth = r.ReadUInt16();
        //    NonPow2Width = 0;
        //    NonPow2Height = 0;
        //    Format = (VTexFormat)r.ReadByte();
        //    NumMipLevels = r.ReadByte();
        //    Picmip0Res = r.ReadUInt32();
        //    var extraDataOffset = r.ReadUInt32();
        //    var extraDataCount = r.ReadUInt32();
        //    if (extraDataCount > 0)
        //    {
        //        r.BaseStream.Position += extraDataOffset - 8; // 8 is 2 uint32s we just read
        //        for (var i = 0; i < extraDataCount; i++)
        //        {
        //            var type = (VTexExtraData)r.ReadUInt32();
        //            var offset = r.ReadUInt32() - 8;
        //            var size = r.ReadUInt32();
        //            var prevOffset = r.BaseStream.Position;
        //            r.BaseStream.Position += offset;
        //            if (type == VTexExtraData.FILL_TO_POWER_OF_TWO)
        //            {
        //                r.ReadUInt16();
        //                var nw = r.ReadUInt16();
        //                var nh = r.ReadUInt16();
        //                if (nw > 0 && nh > 0 && Width >= nw && Height >= nh)
        //                {
        //                    NonPow2Width = nw;
        //                    NonPow2Height = nh;
        //                }
        //                r.BaseStream.Position -= 6;
        //            }
        //            ExtraData.Add(type, r.ReadBytes((int)size));
        //            if (type == VTexExtraData.COMPRESSED_MIP_SIZE)
        //            {
        //                r.BaseStream.Position -= size;
        //                var int1 = r.ReadUInt32(); // 1?
        //                var int2 = r.ReadUInt32(); // 8?
        //                var mips = r.ReadUInt32();
        //                if (int1 != 1 && int1 != 0)
        //                    throw new Exception($"int1 got: {int1}");
        //                if (int2 != 8)
        //                    throw new Exception($"int2 expected 8 but got: {int2}");
        //                IsActuallyCompressedMips = int1 == 1; // TODO: Verify whether this int is the one that actually controls compression
        //                CompressedMips = new int[mips];
        //                for (var mip = 0; mip < mips; mip++)
        //                    CompressedMips[mip] = r.ReadInt32();
        //            }
        //            r.BaseStream.Position = prevOffset;
        //        }
        //    }
        //    DataOffset = Offset + Size;
        //}

        //public SpritesheetData GetSpriteSheetData()
        //{
        //    if (ExtraData.TryGetValue(VTexExtraData.SHEET, out var bytes))
        //    {
        //        using (var memoryStream = new MemoryStream(bytes))
        //        using (var r = new BinaryReader(memoryStream))
        //        {
        //            var version = r.ReadUInt32();
        //            var numSequences = r.ReadUInt32();
        //            var sequences = new SpritesheetData.Sequence[numSequences];
        //            for (var i = 0; i < numSequences; i++)
        //            {
        //                var sequenceNumber = r.ReadUInt32();
        //                var unknown1 = r.ReadUInt32(); // 1?
        //                var unknown2 = r.ReadUInt32();
        //                var numFrames = r.ReadUInt32();
        //                var framesPerSecond = r.ReadSingle(); // Not too sure about this one
        //                var dataOffset = r.BaseStream.Position + r.ReadUInt32();
        //                var unknown4 = r.ReadUInt32(); // 0?
        //                var unknown5 = r.ReadUInt32(); // 0?
        //                var endOfHeaderOffset = r.BaseStream.Position; // Store end of header to return to later
        //                // Seek to start of the sequence data
        //                r.BaseStream.Position = dataOffset;
        //                var sequenceName = r.ReadZUTF8();
        //                var frameUnknown = r.ReadUInt16();
        //                var frames = new SpritesheetData.Sequence.Frame[numFrames];
        //                for (var j = 0; j < numFrames; j++)
        //                {
        //                    var frameUnknown1 = r.ReadSingle();
        //                    var frameUnknown2 = r.ReadUInt32();
        //                    var frameUnknown3 = r.ReadSingle();
        //                    frames[j] = new SpritesheetData.Sequence.Frame();
        //                }
        //                for (var j = 0; j < numFrames; j++)
        //                {
        //                    frames[j].StartMins = new Vector2(r.ReadSingle(), r.ReadSingle());
        //                    frames[j].StartMaxs = new Vector2(r.ReadSingle(), r.ReadSingle());
        //                    frames[j].EndMins = new Vector2(r.ReadSingle(), r.ReadSingle());
        //                    frames[j].EndMaxs = new Vector2(r.ReadSingle(), r.ReadSingle());
        //                }
        //                r.BaseStream.Position = endOfHeaderOffset;
        //                sequences[i] = new SpritesheetData.Sequence
        //                {
        //                    Frames = frames,
        //                    FramesPerSecond = framesPerSecond,
        //                };
        //            }
        //            return new SpritesheetData { Sequences = sequences };
        //        }
        //    }
        //    return null;
        //}

        //public SKBitmap GenerateBitmap()
        //{
        //    Reader.BaseStream.Position = DataOffset;

        //    var width = NonPow2Width > 0 ? NonPow2Width : Width;
        //    var height = NonPow2Height > 0 ? NonPow2Height : Height;

        //    width >>= MipmapLevelToExtract;
        //    height >>= MipmapLevelToExtract;

        //    var imageInfo = new SKImageInfo(width, height, SKColorType.Bgra8888, SKAlphaType.Unpremul);
        //    Span<byte> data = new byte[imageInfo.RowBytes * imageInfo.Height];

        //    SkipMipmaps();

        //    switch (Format)
        //    {
        //        case VTexFormat.DXT1:
        //            TextureDecompressors.UncompressDXT1(imageInfo, GetDecompressedBuffer(), data, Width >> MipmapLevelToExtract, Height >> MipmapLevelToExtract);
        //            break;

        //        case VTexFormat.DXT5:
        //            var yCoCg = false;
        //            var normalize = false;
        //            var invert = false;
        //            var hemiOct = false;

        //            if (Resource.EditInfo.Structs.ContainsKey(ResourceEditInfo.REDIStruct.SpecialDependencies))
        //            {
        //                var specialDeps = (SpecialDependencies)Resource.EditInfo.Structs[ResourceEditInfo.REDIStruct.SpecialDependencies];

        //                yCoCg = specialDeps.List.Any(dependancy => dependancy.CompilerIdentifier == "CompileTexture" && dependancy.String == "Texture Compiler Version Image YCoCg Conversion");
        //                normalize = specialDeps.List.Any(dependancy => dependancy.CompilerIdentifier == "CompileTexture" && dependancy.String == "Texture Compiler Version Image NormalizeNormals");
        //                invert = specialDeps.List.Any(dependancy => dependancy.CompilerIdentifier == "CompileTexture" && dependancy.String == "Texture Compiler Version LegacySource1InvertNormals");
        //                hemiOct = specialDeps.List.Any(dependancy => dependancy.CompilerIdentifier == "CompileTexture" && dependancy.String == "Texture Compiler Version Mip HemiOctAnisoRoughness");
        //            }

        //            TextureDecompressors.UncompressDXT5(imageInfo, GetDecompressedBuffer(), data, Width, Height, yCoCg, normalize, invert, hemiOct);
        //            break;

        //        case VTexFormat.I8:
        //            return TextureDecompressors.ReadI8(GetDecompressedBuffer(), Width, Height);

        //        case VTexFormat.RGBA8888:
        //            return TextureDecompressors.ReadUIntPixels(GetDecompressedBuffer(), Width, Height, SKColorType.Rgba8888);

        //        case VTexFormat.R16:
        //            return TextureDecompressors.ReadR16(GetDecompressedBuffer(), Width, Height);

        //        case VTexFormat.RG1616:
        //            return TextureDecompressors.ReadRG1616(GetDecompressedBuffer(), Width, Height);

        //        case VTexFormat.RGBA16161616:
        //            TextureDecompressors.ReadRGBA16161616(imageInfo, GetDecompressedBuffer(), data);
        //            break;

        //        case VTexFormat.R16F:
        //            return TextureDecompressors.ReadR16F(GetDecompressedBuffer(), Width, Height);

        //        case VTexFormat.RG1616F:
        //            return TextureDecompressors.ReadRG1616F(GetDecompressedBuffer(), Width, Height);

        //        case VTexFormat.RGBA16161616F:
        //            TextureDecompressors.ReadRGBA16161616F(imageInfo, GetDecompressedBuffer(), data);
        //            break;

        //        case VTexFormat.R32F:
        //            return TextureDecompressors.ReadR32F(GetDecompressedBuffer(), Width, Height);

        //        case VTexFormat.RG3232F:
        //            return TextureDecompressors.ReadRG3232F(GetDecompressedBuffer(), Width, Height);

        //        case VTexFormat.RGB323232F:
        //            return TextureDecompressors.ReadRGB323232F(GetDecompressedBuffer(), Width, Height);

        //        case VTexFormat.RGBA32323232F:
        //            return TextureDecompressors.ReadRGBA32323232F(GetDecompressedBuffer(), Width, Height);

        //        case VTexFormat.BC6H:
        //            BPTC.BPTCDecoders.UncompressBC6H(imageInfo, GetDecompressedBuffer(), data, Width, Height);
        //            break;

        //        case VTexFormat.BC7:
        //            bool hemiOctRB = false;
        //            invert = false;
        //            if (Resource.EditInfo.Structs.ContainsKey(ResourceEditInfo.REDIStruct.SpecialDependencies))
        //            {
        //                var specialDeps = (SpecialDependencies)Resource.EditInfo.Structs[ResourceEditInfo.REDIStruct.SpecialDependencies];
        //                hemiOctRB = specialDeps.List.Any(dependancy => dependancy.CompilerIdentifier == "CompileTexture" && dependancy.String == "Texture Compiler Version Mip HemiOctIsoRoughness_RG_B");
        //                invert = specialDeps.List.Any(dependancy => dependancy.CompilerIdentifier == "CompileTexture" && dependancy.String == "Texture Compiler Version LegacySource1InvertNormals");
        //            }

        //            BPTC.BPTCDecoders.UncompressBC7(imageInfo, GetDecompressedBuffer(), data, Width, Height, hemiOctRB, invert);
        //            break;

        //        case VTexFormat.ATI2N:
        //            normalize = false;
        //            if (Resource.EditInfo.Structs.ContainsKey(ResourceEditInfo.REDIStruct.SpecialDependencies))
        //            {
        //                var specialDeps = (SpecialDependencies)Resource.EditInfo.Structs[ResourceEditInfo.REDIStruct.SpecialDependencies];
        //                normalize = specialDeps.List.Any(dependancy => dependancy.CompilerIdentifier == "CompileTexture" && dependancy.String == "Texture Compiler Version Image NormalizeNormals");
        //            }

        //            TextureDecompressors.UncompressATI2N(GetDecompressedBuffer(), data, Width, Height, normalize);
        //            break;

        //        case VTexFormat.IA88:
        //            return TextureDecompressors.ReadIA88(GetDecompressedBuffer(), Width, Height);

        //        case VTexFormat.ATI1N:
        //            TextureDecompressors.UncompressATI1N(GetDecompressedBuffer(), data, Width, Height);
        //            break;

        //        // TODO: Are we sure DXT5 and RGBA8888 are just raw buffers?
        //        case VTexFormat.JPEG_DXT5:
        //        case VTexFormat.JPEG_RGBA8888:
        //        case VTexFormat.PNG_DXT5:
        //        case VTexFormat.PNG_RGBA8888:
        //            return ReadBuffer();

        //        case VTexFormat.ETC2:
        //            var etc = new Etc.EtcDecoder();
        //            var rewriteMeProperlyPlease = new byte[data.Length]; // TODO
        //            etc.DecompressETC2(GetDecompressedTextureAtMipLevel(0), width, height, rewriteMeProperlyPlease);
        //            data = rewriteMeProperlyPlease;
        //            break;

        //        case VTexFormat.ETC2_EAC:
        //            var etc2 = new Etc.EtcDecoder();
        //            var rewriteMeProperlyPlease2 = new byte[data.Length]; // TODO
        //            etc2.DecompressETC2A8(GetDecompressedTextureAtMipLevel(0), width, height, rewriteMeProperlyPlease2);
        //            data = rewriteMeProperlyPlease2;
        //            break;

        //        case VTexFormat.BGRA8888:
        //            return TextureDecompressors.ReadUIntPixels(GetDecompressedBuffer(), Width, Height, SKColorType.Bgra8888);

        //        default:
        //            throw new NotImplementedException(string.Format("Unhandled image type: {0}", Format));
        //    }

        //    // pin the managed array so that the GC doesn't move it
        //    // TODO: There's probably a better way of handling this with Span<byte>
        //    var gcHandle = GCHandle.Alloc(data.ToArray(), GCHandleType.Pinned);

        //    // install the pixels with the color type of the pixel data
        //    var bitmap = new SKBitmap();
        //    bitmap.InstallPixels(imageInfo, gcHandle.AddrOfPinnedObject(), imageInfo.RowBytes, (address, context) => { gcHandle.Free(); }, null);

        //    return bitmap;
        //}

        //private int CalculateBufferSizeForMipLevel(int mipLevel)
        //{
        //    var bytesPerPixel = GetBlockSize();
        //    var width = Width >> mipLevel;
        //    var height = Height >> mipLevel;
        //    var depth = Depth >> mipLevel;

        //    if (depth < 1)
        //    {
        //        depth = 1;
        //    }

        //    if (Format == VTexFormat.DXT1
        //    || Format == VTexFormat.DXT5
        //    || Format == VTexFormat.BC6H
        //    || Format == VTexFormat.BC7
        //    || Format == VTexFormat.ETC2
        //    || Format == VTexFormat.ETC2_EAC
        //    || Format == VTexFormat.ATI1N)
        //    {
        //        var misalign = width % 4;

        //        if (misalign > 0)
        //        {
        //            width += 4 - misalign;
        //        }

        //        misalign = height % 4;

        //        if (misalign > 0)
        //        {
        //            height += 4 - misalign;
        //        }

        //        if (width < 4 && width > 0)
        //        {
        //            width = 4;
        //        }

        //        if (height < 4 && height > 0)
        //        {
        //            height = 4;
        //        }

        //        if (depth < 4 && depth > 1)
        //        {
        //            depth = 4;
        //        }

        //        var numBlocks = (width * height) >> 4;
        //        numBlocks *= depth;

        //        return numBlocks * bytesPerPixel;
        //    }

        //    return width * height * depth * bytesPerPixel;
        //}

        //private void SkipMipmaps()
        //{
        //    if (NumMipLevels < 2)
        //    {
        //        return;
        //    }

        //    for (var j = NumMipLevels - 1; j > MipmapLevelToExtract; j--)
        //    {
        //        int offset;

        //        if (CompressedMips != null)
        //        {
        //            offset = CompressedMips[j];
        //        }
        //        else
        //        {
        //            offset = CalculateBufferSizeForMipLevel(j) * (Flags.HasFlag(VTexFlags.CUBE_TEXTURE) ? 6 : 1);
        //        }

        //        Reader.BaseStream.Position += offset;
        //    }
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

        //private BinaryReader GetDecompressedBuffer()
        //{
        //    if (!IsActuallyCompressedMips)
        //    {
        //        return Reader;
        //    }

        //    var outStream = new MemoryStream(GetDecompressedTextureAtMipLevel(MipmapLevelToExtract), false);

        //    return new BinaryReader(outStream); // TODO: dispose
        //}

        //private SKBitmap ReadBuffer()
        //{
        //    return SKBitmap.Decode(Reader.ReadBytes((int)Reader.BaseStream.Length));
        //}

        //public int GetBlockSize()
        //{
        //    switch (Format)
        //    {
        //        case VTexFormat.DXT1: return 8;
        //        case VTexFormat.DXT5: return 16;
        //        case VTexFormat.RGBA8888: return 4;
        //        case VTexFormat.R16: return 2;
        //        case VTexFormat.RG1616: return 4;
        //        case VTexFormat.RGBA16161616: return 8;
        //        case VTexFormat.R16F: return 2;
        //        case VTexFormat.RG1616F: return 4;
        //        case VTexFormat.RGBA16161616F: return 8;
        //        case VTexFormat.R32F: return 4;
        //        case VTexFormat.RG3232F: return 8;
        //        case VTexFormat.RGB323232F: return 12;
        //        case VTexFormat.RGBA32323232F: return 16;
        //        case VTexFormat.BC6H: return 16;
        //        case VTexFormat.BC7: return 16;
        //        case VTexFormat.IA88: return 2;
        //        case VTexFormat.ETC2: return 8;
        //        case VTexFormat.ETC2_EAC: return 16;
        //        case VTexFormat.BGRA8888: return 4;
        //        case VTexFormat.ATI1N: return 8;
        //    }

        //    return 1;
        //}

        //public override string ToString()
        //{
        //    using (var writer = new IndentedTextWriter())
        //    {
        //        writer.WriteLine("{0,-12} = {1}", "VTEX Version", Version);
        //        writer.WriteLine("{0,-12} = {1}", "Width", Width);
        //        writer.WriteLine("{0,-12} = {1}", "Height", Height);
        //        writer.WriteLine("{0,-12} = {1}", "Depth", Depth);
        //        writer.WriteLine("{0,-12} = {1}", "NonPow2W", NonPow2Width);
        //        writer.WriteLine("{0,-12} = {1}", "NonPow2H", NonPow2Height);
        //        writer.WriteLine("{0,-12} = ( {1:F6}, {2:F6}, {3:F6}, {4:F6} )", "Reflectivity", Reflectivity[0], Reflectivity[1], Reflectivity[2], Reflectivity[3]);
        //        writer.WriteLine("{0,-12} = {1}", "NumMipLevels", NumMipLevels);
        //        writer.WriteLine("{0,-12} = {1}", "Picmip0Res", Picmip0Res);
        //        writer.WriteLine("{0,-12} = {1} (VTEX_FORMAT_{2})", "Format", (int)Format, Format);
        //        writer.WriteLine("{0,-12} = 0x{1:X8}", "Flags", (int)Flags);

        //        foreach (Enum value in Enum.GetValues(Flags.GetType()))
        //        {
        //            if (Flags.HasFlag(value))
        //            {
        //                writer.WriteLine("{0,-12} | 0x{1:X8} = VTEX_FLAG_{2}", string.Empty, Convert.ToInt32(value), value);
        //            }
        //        }

        //        writer.WriteLine("{0,-12} = {1} entries:", "Extra Data", ExtraData.Count);

        //        var entry = 0;

        //        foreach (var b in ExtraData)
        //        {
        //            writer.WriteLine("{0,-12}   [ Entry {1}: VTEX_EXTRA_DATA_{2} - {3} bytes ]", string.Empty, entry++, b.Key, b.Value.Length);

        //            if (b.Key == VTexExtraData.COMPRESSED_MIP_SIZE)
        //            {
        //                writer.WriteLine("{0,-16}   [ {1} mips, sized: {2} ]", string.Empty, CompressedMips.Length, string.Join(", ", CompressedMips));
        //            }
        //        }

        //        for (var j = 0; j < NumMipLevels; j++)
        //        {
        //            writer.WriteLine($"Mip level {j} - buffer size: {CalculateBufferSizeForMipLevel(j)}");
        //        }

        //        return writer.ToString();
        //    }
        //}
    }
}
