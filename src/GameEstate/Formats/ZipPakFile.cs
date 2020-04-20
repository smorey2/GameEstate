using GameEstate.Core;
using GameEstate.Formats.Binary;

namespace GameEstate.Formats
{
    /// <summary>
    /// ZipPakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.CorePakFile" />
    public class ZipPakFile : CorePakFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZipPakFile" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="game">The game.</param>
        public ZipPakFile(string filePath, string game) : base(filePath, game, new PakFormatZip()) => Open();
    }
}