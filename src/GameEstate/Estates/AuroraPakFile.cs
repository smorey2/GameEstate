using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Formats._Packages;
using System;

namespace GameEstate.Estates
{
    /// <summary>
    /// AuroraPakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.BinaryPakFile" />
    public class AuroraPakFile : BinaryPakManyFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuroraPakFile" /> class.
        /// </summary>
        /// <param name="estate">The estate.</param>
        /// <param name="game">The game.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="tag">The tag.</param>
        public AuroraPakFile(Estate estate, string game, string filePath, object tag = null) : base(estate, game, filePath, filePath.EndsWith(".zip", StringComparison.OrdinalIgnoreCase) ? PakBinaryZip2.Instance : PakBinaryAurora.Instance, tag)
        {
            ExplorerItems = StandardExplorerItem.GetPakFilesAsync;
            Open();
        }
    }
}