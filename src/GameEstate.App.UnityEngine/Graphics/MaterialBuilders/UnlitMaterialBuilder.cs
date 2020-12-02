﻿using System;
using UnityEngine;
using ur = UnityEngine.Rendering;

namespace GameEstate.Graphics.MaterialBuilders
{
    /// <summary>
    /// A material that uses the Unlit Shader.
    /// </summary>
    public class UnliteMaterial : AbstractMaterialBuilder
    {
        public UnliteMaterial(TextureManager textureManager) : base(textureManager) { }

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
                    if (p.Textures.MainFilePath != null) material.mainTexture = _textureManager.LoadTexture(p.Textures.MainFilePath);
                    return material;
                case MaterialTerrain _: return BuildMaterialTerrain();
                default: throw new ArgumentOutOfRangeException(nameof(key));
            }
        }

        static Material BuildMaterial() => new Material(Shader.Find("Unlit/Texture"));

        static Material BuildMaterialTerrain() => new Material(Shader.Find("Nature/Terrain/Diffuse"));

        static Material BuildMaterialBlended(ur.BlendMode sourceBlendMode, ur.BlendMode destinationBlendMode)
        {
            var material = BuildMaterialTested();
            material.SetInt("_SrcBlend", (int)sourceBlendMode);
            material.SetInt("_DstBlend", (int)destinationBlendMode);
            return material;
        }

        static Material BuildMaterialTested(float cutoff = 0.5f)
        {
            var material = new Material(Shader.Find("Unlit/Transparent Cutout"));
            material.SetFloat("_AlphaCutoff", cutoff);
            return material;
        }
    }
}