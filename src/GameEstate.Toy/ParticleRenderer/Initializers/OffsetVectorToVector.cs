using System;
using System.Collections.Generic;
using System.Numerics;

namespace GameEstate.Toy.ParticleRenderer.Initializers
{
    public class OffsetVectorToVector : IParticleInitializer
    {
        readonly Random random = new Random();

        readonly ParticleField _inputField;
        readonly ParticleField _outputField;
        readonly Vector3 _offsetMin;
        readonly Vector3 _offsetMax;

        public OffsetVectorToVector(IDictionary<string, object> keyValues)
        {
            _inputField = (ParticleField)keyValues.GetInt("m_nFieldInput");
            _outputField = (ParticleField)keyValues.GetInt("m_nFieldOutput");
            _offsetMin = keyValues.TryGet<double[]>("m_vecOutputMin", out var vectorValues)
                ? new Vector3((float)vectorValues[0], (float)vectorValues[1], (float)vectorValues[2])
                : Vector3.Zero;
            _offsetMax = keyValues.TryGet<double[]>("m_vecOutputMax", out vectorValues)
                ? new Vector3((float)vectorValues[0], (float)vectorValues[1], (float)vectorValues[2])
                : Vector3.One;
        }

        public Particle Initialize(ref Particle particle, ParticleSystemRenderState particleSystemState)
        {
            var input = particle.GetVector(_inputField);

            var offset = new Vector3(
                Lerp(_offsetMin.X, _offsetMax.X, (float)random.NextDouble()),
                Lerp(_offsetMin.Y, _offsetMax.Y, (float)random.NextDouble()),
                Lerp(_offsetMin.Z, _offsetMax.Z, (float)random.NextDouble()));

            if (_outputField == ParticleField.Position)
                particle.Position += input + offset;
            else if (_outputField == ParticleField.PositionPrevious)
                particle.PositionPrevious = input + offset;

            return particle;
        }

        static float Lerp(float min, float max, float t)
           => min + (t * (max - min));
    }
}
