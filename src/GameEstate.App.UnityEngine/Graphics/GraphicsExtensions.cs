using UnityEngine.Rendering;

namespace GameEstate.Graphics
{
    /// <summary>
    /// MatTestMode
    /// </summary>
    public enum MatTestMode { Always, Less, LEqual, Equal, GEqual, Greater, NotEqual, Never }

    /// <summary>
    /// MaterialType
    /// </summary>
    public enum MaterialType { None, Default, Standard, BumpedDiffuse, Unlit }

    public struct MaterialTextures
    {
        public string MainFilePath;
        public string DarkFilePath;
        public string DetailFilePath;
        public string GlossFilePath;
        public string GlowFilePath;
        public string BumpFilePath;
    }

    public struct MaterialProps
    {
        public MaterialTextures Textures;
        public bool AlphaBlended;
        public BlendMode SrcBlendMode;
        public BlendMode DstBlendMode;
        public bool AlphaTest;
        public float AlphaCutoff;
        public bool ZWrite;
    }

    public struct MaterialTerrain { }

    public struct MaterialBlended
    {
        public BlendMode SrcBlendMode;
        public BlendMode DstBlendMode;
    }

    public struct MaterialTested
    {
        public float Cutoff;
    }

    public static class GraphicsExtensions
    {
    }
}