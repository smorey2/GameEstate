﻿using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Formats.Binary;

namespace GameEstate.Estates
{
    /// <summary>
    /// CyanidePakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.BinaryPakFile" />
    public class CyanidePakFile : BinaryPakFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UknPakFile" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="game">The game.</param>
        /// <param name="tag">The tag.</param>
        public CyanidePakFile(string filePath, string game, object tag = null) : base(filePath, game, new PakBinaryCyanide())
        {
            ExplorerItem = StandardExplorerItem.GetPakFilesAsync;
            ExplorerInfos.Add("_default", StandardExplorerInfo.GetDefaultAsync);
            ExplorerInfos.Add(".dds", StandardExplorerInfo.GetDdsAsync);
            Open();
        }
    }
}