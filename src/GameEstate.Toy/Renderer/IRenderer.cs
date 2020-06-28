using System;

namespace GameEstate.Toy.Renderer
{
    public interface IRenderer
    {
        AABB BoundingBox { get; }

        void Render(Camera camera, RenderPass renderPass);

        void Update(float frameTime);
    }
}
