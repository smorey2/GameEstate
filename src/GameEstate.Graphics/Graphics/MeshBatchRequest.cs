using GameEstate.Graphics.OpenGL;
using System.Numerics;

namespace GameEstate.Graphics
{
    public struct MeshBatchRequest
    {
        public Matrix4x4 Transform;
        public Mesh Mesh;
        public DrawCall Call;
        public float DistanceFromCamera;
        //
        public GLMesh GLMesh => (GLMesh)Mesh;
    }
}
