using System;

namespace GameEstate.Graphics.OpenGL
{
    public interface IGLContext : IGraphicContext
    {
        // material
        public Func<string, Material> LoadMaterial { get; }
        int CreateSolidTexture(float v1, float v2, float v3);

        // cache
        public GpuMeshBufferCache MeshBufferCache { get; }
        public QuadIndexBuffer QuadIndices { get; }
    }
}
