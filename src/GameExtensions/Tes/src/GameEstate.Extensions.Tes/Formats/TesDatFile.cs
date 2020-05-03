using GameEstate.Core;
using GameEstate.Formats.Binary;

namespace GameEstate.Formats
{
    /// <summary>
    /// TesDatFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.CoreDatFile" />
    public class TesDatFile : CoreDatFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TesDatFile" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="game">The game.</param>
        public TesDatFile(string filePath, string game) : base(filePath, game, new DatFormatTes()) => Open();
    }
}