using GameEstate.Core;
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
        /// <summary>
        /// Initializes a new instance of the <see cref="RsiPakFile" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="game">The game.</param>
        /// <param name="tag">The tag.</param>
        public RsiPakFile(string filePath, string game, object tag = null) : base(filePath, game, new PakBinaryP4k())
        {
            ExplorerItem = StandardExplorerItem.GetPakFilesAsync;
            ExplorerInfos.Add(".dds", StandardExplorerInfo.GetDdsAsync);
            Open();
        }
    }
}