using System;

namespace GameEstate.Graphics.ParticleSystem.Operators
{
    public interface IParticleOperator
    {
        void Update(Span<Particle> particles, float frameTime, ParticleSystemRenderState particleSystemState);
    }
}
