using GameEstate.Graphics.OpenGL;
using System.Collections.Generic;

namespace GameEstate.Graphics.ShaderBuilders
{
    public class OpenGLShaderBuilder : AbstractShaderBuilder<Shader>
    {
        static readonly ShaderLoader _loader = new ShaderDebugLoader();

        public override Shader BuildShader(string path, IDictionary<string, bool> arguments) => _loader.LoadShader(path, arguments);
        public override Shader BuildPlaneShader(string path, IDictionary<string, bool> arguments) => _loader.LoadPlaneShader(path, arguments);
    }
}