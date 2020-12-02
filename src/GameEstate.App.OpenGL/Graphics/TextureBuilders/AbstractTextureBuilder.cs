namespace GameEstate.Graphics.TextureBuilders
{
    public abstract class AbstractTextureBuilder
    {
        public abstract int ErrorTexture { get; }
        public abstract int BuildTexture(TextureInfo textureInfo);
    }
}
