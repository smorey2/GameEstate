namespace GameEstate
{
    /// <summary>
    /// IEstateGraphic
    /// </summary>
    public interface IEstateGraphic
    {
        void PreloadTexture(string texturePath);
        void PreloadObject(string filePath);
    }
}