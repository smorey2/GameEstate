using GameEstate.Graphics.TextureBuilders;
using System.Collections.Generic;
using System.Threading.Tasks;
using static GameEstate.EstateDebug;

namespace GameEstate.Graphics
{
    public class TextureManager<Texture>
    {
        readonly EstatePakFile _pakFile;
        readonly AbstractTextureBuilder<Texture> _builder;
        readonly Dictionary<string, Texture> _cachedTextures = new Dictionary<string, Texture>();
        readonly Dictionary<string, Task<TextureInfo>> _preloadTasks = new Dictionary<string, Task<TextureInfo>>();

        public TextureManager(EstatePakFile pakFile, AbstractTextureBuilder<Texture> builder)
        {
            _pakFile = pakFile;
            _builder = builder;
        }

        public Texture ErrorTexture => _builder.ErrorTexture;

        public Texture LoadTexture(string path, out IDictionary<string, object> data)
        {
            data = null;
            if (_cachedTextures.TryGetValue(path, out var texture))
                return texture;
            // Load & cache the texture.
            var textureInfo = LoadTextureInfo(path);
            texture = textureInfo != null ? _builder.BuildTexture(textureInfo) : _builder.ErrorTexture;
            _cachedTextures[path] = texture;
            return texture;
        }

        public void PreloadTexture(string path)
        {
            if (_cachedTextures.ContainsKey(path))
                return;
            // Start loading the texture file asynchronously if we haven't already started.
            if (!_preloadTasks.ContainsKey(path))
                _preloadTasks[path] = _pakFile.LoadFileObjectAsync<TextureInfo>(path);
        }

        TextureInfo LoadTextureInfo(string path)
        {
            Assert(!_cachedTextures.ContainsKey(path));
            PreloadTexture(path);
            var textureInfo = _preloadTasks[path].Result;
            _preloadTasks.Remove(path);
            return textureInfo;
        }

        public Texture BuildSolidTexture(params float[] rgba) => _builder.BuildSolidTexture(rgba);

        public Texture BuildNormalMap(Texture source, float strength) => _builder.BuildNormalMap(source, strength);
    }
}