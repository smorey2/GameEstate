using System;
using System.Collections.Generic;
using GameEstate.Toy.Models;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace GameEstate.Toy.Renderer
{
    public class RenderMaterial
    {
        public Material Material { get; }
        public Dictionary<string, int> Textures { get; } = new Dictionary<string, int>();
        public bool IsBlended => _isTranslucent;

        readonly float _flAlphaTestReference;
        readonly bool _isTranslucent;
        readonly bool _isAdditiveBlend;
        readonly bool _isRenderBackfaces;

        public RenderMaterial(Material material)
        {
            Material = material;

            if (material.IntParams.ContainsKey("F_ALPHA_TEST") &&
                material.IntParams["F_ALPHA_TEST"] == 1 &&
                material.FloatParams.ContainsKey("g_flAlphaTestReference"))
                _flAlphaTestReference = material.FloatParams["g_flAlphaTestReference"];

            _isTranslucent = (material.IntParams.ContainsKey("F_TRANSLUCENT") && material.IntParams["F_TRANSLUCENT"] == 1) || material.IntAttributes.ContainsKey("mapbuilder.water");
            _isAdditiveBlend = material.IntParams.ContainsKey("F_ADDITIVE_BLEND") && material.IntParams["F_ADDITIVE_BLEND"] == 1;
            _isRenderBackfaces = material.IntParams.ContainsKey("F_RENDER_BACKFACES") && material.IntParams["F_RENDER_BACKFACES"] == 1;
        }

        public void Render(Shader shader)
        {
            // Start at 1, texture unit 0 is reserved for the animation texture
            var textureUnit = 1;
            int uniformLocation;

            foreach (var texture in Textures)
            {
                uniformLocation = shader.GetUniformLocation(texture.Key);

                if (uniformLocation > -1)
                {
                    GL.ActiveTexture(TextureUnit.Texture0 + textureUnit);
                    GL.BindTexture(TextureTarget.Texture2D, texture.Value);
                    GL.Uniform1(uniformLocation, textureUnit);

                    textureUnit++;
                }
            }

            foreach (var param in Material.FloatParams)
            {
                uniformLocation = shader.GetUniformLocation(param.Key);

                if (uniformLocation > -1)
                    GL.Uniform1(uniformLocation, param.Value);
            }

            foreach (var param in Material.VectorParams)
            {
                uniformLocation = shader.GetUniformLocation(param.Key);

                if (uniformLocation > -1)
                    GL.Uniform4(uniformLocation, new Vector4(param.Value.X, param.Value.Y, param.Value.Z, param.Value.W));
            }

            var alphaReference = shader.GetUniformLocation("g_flAlphaTestReference");

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
