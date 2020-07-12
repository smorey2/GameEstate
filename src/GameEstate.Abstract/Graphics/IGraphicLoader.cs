using System.Threading.Tasks;

namespace GameEstate.Graphics
{
    /// <summary>
    /// IGraphicLoader
    /// </summary>
    public interface IGraphicLoader
    {
        /// <summary>
        /// Loads the texture information asynchronous.
        /// </summary>
        /// <param name="texturePath">The texture path.</param>
        /// <returns></returns>
        public Task<TextureInfo> LoadTextureInfoAsync(string texturePath);

        /// <summary>
        /// Loads the object information asynchronous.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public Task<object> LoadObjectInfoAsync(string filePath);
    }
}