using GameEstate.Graphics.MaterialBuilders;
using GameEstate.Graphics.OpenGL;
using System.Collections.Generic;

namespace GameEstate.Graphics
{
    /// <summary>
    /// Manages loading and instantiation of materials.
    /// </summary>
    public class MaterialManager
    {
        readonly AbstractMaterialBuilder _builder;
        readonly Dictionary<object, Material> _existingMaterials = new Dictionary<object, Material>();

        public TextureManager TextureManager { get; }

        public MaterialManager(TextureManager textureManager)
        {
            TextureManager = textureManager;
            _builder = new ValveMaterialBuilder(textureManager);
        }

        public Material GetMaterial(object key) => _existingMaterials.TryGetValue(key, out var material)
            ? material
            : _existingMaterials[key] = _builder.BuildMaterial(key);
    }
}