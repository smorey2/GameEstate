using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Formats.Binary;

namespace GameEstate.Estates
{
    /// <summary>
    /// TesPakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.BinaryPakFile" />
    public class TesPakFile : BinaryPakFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TesPakFile" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="game">The game.</param>
        /// <param name="tag">The tag.</param>
        public TesPakFile(string filePath, string game, object tag = null) : base(filePath, game, new PakBinaryTes())
        {
            ExplorerItem = StandardExplorerItem.GetPakFilesAsync;
            ExplorerInfos.Add(".dds", StandardExplorerInfo.GetDdsAsync);
            Open();
        }
    }
}