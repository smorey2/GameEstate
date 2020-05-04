using GameEstate.Core;
using GameEstate.Formats.Binary;

namespace GameEstate.Estates
{
    /// <summary>
    /// UODatFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.BinaryDatFile" />
    public class UODatFile : BinaryDatFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TesDatFile" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="game">The game.</param>
        public UODatFile(string filePath, string game) : base(filePath, game, new DatBinaryUO())
        {
            UsePool = false;
            Open();
        }
    }
}