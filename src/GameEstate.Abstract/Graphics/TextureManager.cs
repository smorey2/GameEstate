﻿using GameEstate.Graphics.TextureBuilders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static GameEstate.EstateDebug;

namespace GameEstate.Graphics
{
    public class TextureManager<Texture>
    {
        readonly EstatePakFile _pakFile;
        readonly AbstractTextureBuilder<Texture> _builder;
        readonly Dictionary<object, (Texture texture, IDictionary<string, object> data)> _cachedTextures = new Dictionary<object, (Texture texture, IDictionary<string, object> data)>();
        readonly Dictionary<object, Task<ITextureInfo>> _preloadTasks = new Dictionary<object, Task<ITextureInfo>>();

        public TextureManager(EstatePakFile pakFile, AbstractTextureBuilder<Texture> builder)
        {
            _pakFile = pakFile;
            _builder = builder;
        }

        public Texture DefaultTexture => _builder.DefaultTexture;

        public Texture LoadTexture(object key, out IDictionary<string, object> data)
        {
            if (_cachedTextures.TryGetValue(key, out var cache))
            {
                data = cache.data;
                return cache.texture;
            }
            // Load & cache the texture.
            var info = key is ITextureInfo z ? z : LoadTextureInfo(key);
            var texture = info != null ? _builder.BuildTexture(info) : _builder.DefaultTexture;
            data = info?.Data;
            _cachedTextures[key] = (texture, data);
            return texture;
        }

        public void PreloadTexture(string path)
        {
            if (_cachedTextures.ContainsKey(path))
                return;
            // Start loading the texture file asynchronously if we haven't already started.
            if (!_preloadTasks.ContainsKey(path))
                _preloadTasks[path] = _pakFile.LoadFileObjectAsync<ITextureInfo>(path);
        }

        ITextureInfo LoadTextureInfo(object key)
        {
            Assert(!_cachedTextures.ContainsKey(key));
            switch (key)
            {
                case string path:
                    PreloadTexture(path);
                    var info = _preloadTasks[key].Result;
                    _preloadTasks.Remove(key);
                    return info;
                default: throw new ArgumentOutOfRangeException(nameof(key), $"{key}");
            }
        }

        public Texture BuildSolidTexture(int width, int height, params float[] rgba) => _builder.BuildSolidTexture(width, height, rgba);

        public Texture BuildNormalMap(Texture source, float strength) => _builder.BuildNormalMap(source, strength);
    }
}