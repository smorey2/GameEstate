using GameEstate.Formats.Valve.Blocks;
using GameEstate.Graphics.OpenGL;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace GameEstate.Graphics
{
    public class Material
    {
        public Shader Shader { get; private set; }
        public DATAMaterial Data { get; private set; }
        public Dictionary<string, int> Textures { get; } = new Dictionary<string, int>();
        public bool IsBlended => _isTranslucent;

        float _flAlphaTestReference;
        bool _isTranslucent;
        bool _isAdditiveBlend;
        bool _isRenderBackfaces;

        public Material(Shader shader) => Shader = shader;

        public void Load(DATAMaterial data)
        {
            Data = data;
            if (data.IntParams.ContainsKey("F_ALPHA_TEST") && data.IntParams["F_ALPHA_TEST"] == 1 && data.FloatParams.ContainsKey("g_flAlphaTestReference"))
                _flAlphaTestReference = data.FloatParams["g_flAlphaTestReference"];
            _isTranslucent = (data.IntParams.ContainsKey("F_TRANSLUCENT") && data.IntParams["F_TRANSLUCENT"] == 1) || data.IntAttributes.ContainsKey("mapbuilder.water");
            _isAdditiveBlend = data.IntParams.ContainsKey("F_ADDITIVE_BLEND") && data.IntParams["F_ADDITIVE_BLEND"] == 1;
            _isRenderBackfaces = data.IntParams.ContainsKey("F_RENDER_BACKFACES") && data.IntParams["F_RENDER_BACKFACES"] == 1;
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

            foreach (var param in Data.FloatParams)
            {
                uniformLocation = Shader.GetUniformLocation(param.Key);
                if (uniformLocation > -1)
                    GL.Uniform1(uniformLocation, param.Value);
            }

            foreach (var param in Data.VectorParams)
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
