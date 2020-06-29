﻿using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Formats.Binary;

namespace GameEstate.Estates
{
    /// <summary>
    /// RsiPakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.BinaryPakFile" />
    public class RsiPakFile : BinaryPakFile
    {
        static RsiPakFile()
        {
            ExplorerItemAsync = StandardExplorerItem.GetPakFilesAsync;
            ExplorerInfoAsyncs.Add(".dds", StandardExplorerInfo.GetDdsInfo);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RsiPakFile" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="game">The game.</param>
        public RsiPakFile(string filePath, string game) : base(filePath, game, new PakBinaryP4k()) => Open();
    }
}