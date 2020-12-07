namespace GameEstate.Graphics.TextureBuilders
{
    public abstract class AbstractTextureBuilder<Texture>
    {
        public abstract Texture DefaultTexture { get; }
        public abstract Texture BuildTexture(ITextureInfo info);
        public abstract Texture BuildSolidTexture(int width, int height, float[] rgba);
        public abstract Texture BuildNormalMap(Texture source, float strength);
    }
}
