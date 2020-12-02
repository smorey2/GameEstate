using GameEstate.Graphics.MaterialBuilders;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace GameEstate.Graphics
{
    /// <summary>
    /// MatTestMode
    /// </summary>
    public enum MatTestMode { Always, Less, LEqual, Equal, GEqual, Greater, NotEqual, Never }

    /// <summary>
    /// MaterialType
    /// </summary>
    public enum MaterialType { None, Default, Standard, BumpedDiffuse, Unlit }

    public struct MaterialTextures
    {
        public string MainFilePath;
        public string DarkFilePath;
        public string DetailFilePath;
        public string GlossFilePath;
        public string GlowFilePath;
        public string BumpFilePath;
    }

    public struct MaterialProps
    {
        public MaterialTextures Textures;
        public bool AlphaBlended;
        public BlendMode SrcBlendMode;
        public BlendMode DstBlendMode;
        public bool AlphaTest;
        public float AlphaCutoff;
        public bool ZWrite;
    }

    public struct MaterialTerrain { }

    public struct MaterialBlended
    {
        public BlendMode SrcBlendMode;
        public BlendMode DstBlendMode;
    }

    public struct MaterialTested
    {
        public float Cutoff;
    }

    /// <summary>
    /// Manages loading and instantiation of materials.
    /// </summary>
    public class MaterialManager
    {
        readonly AbstractMaterialBuilder _material;
        readonly Dictionary<object, Material> _existingMaterials = new Dictionary<object, Material>();

        public TextureManager TextureManager { get; }

        public MaterialManager(TextureManager textureManager)
        {
            TextureManager = textureManager;
            _material = new BumpedDiffuseMaterialBuilder(textureManager);
            //switch (MaterialType.Default)
            //{
            //    case MaterialType.None: _material = null; break;
            //    case MaterialType.Default: _material = new DefaultMaterial(textureManager); break;
            //    case MaterialType.Standard: _material = new StandardMaterial(textureManager); break;
            //    case MaterialType.Unlit: _material = new UnliteMaterial(textureManager); break;
            //    default: _material = new BumpedDiffuseMaterial(textureManager); break;
            //}
        }

        public Material GetMaterial(object key) => _existingMaterials.TryGetValue(key, out var material)
            ? material
            : _existingMaterials[key] = _material.BuildMaterial(key);
    }
}