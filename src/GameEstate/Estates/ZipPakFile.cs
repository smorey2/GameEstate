using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Formats.Binary;

namespace GameEstate.Estates
{
    /// <summary>
    /// ZipPakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.BinaryPakFile" />
    public class ZipPakFile : BinaryPakFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZipPakFile" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="game">The game.</param>
        /// <param name="tag">The tag.</param>
        public ZipPakFile(string filePath, string game, object tag = null) : base(filePath, game, PakBinaryZip.Instance, tag)
        {
            ExplorerItem = StandardExplorerItem.GetPakFilesAsync;
            ExplorerInfos.Add("_default", StandardExplorerInfo.GetDefaultAsync);
            ExplorerInfos.Add(".dds", StandardExplorerInfo.GetDdsAsync);
            Open();
        }
    }
}