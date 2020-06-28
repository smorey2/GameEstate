using System;

namespace GameEstate.Toy.ParticleRenderer.Operators
{
    public interface IParticleOperator
    {
        void Update(Span<Particle> particles, float frameTime, ParticleSystemRenderState particleSystemState);
    }
}
