using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static GameEstate.EstateDebug;

namespace GameEstate.Graphics
{
    public class TextureManager
    {
        readonly EstatePakFile _pakFile;
        readonly Dictionary<string, Task<TextureInfo>> _textureFilePreloadTasks = new Dictionary<string, Task<TextureInfo>>();
        readonly Dictionary<string, Texture2D> _cachedTextures = new Dictionary<string, Texture2D>();

        public TextureManager(EstatePakFile pakFile) => _pakFile = pakFile;

        public Texture2D LoadTexture(string texturePath)
        {
            if (!_cachedTextures.TryGetValue(texturePath, out var texture))
            {
                // Load & cache the texture.
                var textureInfo = LoadTextureInfo(texturePath);
                texture = textureInfo != null ? textureInfo.ToUnity() : new Texture2D(1, 1);
                _cachedTextures[texturePath] = texture;
            }
            return texture;
        }

        public void PreloadTexture(string texturePath)
        {
            // If the texture has already been created we don't have to load the file again.
            if (_cachedTextures.ContainsKey(texturePath))
                return;
            // Start loading the texture file asynchronously if we haven't already started.
            if (!_textureFilePreloadTasks.TryGetValue(texturePath, out _))
                _textureFilePreloadTasks[texturePath] = _pakFile.LoadFileObjectAsync<TextureInfo>(texturePath);
        }

        TextureInfo LoadTextureInfo(string texturePath)
        {
            Assert(!_cachedTextures.ContainsKey(texturePath));
            PreloadTexture(texturePath);
            var textureInfo = _textureFilePreloadTasks[texturePath].Result;
            _textureFilePreloadTasks.Remove(texturePath);
            return textureInfo;
        }
    }
}