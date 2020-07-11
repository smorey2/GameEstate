using GameEstate.Core;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

// https://docs.microsoft.com/en-us/windows/win32/direct3ddds/dds-header
namespace GameEstate.Graphics.DirectX
{
    /// <summary>
    /// Flags to indicate which members contain valid data.
    /// </summary>
    [Flags]
    public enum DDSFlags : uint
    {
        /// <summary>
        /// Required in every .dds file.
        /// </summary>
        Caps = 0x1,
        /// <summary>
        /// Required in every .dds file.
        /// </summary>
        Height = 0x2,
        /// <summary>
        /// Required in every .dds file.
        /// </summary>
        Width = 0x4,
        /// <summary>
        /// Required when pitch is provided for an uncompressed texture.
        /// </summary>
        Pitch = 0x8,
        /// <summary>
        /// Required in every .dds file.
        /// </summary>
        PixelFormat = 0x1000,
        /// <summary>
        /// Required in a mipmapped texture.
        /// </summary>
        MipmapCount = 0x20000,
        /// <summary>
        /// Required when pitch is provided for a compressed texture.
        /// </summary>
        LinearSize = 0x80000,
        /// <summary>
        /// Required in a depth texture.
        /// </summary>
        Depth = 0x800000,
        HEADER_FLAGS_TEXTURE = Caps | Height | Width | PixelFormat,
        HEADER_FLAGS_MIPMAP = MipmapCount,
        HEADER_FLAGS_VOLUME = Depth,
        HEADER_FLAGS_PITCH = Pitch,
        HEADER_FLAGS_LINEARSIZE = LinearSize,
    }

    /// <summary>
    /// Specifies the complexity of the surfaces stored.
    /// </summary>
    [Flags]
    public enum DDSCaps : uint //: dwSurfaceFlags
    {
        /// <summary>
        /// Optional; must be used on any file that contains more than one surface (a mipmap, a cubic environment map, or mipmapped volume texture).
        /// </summary>
        Complex = 0x8,
        /// <summary>
        /// Optional; should be used for a mipmap.
        /// </summary>
        Mipmap = 0x400000,
        /// <summary>
        /// Required
        /// </summary>
        Texture = 0x1000,
        SURFACE_FLAGS_MIPMAP = Complex | Mipmap,
        SURFACE_FLAGS_TEXTURE = Texture,
        SURFACE_FLAGS_CUBEMAP = Complex,
    }

    /// <summary>
    /// Additional detail about the surfaces stored.
    /// </summary>
    [Flags]
    public enum DDSCaps2 : uint //: dwCubemapFlags
    {
        /// <summary>
        /// Required for a cube map.
        /// </summary>
        Cubemap = 0x200,
        /// <summary>
        /// Required when these surfaces are stored in a cube map.	
        /// </summary>
        CubemapPositiveX = 0x400,
        /// <summary>
        /// Required when these surfaces are stored in a cube map.
        /// </summary>
        CubemapNegativeX = 0x800,
        /// <summary>
        /// Required when these surfaces are stored in a cube map.
        /// </summary>
        CubemapPositiveY = 0x1000,
        /// <summary>
        /// Required when these surfaces are stored in a cube map.
        /// </summary>
        CubemapNegativeY = 0x2000,
        /// <summary>
        /// Required when these surfaces are stored in a cube map.
        /// </summary>
        CubemapPositiveZ = 0x4000,
        /// <summary>
        /// Required when these surfaces are stored in a cube map.
        /// </summary>
        CubemapNegativeZ = 0x8000,
        /// <summary>
        /// Required for a volume texture.
        /// </summary>
        Volume = 0x200000,
        CUBEMAP_POSITIVEX = Cubemap | CubemapPositiveX,
        CUBEMAP_NEGATIVEX = Cubemap | CubemapNegativeX,
        CUBEMAP_POSITIVEY = Cubemap | CubemapPositiveY,
        CUBEMAP_NEGATIVEY = Cubemap | CubemapNegativeY,
        CUBEMAP_POSITIVEZ = Cubemap | CubemapPositiveZ,
        CUBEMAP_NEGATIVEZ = Cubemap | CubemapNegativeZ,
        CUBEMAP_ALLFACES = CubemapPositiveX | CubemapNegativeX | CubemapPositiveY | CubemapNegativeY | CubemapPositiveZ | CubemapNegativeZ,
        FLAGS_VOLUME = Volume,
    }

    /// <summary>
    /// Describes a DDS file header.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
    public unsafe struct DDS_HEADER
    {
        /// <summary>
        /// Literal
        /// </summary>
        public static class Literal
        {
            public static readonly byte[] DDS_ = Encoding.ASCII.GetBytes("DDS ");
            public static readonly byte[] DX10 = Encoding.ASCII.GetBytes("DX10");
            public static readonly byte[] DXT1 = Encoding.ASCII.GetBytes("DXT1");
            public static readonly byte[] DXT3 = Encoding.ASCII.GetBytes("DXT3");
            public static readonly byte[] DXT5 = Encoding.ASCII.GetBytes("DXT5");
            public static readonly byte[] ATI2 = Encoding.ASCII.GetBytes("ATI2");
        }

        /// <summary>
        /// The size of
        /// </summary>
        public const int SizeOf = 124;

