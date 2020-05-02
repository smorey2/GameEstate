using GameEstate.Core;

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
        public UOPakFile(string filePath, string game) : base(filePath, game, null) => Open();
    }
}