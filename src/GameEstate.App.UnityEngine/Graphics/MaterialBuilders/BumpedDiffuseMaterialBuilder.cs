using System;
using UnityEngine;
using ur = UnityEngine.Rendering;

namespace GameEstate.Graphics.MaterialBuilders
{
    /// <summary>
    /// A material that uses the legacy Bumped Diffuse Shader.
    /// </summary>
    public class BumpedDiffuseMaterialBuilder : AbstractMaterialBuilder
    {
        public BumpedDiffuseMaterialBuilder(TextureManager textureManager) : base(textureManager) { }

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
                    if (p.Textures.MainFilePath != null)
                    {
                        material.mainTexture = _textureManager.LoadTexture(p.Textures.MainFilePath);
                        if (NormalGeneratorIntensity != null) material.SetTexture("_BumpMap", GenerateNormalMap((Texture2D)material.mainTexture, NormalGeneratorIntensity.Value));
                    }
                    if (p.Textures.BumpFilePath != null) material.SetTexture("_BumpMap", _textureManager.LoadTexture(p.Textures.BumpFilePath));
                    return material;
                case MaterialTerrain _: return BuildMaterialTerrain();
                default: throw new ArgumentOutOfRangeException(nameof(key));
            }
        }

        static Material BuildMaterial() => new Material(Shader.Find("Legacy Shaders/Bumped Diffuse"));

        static Material BuildMaterialTerrain() => new Material(Shader.Find("Nature/Terrain/Diffuse"));

        static Material BuildMaterialBlended(ur.BlendMode srcBlendMode, ur.BlendMode dstBlendMode)
        {
            var material = new Material(Shader.Find("Legacy Shaders/Transparent/Cutout/Bumped Diffuse"));
            material.SetInt("_SrcBlend", (int)srcBlendMode);
            material.SetInt("_DstBlend", (int)dstBlendMode);
            return material;
        }

        static Material BuildMaterialTested(float cutoff = 0.5f)
        {
            var material = new Material(Shader.Find("Legacy Shaders/Transparent/Cutout/Bumped Diffuse"));
            material.SetFloat("_AlphaCutoff", cutoff);
            return material;
        }
    }
}