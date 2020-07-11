using System.Numerics;

namespace GameEstate.Graphics.ParticleSystem.Renderers
{
    public interface IParticleRenderer
    {
        void Render(ParticleBag particles, Matrix4x4 viewProjectionMatrix, Matrix4x4 modelViewMatrix);
    }
}
