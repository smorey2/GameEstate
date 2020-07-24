namespace GameEstate.Graphics.Scene
{
    public class ParticleSceneNode : SceneNode
    {
        ParticleRenderer.ParticleRenderer _particleRenderer;

        public ParticleSceneNode(Scene scene, ParticleSystem particleSystem)
            : base(scene)
        {
            _particleRenderer = new ParticleRenderer.ParticleRenderer(particleSystem, Scene.Context);
            LocalBoundingBox = _particleRenderer.BoundingBox;
        }

        public override void Update(Scene.UpdateContext context)
        {
            _particleRenderer.Position = Transform.Translation;
            _particleRenderer.Update(context.Timestep);

            LocalBoundingBox = _particleRenderer.BoundingBox.Translate(-_particleRenderer.Position);
        }

        public override void Render(Scene.RenderContext context) => _particleRenderer.Render(context.Camera, context.RenderPass);
    }
}
