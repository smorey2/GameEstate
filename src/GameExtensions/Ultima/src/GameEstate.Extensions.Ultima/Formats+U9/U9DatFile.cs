using GameEstate.Core;
using GameEstate.Formats.Binary;

namespace GameEstate.Formats
{
    /// <summary>
    /// U9DatFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.CoreDatFile" />
    public class U9DatFile : CoreDatFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="U9DatFile" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="game">The game.</param>
        public U9DatFile(string filePath, string game) : base(filePath, game, new DatFormatU9()) => Open();
    }
}