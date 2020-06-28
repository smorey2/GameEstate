using System;
using System.Numerics;

namespace GameEstate.Toy.ParticleRenderer.Renderers
{
    public interface IParticleRenderer
    {
        void Render(ParticleBag particles, Matrix4x4 viewProjectionMatrix, Matrix4x4 modelViewMatrix);
    }
}
