using GameEstate.Core;
using ICSharpCode.SharpZipLib.Lzw;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    public class DatFormatTes : DatFormat
    {
        const uint SSE_BSAHEADER_VERSION = 0x69; // Version number of a Skyrim SE BSA

        public override Task<byte[]> ReadAsync(CorePakFile source, BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception = null)
        {
            var fileSize = (int)file.FileSize;
            byte[] fileData;
            int newFileSize;
            r.Position(file.Position);
            if (source.HasNamePrefix)
            {
                var len = r.ReadByte();
                fileSize -= len + 1;
                r.Position(file.Position + 1 + len);
            }
            fileData = r.ReadBytes(fileSize);
            newFileSize = source.Version == SSE_BSAHEADER_VERSION && file.Compressed ? r.ReadInt32() - 4 : fileSize;
            // BSA
            if (file.Compressed)
            {
                var newFileData = new byte[newFileSize];
                if (source.Version != SSE_BSAHEADER_VERSION)
                {
                    if (fileData.Length > 4)
                        using (var s = new MemoryStream(fileData, 4, fileSize - 4))
                        using (var gs = new InflaterInputStream(s))
                            gs.Read(newFileData, 0, newFileData.Length);
                    else newFileData = fileData;
                }
                else
                {
                    using (var s = new MemoryStream(fileData))
                    using (var gs = new Lzw​Input​Stream(s))
                        gs.Read(newFileData, 0, newFileData.Length);
                }
                fileData = newFileData;
            }
            // General BA2
            else if (file.PackedSize > 0 && file.Tag == null)
            {
                var newFileData = new byte[file.FileSize];
                using (var s = new MemoryStream(fileData))
                using (var gs = new InflaterInputStream(s))
                    gs.Read(newFileData, 0, newFileData.Length);
                fileData = newFileData;
            }
            // Fill DDS Header
            else if (file.Tag != null)
            {
                var info = (PakFormatTes.F4_HeaderFile2)file.Info;
                //var tag = (PakFormat02.F4_HeaderInfo2Chunk)file.Tag;
                // Fill DDS Header
                var ddsHeader = new DDSHeader
                {
                    dwFlags = DDSFlags.HEADER_FLAGS_TEXTURE | DDSFlags.HEADER_FLAGS_LINEARSIZE | DDSFlags.HEADER_FLAGS_MIPMAP,
                    dwHeight = info.Height,
                    dwWidth = info.Width,
                    dwMipMapCount = info.NumMips,
                    dwCaps = DDSCaps.SURFACE_FLAGS_TEXTURE | DDSCaps.SURFACE_FLAGS_MIPMAP,
                    dwCaps2 = info.Unk16 == 2049 ? DDSCaps2.CUBEMAP_ALLFACES : 0,
                };
                var dx10Header = new DDSHeader_DXT10();
                var dx10 = false;
                // map tex format
                switch ((DXGIFormat)info.Format)
                {
                    case DXGIFormat.BC1_UNORM:
                        ddsHeader.ddspf.dwFlags = DDSPixelFormats.FourCC;
                        ddsHeader.ddspf.dwFourCC = Encoding.ASCII.GetBytes("DXT1");
                        ddsHeader.dwPitchOrLinearSize = (uint)info.Width * info.Height / 2U; // 4bpp
                        break;
                    case DXGIFormat.BC2_UNORM:
                        ddsHeader.ddspf.dwFlags = DDSPixelFormats.FourCC;
                        ddsHeader.ddspf.dwFourCC = Encoding.ASCII.GetBytes("DXT3");
                        ddsHeader.dwPitchOrLinearSize = (uint)info.Width * info.Height; // 8bpp
                        break;
                    case DXGIFormat.BC3_UNORM:
                        ddsHeader.ddspf.dwFlags = DDSPixelFormats.FourCC;
                        ddsHeader.ddspf.dwFourCC = Encoding.ASCII.GetBytes("DXT5");
                        ddsHeader.dwPitchOrLinearSize = (uint)info.Width * info.Height; // 8bpp
                        break;
                    case DXGIFormat.BC5_UNORM:
                        ddsHeader.ddspf.dwFlags = DDSPixelFormats.FourCC;
                        ddsHeader.ddspf.dwFourCC = Encoding.ASCII.GetBytes("ATI2");
                        ddsHeader.dwPitchOrLinearSize = (uint)info.Width * info.Height; // 8bpp
                        break;
                    case DXGIFormat.BC7_UNORM:
                        ddsHeader.ddspf.dwFlags = DDSPixelFormats.FourCC;
                        ddsHeader.ddspf.dwFourCC = Encoding.ASCII.GetBytes("DX10");
                        ddsHeader.dwPitchOrLinearSize = (uint)info.Width * info.Height; // 8bpp
                        dx10 = true;
                        dx10Header.dxgiFormat = DXGIFormat.BC7_UNORM;
                        break;
                    case DXGIFormat.DXGI_FORMAT_B8G8R8A8_UNORM:
                        ddsHeader.ddspf.dwFlags = DDSPixelFormats.RGB | DDSPixelFormats.AlphaPixels;
                        ddsHeader.ddspf.dwRGBBitCount = 32;
                        ddsHeader.ddspf.dwRBitMask = 0x00FF0000;
                        ddsHeader.ddspf.dwGBitMask = 0x0000FF00;
                        ddsHeader.ddspf.dwBBitMask = 0x000000FF;
                        ddsHeader.ddspf.dwABitMask = 0xFF000000;
                        ddsHeader.dwPitchOrLinearSize = (uint)info.Width * info.Height * 4; // 32bpp
                        break;
                    case DXGIFormat.DXGI_FORMAT_R8_UNORM:
                        ddsHeader.ddspf.dwFlags = DDSPixelFormats.RGB;
                        ddsHeader.ddspf.dwRGBBitCount = 8;
                        ddsHeader.ddspf.dwRBitMask = 0xFF;
                        ddsHeader.dwPitchOrLinearSize = (uint)info.Width * info.Height; // 8bpp
                        break;
                    default: throw new InvalidOperationException("DDS FAILED");
                }
                if (dx10)
                {
                    dx10Header.resourceDimension = DDSDimension.Texture2D;
                    dx10Header.miscFlag = 0;
                    dx10Header.arraySize = 1;
                    dx10Header.miscFlags2 = 0;
                    dx10Header.Write(null);
                    //char dds2[sizeof(dx10Header)];
                    //memcpy(dds2, &dx10Header, sizeof(dx10Header));
                    //content.append(QByteArray::fromRawData(dds2, sizeof(dx10Header)));
                }
            }
            return Task.FromResult(fileData);
        }

        public override Task WriteAsync(CorePakFile source, BinaryWriter w, FileMetadata file, byte[] data, Action<FileMetadata, string> exception = null) => throw new NotImplementedException();
    }
}