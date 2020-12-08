using System.Collections.Generic;

namespace GameEstate.Graphics
{
    /// <summary>
    /// ITextureInfo
    /// </summary>
    public interface ITextureInfo
    {
        byte[] this[int index] { get; set; }
        IDictionary<string, object> Data { get; }
        int Width { get; }
        int Height { get; }
        int Depth { get; }
        TextureFlags Flags { get; }
        TextureUnityFormat UnityFormat { get; }
        TextureGLFormat GLFormat { get; }
        int NumMipMaps { get; }
        void MoveToData();
    }
}