        /// <summary>
        /// Size of structure. This member must be set to 124.
        /// </summary>
        /// <value>
        /// The size of the dw.
        /// </value>
        public uint dwSize; //: 124
        /// <summary>
        /// Flags to indicate which members contain valid data.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)] public DDSFlags dwFlags;
        /// <summary>
        /// Surface height (in pixels).
        /// </summary>
        public uint dwHeight;
        /// <summary>
        /// Surface width (in pixels).
        /// </summary>
        public uint dwWidth;
        /// <summary>
        /// The pitch or number of bytes per scan line in an uncompressed texture; the total number of bytes in the top level texture for a compressed texture. For information about how to compute the pitch, see the DDS File Layout section of the Programming Guide for DDS.
        /// </summary>
        public uint dwPitchOrLinearSize;
        /// <summary>
        /// Depth of a volume texture (in pixels), otherwise unused.
        /// </summary>
        public uint dwDepth; // only if DDS_HEADER_FLAGS_VOLUME is set in flags
        /// <summary>
        /// Number of mipmap levels, otherwise unused.
        /// </summary>
        public uint dwMipMapCount;
        /// <summary>
        /// Unused.
        /// </summary>
        public fixed uint dwReserved1[11];
        /// <summary>
        /// The pixel format (see DDS_PIXELFORMAT).
        /// </summary>
        public DDS_PIXELFORMAT ddspf;
        /// <summary>
        /// Specifies the complexity of the surfaces stored.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)] public DDSCaps dwCaps;
        /// <summary>
        /// Additional detail about the surfaces stored.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)] public DDSCaps2 dwCaps2;
        /// <summary>
        /// The dw caps3
        /// </summary>
        public uint dwCaps3;
        /// <summary>
        /// The dw caps4
        /// </summary>
        public uint dwCaps4;
        /// <summary>
        /// The dw reserved2
        /// </summary>
        public uint dwReserved2;

        /// <summary>
        /// Verifies this instance.
        /// </summary>
        public void Verify()
        {
            if (dwSize != 124)
                throw new FileFormatException($"Invalid DDS file header size: {dwSize}.");
            if (!dwFlags.HasFlag(DDSFlags.Height | DDSFlags.Width))
                throw new FileFormatException($"Invalid DDS file flags: {dwFlags}.");
            if (!dwCaps.HasFlag(DDSCaps.Texture))
                throw new FileFormatException($"Invalid DDS file caps: {dwCaps}.");
            if (!dwCaps.HasFlag(DDSCaps.Texture))
                throw new FileFormatException($"Invalid DDS file caps: {dwCaps}.");
            if (ddspf.dwSize != 32)
                throw new FileFormatException($"Invalid DDS file pixel format size: {ddspf.dwSize}.");
        }

        /// <summary>
        /// DecodeAndRead
        /// </summary>
        public void DecodeAndRead(TextureInfo source, BinaryReader reader)
        {
            var hasMipmaps = dwCaps.HasFlag(DDSCaps.Mipmap);
            source.Mipmaps = hasMipmaps ? (ushort)dwMipMapCount : (ushort)1U; // Non-mipmapped textures still have one mipmap level: the texture itself.
            source.Width = (int)dwWidth;
            source.Height = (int)dwHeight;
            source.BytesPerPixel = 4;
            // If the DDS file contains uncompressed data.
            if (ddspf.dwFlags.HasFlag(DDPF.RGB))
            {
                if (!ddspf.dwFlags.HasFlag(DDPF.AlphaPixels)) throw new NotImplementedException("Unsupported DDS file pixel format.");
                else
                {
                    if (ddspf.dwRGBBitCount != 32) throw new FileFormatException("Invalid DDS file pixel format.");
                    // BGRA32
                    if (ddspf.dwBBitMask == 0x000000FF && ddspf.dwGBitMask == 0x0000FF00 && ddspf.dwRBitMask == 0x00FF0000 && ddspf.dwABitMask == 0xFF000000) { source.UnityFormat = TextureUnityFormat.BGRA32; }
                    // ARGB32
                    else if (ddspf.dwABitMask == 0x000000FF && ddspf.dwRBitMask == 0x0000FF00 && ddspf.dwGBitMask == 0x00FF0000 && ddspf.dwBBitMask == 0xFF000000) { source.UnityFormat = TextureUnityFormat.ARGB32; }
                    else throw new NotImplementedException("Unsupported DDS file pixel format.");
                    reader.ReadToEnd(source.Data = new byte[!hasMipmaps ? (int)(dwPitchOrLinearSize * dwHeight) : source.GetMipmapDataSize()]);
                }
            }
            else if (Literal.DXT1.SequenceEqual(ddspf.dwFourCC))
            {
                source.UnityFormat = TextureUnityFormat.ARGB32;
                source.Data = reader.ReadToEnd();
                //source.Data = DecodeDXT1ToARGB(compressedTextureData, dwWidth, dwHeight, ddspf, source.Mipmaps);
            }
            else if (Literal.DXT3.SequenceEqual(ddspf.dwFourCC))
            {
                source.UnityFormat = TextureUnityFormat.ARGB32;
                source.Data = reader.ReadToEnd();
                //source.Data = DecodeDXT3ToARGB(compressedTextureData, dwWidth, dwHeight, ddspf, source.Mipmaps);
            }
            else if (Literal.DXT5.SequenceEqual(ddspf.dwFourCC))
            {
                source.UnityFormat = TextureUnityFormat.ARGB32;
                source.Data = reader.ReadToEnd();
                //source.Data = DecodeDXT5ToARGB(compressedTextureData, dwWidth, dwHeight, ddspf, source.Mipmaps);
            }
            else throw new NotImplementedException("Unsupported DDS file pixel format.");
        }
    }
}