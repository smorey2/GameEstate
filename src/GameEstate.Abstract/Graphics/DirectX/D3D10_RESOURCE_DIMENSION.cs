using System;

// https://docs.microsoft.com/en-us/windows/win32/api/d3d10/ne-d3d10-d3d10_resource_dimension
namespace GameEstate.Graphics.DirectX
{
    [Flags]
    public enum D3D10_RESOURCE_DIMENSION : uint
    {
        /// <summary>
        /// Resource is of unknown type.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Resource is a buffer.
        /// </summary>
        Buffer = 1,
        /// <summary>
        /// Resource is a 1D texture. The dwWidth member of DDS_HEADER specifies the size of the texture. Typically, you set the dwHeight member of DDS_HEADER to 1; you also must set the DDSD_HEIGHT flag in the dwFlags member of DDS_HEADER.
        /// </summary>
        Texture1D = 2,
        /// <summary>
        /// Resource is a 2D texture with an area specified by the dwWidth and dwHeight members of DDS_HEADER. You can also use this type to identify a cube-map texture. For more information about how to identify a cube-map texture, see miscFlag and arraySize members.
        /// </summary>
        Texture2D = 3,
        /// <summary>
        /// Resource is a 3D texture with a volume specified by the dwWidth, dwHeight, and dwDepth members of DDS_HEADER. You also must set the DDSD_DEPTH flag in the dwFlags member of DDS_HEADER.
        /// </summary>
        Texture3D = 4,
    }
}