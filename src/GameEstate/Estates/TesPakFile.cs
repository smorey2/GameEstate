using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Formats.Binary;
using GameEstate.Graphics;
using System.IO;
using static GameEstate.EstateDebug;

namespace GameEstate.Estates
{
    /// <summary>
    /// TesPakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.BinaryPakFile" />
    public class TesPakFile : BinaryPakManyFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TesPakFile" /> class.
        /// </summary>
        /// <param name="estate">The estate.</param>
        /// <param name="game">The game.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="tag">The tag.</param>
        public TesPakFile(Estate estate, string game, string filePath, object tag = null) : base(estate, game, filePath, Path.GetExtension(filePath) != ".esm" ? PakBinaryTes.Instance : PakBinaryTesEsm.Instance, tag)
        {
            ExplorerItems = StandardExplorerItem.GetPakFilesAsync;
            Open();
        }

        #region Loader

        //public Task<TextureInfo> LoadTextureInfoAsync(string texturePath)
        //{
        //    var filePath = FindTexture(texturePath);
        //    return filePath != null
        //        ? Task.Run(async () =>
        //        {
        //            var fileData = await LoadFileDataAsync(filePath);
        //            var fileExtension = Path.GetExtension(filePath);
        //            if (fileExtension.ToLowerInvariant() == ".dds") return new TextureInfo().ReadDds(fileData);
        //            else throw new NotSupportedException($"Unsupported texture type: {fileExtension}");
        //        })
        //        : Task.FromResult<TextureInfo>(null);
        //}

        /// <summary>
        /// Finds the actual path of a texture.
        /// </summary>
        public override string FindTexture(string texturePath)
        {
            var textureName = Path.GetFileNameWithoutExtension(texturePath);
            var textureNameInTexturesDir = $"textures/{textureName}";
            var filePath = $"{textureNameInTexturesDir}.dds";
            if (Contains(filePath))
                return filePath;
            //filePath = $"{textureNameInTexturesDir}.tga";
            //if (Contains(filePath))
            //    return filePath;
            var texturePathWithoutExtension = $"{Path.GetDirectoryName(texturePath)}/{textureName}";
            filePath = $"{texturePathWithoutExtension}.dds";
            if (Contains(filePath))
                return filePath;
            //filePath = $"{texturePathWithoutExtension}.tga";
            //if (Contains(filePath))
            //    return filePath;
            Log($"Could not find file '{texturePath}' in a BSA file.");
            return null;
        }

        #endregion
    }
}