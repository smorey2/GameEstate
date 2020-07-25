using System;
using System.Collections.Generic;

namespace GameEstate.Graphics
{
    public interface IGraphicContext
    {
        // load
        T LoadFile<T>(string filePath);

        // shader
        public Func<string, IDictionary<string, bool>, Shader> LoadShader { get; }
        public Func<string, IDictionary<string, bool>, Shader> LoadPlaneShader { get; }

        // texture
        public Func<string, (int texture, TextureInfo info)> LoadTexture { get; }
        public int ErrorTexture { get; }
    }
}
