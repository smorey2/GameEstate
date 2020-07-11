using GameEstate.Formats;
using System;
using System.Collections.Generic;

namespace GameEstate.Graphics.OpenGL
{
    public interface IGLContext
    {
        // loader
        public Func<string, IDictionary<string, bool>, Shader> LoadShader { get; }
        //public Func<string, IDictionary<string, bool>, object> LoadMaterial { get; }

        // texture
        public Func<string, (int texture, TextureInfo info)> LoadTexture { get; }
        public int ErrorTexture { get; }

        // cache
        public GpuMeshBufferCache MeshBufferCache { get; }
        public QuadIndexBuffer QuadIndices { get; }
    }
}
