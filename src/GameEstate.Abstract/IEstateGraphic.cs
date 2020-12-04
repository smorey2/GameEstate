using GameEstate.Graphics;
using System.Collections.Generic;

namespace GameEstate
{
    /// <summary>
    /// IEstateGraphic
    /// </summary>
    public interface IEstateGraphic
    {
        EstatePakFile Source { get; }
        void PreloadTexture(string texturePath);
        void PreloadObject(string filePath);
    }

    /// <summary>
    /// IEstateGraphic
    /// </summary>
    public interface IEstateGraphic<Object, Material, Texture, Shader> : IEstateGraphic
    {
        TextureManager<Texture> TextureManager { get; }
        MaterialManager<Material, Texture> MaterialManager { get; }
        ShaderManager<Shader> ShaderManager { get; }
        Texture LoadTexture(string path, out IDictionary<string, object> data);
        Object CreateObject(string path, out IDictionary<string, object> data);
        Shader LoadShader(string path, IDictionary<string, bool> args = null);
    }
}