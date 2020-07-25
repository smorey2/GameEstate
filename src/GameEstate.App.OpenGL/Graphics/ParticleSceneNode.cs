using GameEstate.Formats.Valve.Blocks;
using GameEstate.Graphics.OpenGL;
using GameEstate.Graphics.ParticleSystem;
using System.Collections;
using System.Collections.Generic;

namespace GameEstate.Graphics
{
    public class ParticleSceneNode : SceneNode
    {
        class ParticleSystemWrapper : IParticleSystem
        {
            readonly DATAParticleSystem _source;
            public ParticleSystemWrapper(DATAParticleSystem source) => _source = source;
            IDictionary<string, object> IParticleSystem.Data => _source.Data;
            IEnumerable<IDictionary<string, object>> IParticleSystem.Renderers => _source.Renderers;
            IEnumerable<IDictionary<string, object>> IParticleSystem.Operators => _source.Operators;
            IEnumerable<IDictionary<string, object>> IParticleSystem.Initializers => _source.Initializers;
            IEnumerable<IDictionary<string, object>> IParticleSystem.Emitters => _source.Emitters;
            IEnumerable<string> IParticleSystem.GetChildParticleNames(bool enabledOnly) => _source.GetChildParticleNames(enabledOnly);
        }

        GLParticleRenderer _particleRenderer;

        public ParticleSceneNode(Scene scene, DATAParticleSystem particleSystem)
            : base(scene)
        {
            _particleRenderer = new GLParticleRenderer(Scene.Context as IGLContext, new ParticleSystemWrapper(particleSystem));
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
