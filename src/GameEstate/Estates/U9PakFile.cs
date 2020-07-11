using GameEstate.Core;
using GameEstate.Explorer;

namespace GameEstate.Estates
{
    /// <summary>
    /// U9PakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.BinaryPakFile" />
    public class U9PakFile : BinaryPakFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="U9PakFile" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="game">The game.</param>
        /// <param name="tag">The tag.</param>
        public U9PakFile(string filePath, string game, object tag = null) : base(filePath, game, null)
        {
            ExplorerItem = StandardExplorerItem.GetPakFilesAsync;
            ExplorerInfos.Add(".dds", StandardExplorerInfo.GetDdsAsync);
            Open();
        }
    }
}