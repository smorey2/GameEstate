using GameEstate.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static GameEstate.EstateDebug;

namespace GameEstate.Graphics
{
    public class TextureManager
    {
        readonly IGraphicLoader _pakFile;
        readonly Dictionary<string, Task<TextureInfo>> _textureFilePreloadTasks = new Dictionary<string, Task<TextureInfo>>();
        readonly Dictionary<string, Texture2D> _cachedTextures = new Dictionary<string, Texture2D>();

        public TextureManager(AbstractPakFile pakFile) => _pakFile = pakFile as IGraphicLoader;

        public Texture2D LoadTexture(string texturePath)
        {
            if (!_cachedTextures.TryGetValue(texturePath, out var texture))
            {
                // Load & cache the texture.
                var textureInfo = LoadTextureInfo(texturePath);
                texture = textureInfo != null ? textureInfo.ToUnity() : new Texture2D(1, 1);
                //if (method == 1) TextureUtils.FlipTexture2DVertically(texture);
                //if (method == 2) TextureUtils.RotateTexture2D(texture);
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
                _textureFilePreloadTasks[texturePath] = _pakFile.LoadTextureInfoAsync(texturePath);
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