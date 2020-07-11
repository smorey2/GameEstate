using System.Collections.Generic;

namespace GameEstate.Graphics.ParticleSystem
{
    public interface IParticleSystem : IDictionary<string, object>
    {
        IEnumerable<IDictionary<string, object>> GetRenderers();
        IEnumerable<IDictionary<string, object>> GetOperators();
        IEnumerable<IDictionary<string, object>> GetInitializers();
        IEnumerable<IDictionary<string, object>> GetEmitters();
        IEnumerable<string> GetChildParticleNames(bool enabledOnly = false);
    }
}
