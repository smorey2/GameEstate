using GameEstate.Graphics.Scene;
using GameEstate.Toy.Models;
using System.Collections.Generic;
using System.Numerics;

namespace GameEstate.Toy.Renderer
{
    public class MeshSceneNode : SceneNode, IRenderableMeshCollection
    {
        RenderableMesh meshRenderer;

        public MeshSceneNode(Scene scene, Mesh mesh, Dictionary<string, string> skinMaterials = null)
            : base(scene)
        {
            meshRenderer = new RenderableMesh(mesh, Scene.Context, skinMaterials);
            LocalBoundingBox = meshRenderer.BoundingBox;
        }

        public Vector4 Tint
        {
            get => meshRenderer.Tint;
            set => meshRenderer.Tint = value;
        }

        public IEnumerable<RenderableMesh> RenderableMeshes
        {
            get { yield return meshRenderer; }
        }

        public override IEnumerable<string> GetSupportedRenderModes() => meshRenderer.GetSupportedRenderModes();

        public override void SetRenderMode(string renderMode) => meshRenderer.SetRenderMode(renderMode);

        public override void Update(Scene.UpdateContext context) => meshRenderer.Update(context.Timestep);

        public override void Render(Scene.RenderContext context) { } // This node does not render itself; it uses the batching system via IRenderableMeshCollection
    }
}
