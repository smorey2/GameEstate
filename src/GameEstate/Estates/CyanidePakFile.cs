﻿using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Formats._Packages;

namespace GameEstate.Estates
{
    /// <summary>
    /// CyanidePakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.BinaryPakFile" />
    public class CyanidePakFile : BinaryPakManyFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UknPakFile" /> class.
        /// </summary>
        /// <param name="estate">The estate.</param>
        /// <param name="game">The game.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="tag">The tag.</param>
        public CyanidePakFile(Estate estate, string game, string filePath, object tag = null) : base(estate, game, filePath, PakBinaryCyanide.Instance, tag)
        {
            ExplorerItems = StandardExplorerItem.GetPakFilesAsync;
            Open();
        }
    }
}