using GameEstate.Formats.Tes;
using GameEstate.Graphics;
using System;

namespace GameEstate
{
    public interface IOpenGLGraphic : IEstateGraphic
    {
        //MaterialManager MaterialManager { get; }
        //Texture2D LoadTexture(string texturePath);
        //GameObject CreateObject(string filePath);
    }

    public class OpenGLGraphic : IOpenGLGraphic
    {
        readonly EstatePakFile _source;
        readonly TextureManager _textureManager;
        readonly MaterialManager _materialManager;
        //readonly NifManager _nifManager;

        public OpenGLGraphic(EstatePakFile source)
        {
            _source = source;
            _textureManager = new TextureManager(source);
            _materialManager = new MaterialManager(_textureManager);
            //_nifManager = new NifManager(source, _materialManager, 0);
        }

        //public MaterialManager MaterialManager => _materialManager;
        //public Texture2D LoadTexture(string texturePath) => _textureManager.LoadTexture(texturePath);
        public void PreloadTexture(string texturePath) => _textureManager.PreloadTexture(texturePath);
        //public GameObject CreateObject(string filePath) => _nifManager.CreateObject(filePath);
        public void PreloadObject(string filePath) => throw new NotSupportedException(); // _nifManager.PreloadObject(filePath);
    }
}