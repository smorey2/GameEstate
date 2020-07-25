using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Formats.Binary;

namespace GameEstate.Estates
{
    /// <summary>
    /// ACPakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.BinaryPakFile" />
    public class ACPakFile : BinaryPakFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ACPakFile" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="game">The game.</param>
        /// <param name="tag">The tag.</param>
        public ACPakFile(string filePath, string game, object tag = null) : base(filePath, game, PakBinaryAC.Instance, tag)
        {
            ExplorerItem = StandardExplorerItem.GetPakFilesAsync;
            ExplorerInfos.Add("_default", StandardExplorerInfo.GetDefaultAsync);
            ExplorerInfos.Add(".dds", StandardExplorerInfo.GetDdsAsync);
            Open();
        }
    }
}