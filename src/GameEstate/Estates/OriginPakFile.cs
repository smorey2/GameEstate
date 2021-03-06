﻿using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Formats._Packages;

namespace GameEstate.Estates
{
    /// <summary>
    /// OriginPakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.BinaryPakFile" />
    public class OriginPakFile : BinaryPakManyFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OriginPakFile" /> class.
        /// </summary>
        /// <param name="estate">The estate.</param>
        /// <param name="game">The game.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="tag">The tag.</param>
        public OriginPakFile(Estate estate, string game, string filePath, object tag = null) : base(estate, game, filePath, game == "UltimaOnline" ? PakBinaryOriginUO.Instance : PakBinaryOriginU9.Instance, tag)
        {
            ExplorerItems = StandardExplorerItem.GetPakFilesAsync;
            Open();
        }
    }
}