using GameEstate.Core;

namespace GameEstate.Estates
{
    /// <summary>
    /// U9PakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.BinaryPakFile" />
    public class U9PakFile : BinaryPakFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="U9PakFile" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="game">The game.</param>
        public U9PakFile(string filePath, string game) : base(filePath, game, null) => Open();
    }
}