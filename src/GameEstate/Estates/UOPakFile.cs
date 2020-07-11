using GameEstate.Core;
using GameEstate.Explorer;

namespace GameEstate.Estates
{
    /// <summary>
    /// UOPakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.BinaryPakFile" />
    public class UOPakFile : BinaryPakFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UOPakFile" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="game">The game.</param>
        /// <param name="tag">The tag.</param>
        public UOPakFile(string filePath, string game, object tag = null) : base(filePath, game, null)
        {
            ExplorerItem = StandardExplorerItem.GetPakFilesAsync;
            ExplorerInfos.Add(".dds", StandardExplorerInfo.GetDdsAsync);
            Open();
        }
    }
}