using GameEstate.Core;
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
        public ZipPakFile(string filePath, string game) : base(filePath, game, new PakBinaryZip()) => Open();
    }
}