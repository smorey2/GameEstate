using GameEstate.Formats.Valve.Blocks;
using GameEstate.Graphics.OpenGL;
using System.Collections.Generic;
using System.Numerics;

namespace GameEstate.Graphics
{
    public class MeshSceneNode : SceneNode, IMeshCollection
    {
        GLMesh _mesh;

        public MeshSceneNode(Scene scene, DATAMesh mesh, Dictionary<string, string> skinMaterials = null)
            : base(scene)
        {
            _mesh = new GLMesh(Scene.Context as IGLContext, mesh, skinMaterials);
            LocalBoundingBox = _mesh.BoundingBox;
        }

        public Vector4 Tint
        {
            get => _mesh.Tint;
            set => _mesh.Tint = value;
        }

        public IEnumerable<Mesh> Meshes
        {
            get { yield return _mesh; }
        }

        public override IEnumerable<string> GetSupportedRenderModes() => _mesh.GetSupportedRenderModes();

        public override void SetRenderMode(string renderMode) => _mesh.SetRenderMode(renderMode);

        public override void Update(Scene.UpdateContext context) => _mesh.Update(context.Timestep);

        public override void Render(Scene.RenderContext context) { } // This node does not render itself; it uses the batching system via IRenderableMeshCollection
    }
}
