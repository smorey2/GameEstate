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
    public enum DDSD : uint
    {
        /// <summary>
        /// Required in every .dds file.
        /// </summary>
        CAPS = 0x00000001,
        /// <summary>
        /// Required in every .dds file.
        /// </summary>
        HEIGHT = 0x00000002,
        /// <summary>
        /// Required in every .dds file.
        /// </summary>
        WIDTH = 0x00000004,
        /// <summary>
        /// Required when pitch is provided for an uncompressed texture.
        /// </summary>
        PITCH = 0x00000008,
        /// <summary>
        /// Required in every .dds file.
        /// </summary>
        PIXELFORMAT = 0x00001000,
        /// <summary>
        /// Required in a mipmapped texture.
        /// </summary>
        MIPMAPCOUNT = 0x00020000,
        /// <summary>
        /// Required when pitch is provided for a compressed texture.
        /// </summary>
        LINEARSIZE = 0x00080000,
        /// <summary>
        /// Required in a depth texture.
        /// </summary>
        DEPTH = 0x00800000,
        HEADER_FLAGS_TEXTURE = CAPS | HEIGHT | WIDTH | PIXELFORMAT,
        HEADER_FLAGS_MIPMAP = MIPMAPCOUNT,
        HEADER_FLAGS_VOLUME = DEPTH,
        HEADER_FLAGS_PITCH = PITCH,
        HEADER_FLAGS_LINEARSIZE = LINEARSIZE,
    }

    /// <summary>
    /// Specifies the complexity of the surfaces stored.
    /// </summary>
    [Flags]
    public enum DDSCAPS : uint //: dwSurfaceFlags
    {
        /// <summary>
        /// Optional; must be used on any file that contains more than one surface (a mipmap, a cubic environment map, or mipmapped volume texture).
        /// </summary>
        COMPLEX = 0x00000008,
        /// <summary>
        /// Required
        /// </summary>
        TEXTURE = 0x00001000,
        /// <summary>
        /// Optional; should be used for a mipmap.
        /// </summary>
        MIPMAP = 0x00400000,
        SURFACE_FLAGS_MIPMAP = COMPLEX | MIPMAP,
        SURFACE_FLAGS_TEXTURE = TEXTURE,
        SURFACE_FLAGS_CUBEMAP = COMPLEX,
    }

    /// <summary>
    /// Additional detail about the surfaces stored.
    /// </summary>
    [Flags]
    public enum DDSCAPS2 : uint //: dwCubemapFlags
    {
        /// <summary>
        /// Required for a cube map.
        /// </summary>
        CUBEMAP = 0x00000200,
        /// <summary>
        /// Required when these surfaces are stored in a cube map.	
        /// </summary>
        CUBEMAPPOSITIVEX = 0x00000400,
        /// <summary>
        /// Required when these surfaces are stored in a cube map.
        /// </summary>
        CUBEMAPNEGATIVEX = 0x00000800,
        /// <summary>
        /// Required when these surfaces are stored in a cube map.
        /// </summary>
        CUBEMAPPOSITIVEY = 0x00001000,
        /// <summary>
        /// Required when these surfaces are stored in a cube map.
        /// </summary>
        CUBEMAPNEGATIVEY = 0x00002000,
        /// <summary>
        /// Required when these surfaces are stored in a cube map.
        /// </summary>
        CUBEMAPPOSITIVEZ = 0x00004000,
        /// <summary>
        /// Required when these surfaces are stored in a cube map.
        /// </summary>
        CUBEMAPNEGATIVEZ = 0x00008000,
        /// <summary>
        /// Required for a volume texture.
        /// </summary>
        VOLUME = 0x00200000,
        CUBEMAP_POSITIVEX = CUBEMAP | CUBEMAPPOSITIVEX,
        CUBEMAP_NEGATIVEX = CUBEMAP | CUBEMAPNEGATIVEX,
        CUBEMAP_POSITIVEY = CUBEMAP | CUBEMAPPOSITIVEY,
        CUBEMAP_NEGATIVEY = CUBEMAP | CUBEMAPNEGATIVEY,
        CUBEMAP_POSITIVEZ = CUBEMAP | CUBEMAPPOSITIVEZ,
        CUBEMAP_NEGATIVEZ = CUBEMAP | CUBEMAPNEGATIVEZ,
        CUBEMAP_ALLFACES = CUBEMAPPOSITIVEX | CUBEMAPNEGATIVEX | CUBEMAPPOSITIVEY | CUBEMAPNEGATIVEY | CUBEMAPPOSITIVEZ | CUBEMAPNEGATIVEZ,
        FLAGS_VOLUME = VOLUME,
    }

    /// <summary>
    /// Describes a DDS file header.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
    public unsafe struct DDS_HEADER
    {
        //public const uint MAGIC = 0x20534444; // "DDS "

        public static uint MAKEFOURCC(string text) => ((uint)(byte)(text[0]) | ((uint)(byte)(text[1]) << 8) | ((uint)(byte)(text[2]) << 16 | ((uint)(byte)(text[3]) << 24)));

        /// <summary>
        /// Literal
        /// </summary>
        public static class Literal
        {
            public static readonly uint DDS_ = MAKEFOURCC("DDS "); //?
            public static readonly uint DXT1 = MAKEFOURCC("DXT1"); // DXT1
            public static readonly uint DXT2 = MAKEFOURCC("DXT2");
            public static readonly uint DXT3 = MAKEFOURCC("DXT3"); // DXT3
            public static readonly uint DXT4 = MAKEFOURCC("DXT4");
            public static readonly uint DXT5 = MAKEFOURCC("DXT5"); // DXT5
            public static readonly uint RXGB = MAKEFOURCC("RXGB");
            public static readonly uint ATI1 = MAKEFOURCC("ATI1");
            public static readonly uint ATI2 = MAKEFOURCC("ATI2"); // ATI2
            public static readonly uint A2XY = MAKEFOURCC("A2XY");
            public static readonly uint DX10 = MAKEFOURCC("DX10"); // DX10
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
        [MarshalAs(UnmanagedType.U4)] public DDSD dwFlags;
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
        [MarshalAs(UnmanagedType.U4)] public DDSCAPS dwCaps;
        /// <summary>
        /// Additional detail about the surfaces stored.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)] public DDSCAPS2 dwCaps2;
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
            if (!dwFlags.HasFlag(DDSD.HEIGHT | DDSD.WIDTH))
                throw new FileFormatException($"Invalid DDS file flags: {dwFlags}.");
            if (!dwCaps.HasFlag(DDSCAPS.TEXTURE))
                throw new FileFormatException($"Invalid DDS file caps: {dwCaps}.");
            if (!dwCaps.HasFlag(DDSCAPS.TEXTURE))
                throw new FileFormatException($"Invalid DDS file caps: {dwCaps}.");
            if (ddspf.dwSize != 32)
                throw new FileFormatException($"Invalid DDS file pixel format size: {ddspf.dwSize}.");
        }

        /// <summary>
        /// DecodeAndRead
        /// </summary>
        public void DecodeAndRead(TextureInfo source, BinaryReader reader)
        {
            var hasMipmaps = dwCaps.HasFlag(DDSCAPS.MIPMAP);
            source.Mipmaps = hasMipmaps ? (ushort)dwMipMapCount : (ushort)1U; // Non-mipmapped textures still have one mipmap level: the texture itself.
            source.Width = (int)dwWidth;
            source.Height = (int)dwHeight;
            source.BytesPerPixel = 4;
            // If the DDS file contains uncompressed data.
            if (ddspf.dwFlags.HasFlag(DDPF.RGB))
            {
                if (!ddspf.dwFlags.HasFlag(DDPF.ALPHAPIXELS)) throw new NotImplementedException("Unsupported DDS file pixel format.");
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
            else if (ddspf.dwFourCC == Literal.DXT1)
            {
                source.UnityFormat = TextureUnityFormat.ARGB32;
                source.Data = reader.ReadToEnd();
                //source.Data = DecodeDXT1ToARGB(compressedTextureData, dwWidth, dwHeight, ddspf, source.Mipmaps);
            }
            else if (ddspf.dwFourCC == Literal.DXT3)
            {
                source.UnityFormat = TextureUnityFormat.ARGB32;
                source.Data = reader.ReadToEnd();
                //source.Data = DecodeDXT3ToARGB(compressedTextureData, dwWidth, dwHeight, ddspf, source.Mipmaps);
            }
            else if (ddspf.dwFourCC == Literal.DXT5)
            {
                source.UnityFormat = TextureUnityFormat.ARGB32;
                source.Data = reader.ReadToEnd();
                //source.Data = DecodeDXT5ToARGB(compressedTextureData, dwWidth, dwHeight, ddspf, source.Mipmaps);
            }
            else throw new NotImplementedException("Unsupported DDS file pixel format.");
        }
    }
}