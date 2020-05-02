using GameEstate.Core;
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
        public TesPakFile(string filePath, string game) : base(filePath, game, new PakBinaryTes()) => Open();
    }
}