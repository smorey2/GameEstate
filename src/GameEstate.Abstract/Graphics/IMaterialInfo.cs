using System.Collections.Generic;
using System.Numerics;

namespace GameEstate.Graphics
{
    /// <summary>
    /// IMaterialInfo
    /// </summary>
    public interface IMaterialInfo
    {
        //string Name { get; }
        string ShaderName { get; set; }

        Dictionary<string, long> IntParams { get; }
        Dictionary<string, float> FloatParams { get; }
        Dictionary<string, Vector4> VectorParams { get; }
        Dictionary<string, string> TextureParams { get; }
        Dictionary<string, long> IntAttributes { get; }
        //Dictionary<string, float> FloatAttributes { get; }
        //Dictionary<string, Vector4> VectorAttributes { get; }
        //Dictionary<string, string> StringAttributes { get; }

        IDictionary<string, bool> GetShaderArguments();
    }
}