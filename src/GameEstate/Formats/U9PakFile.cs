using GameEstate.Core;

namespace GameEstate.Formats
{
    /// <summary>
    /// U9PakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.CorePakFile" />
    public class U9PakFile : CorePakFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="U9PakFile" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="game">The game.</param>
        public U9PakFile(string filePath, string game) : base(filePath, game, null) => Open();
    }
}