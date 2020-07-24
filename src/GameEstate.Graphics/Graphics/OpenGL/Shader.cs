using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace GameEstate.Graphics.OpenGL
{
    public class Shader
    {
        public enum ShaderKind
        {
            Normal,
            Plane
        }

        public string Name { get; set; }
        public int Program { get; set; }
#pragma warning disable CA2227 // Collection properties should be read only
        public IDictionary<string, bool> Parameters { get; set; }
        public List<string> RenderModes { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

        Dictionary<string, int> Uniforms { get; } = new Dictionary<string, int>();

        public int GetUniformLocation(string name)
        {
            if (Uniforms.TryGetValue(name, out var value))
                return value;

            value = GL.GetUniformLocation(Program, name);
            Uniforms[name] = value;
            return value;
        }
    }
}
