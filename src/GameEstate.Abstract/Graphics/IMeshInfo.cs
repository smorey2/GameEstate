using System.Collections.Generic;
using System.Numerics;

namespace GameEstate.Graphics
{
    /// <summary>
    /// IMeshInfo
    /// </summary>
    public interface IMeshInfo
    {
        IDictionary<string, object> GetData();

        IVBIB VBIB { get; }

        Vector3 MinBounds { get; }
        Vector3 MaxBounds { get; }
    }
}