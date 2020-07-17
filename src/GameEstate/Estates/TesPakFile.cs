﻿using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Formats.Binary;
using GameEstate.Formats.Nif;
using GameEstate.Graphics;
using System;
using System.IO;
using System.Threading.Tasks;
using static GameEstate.EstateDebug;

namespace GameEstate.Estates
{
    /// <summary>
    /// TesPakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.BinaryPakFile" />
    public class TesPakFile : BinaryPakFile, IGraphicLoader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TesPakFile" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="game">The game.</param>
        /// <param name="tag">The tag.</param>
        public TesPakFile(string filePath, string game, object tag = null) : base(filePath, game, new PakBinaryTes())
        {
            ExplorerItem = StandardExplorerItem.GetPakFilesAsync;
            ExplorerInfos.Add(".nif", StandardExplorerInfo.GetNifAsync);
            ExplorerInfos.Add(".dds", StandardExplorerInfo.GetDdsAsync);
            Open();
        }

        #region Loader

        public Task<TextureInfo> LoadTextureInfoAsync(string texturePath)
        {
            var filePath = FindTexture(texturePath);
            return filePath != null
                ? Task.Run(async () =>
                {
                    var fileData = await LoadFileDataAsync(filePath);
                    var fileExtension = Path.GetExtension(filePath);
                    if (fileExtension.ToLowerInvariant() == ".dds") return new TextureInfo().LoadDdsTexture(fileData);
                    else throw new NotSupportedException($"Unsupported texture type: {fileExtension}");
                })
                : Task.FromResult<TextureInfo>(null);
        }

        public Task<object> LoadObjectInfoAsync(string filePath) => Task.Run(async () =>
       {
           var fileData = await LoadFileDataAsync(filePath);
           var file = new NiFile(Path.GetFileNameWithoutExtension(filePath));
           file.Deserialize(new BinaryReader(new MemoryStream(fileData)));
           return (object)file;
       });

        /// <summary>
        /// Finds the actual path of a texture.
        /// </summary>
        string FindTexture(string texturePath)
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