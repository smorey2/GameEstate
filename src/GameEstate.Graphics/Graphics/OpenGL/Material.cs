using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace GameEstate.Graphics.OpenGL
{
    public class Material
    {
        public Shader Shader { get; private set; }
        public IMaterialInfo Info { get; private set; }
        public Dictionary<string, int> Textures { get; } = new Dictionary<string, int>();
        public bool IsBlended => _isTranslucent;

        float _flAlphaTestReference;
        bool _isTranslucent;
        bool _isAdditiveBlend;
        bool _isRenderBackfaces;

        public Material(IMaterialInfo info)
        {
            Info = info;
            if (info.IntParams.ContainsKey("F_ALPHA_TEST") && info.IntParams["F_ALPHA_TEST"] == 1 && info.FloatParams.ContainsKey("g_flAlphaTestReference"))
                _flAlphaTestReference = info.FloatParams["g_flAlphaTestReference"];
            _isTranslucent = (info.IntParams.ContainsKey("F_TRANSLUCENT") && info.IntParams["F_TRANSLUCENT"] == 1) || info.IntAttributes.ContainsKey("mapbuilder.water");
            _isAdditiveBlend = info.IntParams.ContainsKey("F_ADDITIVE_BLEND") && info.IntParams["F_ADDITIVE_BLEND"] == 1;
            _isRenderBackfaces = info.IntParams.ContainsKey("F_RENDER_BACKFACES") && info.IntParams["F_RENDER_BACKFACES"] == 1;
        }

        public void Render(Shader shader)
        {
            Shader = shader;

            // Start at 1, texture unit 0 is reserved for the animation texture
            var textureUnit = 1;
            int uniformLocation;

            foreach (var texture in Textures)
            {
                uniformLocation = Shader.GetUniformLocation(texture.Key);
                if (uniformLocation > -1)
                {
                    GL.ActiveTexture(TextureUnit.Texture0 + textureUnit);
                    GL.BindTexture(TextureTarget.Texture2D, texture.Value);
                    GL.Uniform1(uniformLocation, textureUnit);
                    textureUnit++;
                }
            }

            foreach (var param in Info.FloatParams)
            {
                uniformLocation = Shader.GetUniformLocation(param.Key);
                if (uniformLocation > -1)
                    GL.Uniform1(uniformLocation, param.Value);
            }

            foreach (var param in Info.VectorParams)
            {
                uniformLocation = Shader.GetUniformLocation(param.Key);
                if (uniformLocation > -1)
                    GL.Uniform4(uniformLocation, new Vector4(param.Value.X, param.Value.Y, param.Value.Z, param.Value.W));
            }

            var alphaReference = Shader.GetUniformLocation("g_flAlphaTestReference");
            if (alphaReference > -1)
                GL.Uniform1(alphaReference, _flAlphaTestReference);

            if (_isTranslucent)
            {
                GL.DepthMask(false);
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactor.SrcAlpha, _isAdditiveBlend ? BlendingFactor.One : BlendingFactor.OneMinusSrcAlpha);
            }

            if (_isRenderBackfaces)
                GL.Disable(EnableCap.CullFace);
        }

        public void PostRender()
        {
            if (_isTranslucent)
            {
                GL.DepthMask(true);
                GL.Disable(EnableCap.Blend);
            }

            if (_isRenderBackfaces)
                GL.Enable(EnableCap.CullFace);
        }
    }
}
