using GameEstate.Core;
using GameEstate.Formats.Tes;
using GameEstate.Graphics;
using System;
using UnityEngine;

namespace GameEstate
{
    public class UnityPakFile : IDisposable
    {
        readonly AbstractPakFile _source;
        readonly TextureManager _textureManager;
        readonly MaterialManager _materialManager;
        readonly NifManager _nifManager;

        public UnityPakFile(AbstractPakFile source)
        {
            _source = source;
            _textureManager = new TextureManager(source);
            _materialManager = new MaterialManager(_textureManager);
            _nifManager = new NifManager(source, _materialManager, 0);
        }
        public void Dispose() => _source.Dispose();

        public MaterialManager MaterialManager => _materialManager;
        public Texture2D LoadTexture(string texturePath) => _textureManager.LoadTexture(texturePath);
        public void PreloadTexture(string texturePath) => _textureManager.PreloadTexture(texturePath);
        public GameObject CreateObject(string filePath) => _nifManager.CreateObject(filePath);
        public void PreloadObject(string filePath) => _nifManager.PreloadObject(filePath);
    }
}