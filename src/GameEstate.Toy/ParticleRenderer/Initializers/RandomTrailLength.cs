using System;
using System.Collections.Generic;

namespace GameEstate.Toy.ParticleRenderer.Initializers
{
    public class RandomTrailLength : IParticleInitializer
    {
        readonly Random _random = new Random();

        readonly float _minLength = 0.1f;
        readonly float _maxLength = 0.1f;

        public RandomTrailLength(Dictionary<string, object> keyValues)
        {
            _minLength = keyValues.GetFloat("m_flMinLength", 0.1f);
            _maxLength = keyValues.GetFloat("m_flMaxLength", 0.1f);
        }

        public Particle Initialize(ref Particle particle, ParticleSystemRenderState particleSystemState)
        {
            particle.TrailLength = _minLength + ((float)_random.NextDouble() * (_maxLength - _minLength));

            return particle;
        }
    }
}
