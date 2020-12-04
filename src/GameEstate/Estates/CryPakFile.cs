﻿using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Formats._Packages;

namespace GameEstate.Estates
{
    /// <summary>
    /// CryPakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.BinaryPakFile" />
    public class CryPakFile : BinaryPakManyFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CryPakFile" /> class.
        /// </summary>
        /// <param name="estate">The estate.</param>
        /// <param name="game">The game.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="tag">The tag.</param>
        public CryPakFile(Estate estate, string game, string filePath, object tag = null) : base(estate, game, filePath, PakBinaryZip2.Instance, tag)
        {
            ExplorerItems = StandardExplorerItem.GetPakFilesAsync;
            Open();
        }
    }
}