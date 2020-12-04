using GameEstate.Graphics.MaterialBuilders;
using System.Collections.Generic;

namespace GameEstate.Graphics
{
    /// <summary>
    /// Manages loading and instantiation of materials.
    /// </summary>
    public class MaterialManager<Material, Texture>
    {
        readonly AbstractMaterialBuilder<Material, Texture> _builder;
        readonly Dictionary<object, Material> _cachedMaterials = new Dictionary<object, Material>();

        public TextureManager<Texture> TextureManager { get; }

        public MaterialManager(TextureManager<Texture> textureManager, AbstractMaterialBuilder<Material, Texture> builder)
        {
            TextureManager = textureManager;
            _builder = builder;
        }

        public Material LoadMaterial(object key, out IDictionary<string, object> data)
        {
            data = null;
            return _cachedMaterials.TryGetValue(key, out var material)
                ? material
                : _cachedMaterials[key] = _builder.BuildMaterial(key);
        }
    }
}