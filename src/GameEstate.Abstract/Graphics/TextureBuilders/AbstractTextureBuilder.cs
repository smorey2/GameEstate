namespace GameEstate.Graphics.TextureBuilders
{
    public abstract class AbstractTextureBuilder<Texture>
    {
        public abstract Texture ErrorTexture { get; }
        public abstract Texture BuildTexture(TextureInfo textureInfo);
        public abstract Texture BuildSolidTexture(float[] rgba);
        public abstract Texture BuildNormalMap(Texture source, float strength);
    }
}
