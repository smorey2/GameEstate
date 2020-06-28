namespace GameEstate.Toy.ParticleRenderer.Initializers
{
    public interface IParticleInitializer
    {
        Particle Initialize(ref Particle particle, ParticleSystemRenderState particleSystemState);
    }
}
