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
        static ACPakFile()
        {
            ExplorerItemAsync = StandardExplorerItem.GetPakFilesAsync;
            ExplorerInfoAsyncs.Add(".dds", StandardExplorerInfo.GetDdsInfo);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ACPakFile" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="game">The game.</param>
        public ACPakFile(string filePath, string game) : base(filePath, game, new PakBinaryAC()) => Open();
    }
}