using System.Numerics;

namespace GameEstate.Graphics.Scene
{
    public struct MeshBatchRequest
    {
        public Matrix4x4 Transform;
        public RenderableMesh Mesh;
        public object Call;
        public float DistanceFromCamera;
    }
}
