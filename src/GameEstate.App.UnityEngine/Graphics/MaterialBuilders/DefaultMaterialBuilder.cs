using System;
using UnityEngine;
using ur = UnityEngine.Rendering;

namespace GameEstate.Graphics.MaterialBuilders
{
    /// <summary>
    /// A material that uses the default shader created for TESUnity.
    /// </summary>
    public class DefaultMaterialBuilder : AbstractMaterialBuilder
    {
        public DefaultMaterialBuilder(TextureManager textureManager) : base(textureManager) { }

        public override Material BuildMaterial(object key)
        {
            switch (key)
            {
                case null: return BuildMaterial();
                case MaterialProps p:
                    Material material;
                    if (p.AlphaBlended) material = BuildMaterialBlended(p.SrcBlendMode, p.DstBlendMode);
                    else if (p.AlphaTest) material = BuildMaterialTested(p.AlphaCutoff);
                    else material = BuildMaterial();
                    if (p.Textures.MainFilePath != null && material.HasProperty("_MainTex")) material.SetTexture("_MainTex", _textureManager.LoadTexture(p.Textures.MainFilePath));
                    if (p.Textures.DetailFilePath != null && material.HasProperty("_DetailTex")) material.SetTexture("_DetailTex", _textureManager.LoadTexture(p.Textures.DetailFilePath));
                    if (p.Textures.DarkFilePath != null && material.HasProperty("_DarkTex")) material.SetTexture("_DarkTex", _textureManager.LoadTexture(p.Textures.DarkFilePath));
                    if (p.Textures.GlossFilePath != null && material.HasProperty("_GlossTex")) material.SetTexture("_GlossTex", _textureManager.LoadTexture(p.Textures.GlossFilePath));
                    if (p.Textures.GlowFilePath != null && material.HasProperty("_Glowtex")) material.SetTexture("_Glowtex", _textureManager.LoadTexture(p.Textures.GlowFilePath));
                    if (p.Textures.BumpFilePath != null && material.HasProperty("_BumpTex")) material.SetTexture("_BumpTex", _textureManager.LoadTexture(p.Textures.BumpFilePath));
                    if (material.HasProperty("_Metallic")) material.SetFloat("_Metallic", 0f);
                    if (material.HasProperty("_Glossiness")) material.SetFloat("_Glossiness", 0f);
                    return material;
                case MaterialTerrain _: return BuildMaterialTerrain();
                default: throw new ArgumentOutOfRangeException(nameof(key));
            }
        }

        static Material BuildMaterial() => new Material(Shader.Find("TES Unity/Standard"));

        static Material BuildMaterialTerrain() => new Material(Shader.Find("Nature/Terrain/Diffuse"));

        static Material BuildMaterialBlended(ur.BlendMode sourceBlendMode, ur.BlendMode destinationBlendMode)
        {
            var material = new Material(Shader.Find("TES Unity/Alpha Blended"));
            material.SetInt("_SrcBlend", (int)sourceBlendMode);
            material.SetInt("_DstBlend", (int)destinationBlendMode);
            return material;
        }

        static Material BuildMaterialTested(float cutoff = 0.5f)
        {
            var material = new Material(Shader.Find("TES Unity/Alpha Tested"));
            material.SetFloat("_Cutoff", cutoff);
            return material;
        }
    }
}