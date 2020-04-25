using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameEstate.Core
{
    public abstract class AbstractHost
    {
        public abstract Task<HashSet<string>> GetSetAsync(bool shouldThrow = false);
        public abstract Task<byte[]> GetFileAsync(string filePath, bool shouldThrow = false);
    }
}