using System.Collections.Generic;

namespace GameEstate.Graphics
{
    /// <summary>
    /// IModelInfo
    /// </summary>
    public interface IModelInfo
    {
        IDictionary<string, object> Data { get; }
    }
}