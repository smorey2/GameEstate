namespace GameEstate.Graphics
{
    public interface IRenderer
    {
        AABB BoundingBox { get; }

        void Render(Camera camera, RenderPass renderPass);

        void Update(float frameTime);
    }
}
