using GameEstate.Core;

namespace GameEstate.Formats
{
    /// <summary>
    /// UOPakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.CorePakFile" />
    public class UOPakFile : CorePakFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UOPakFile" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="game">The game.</param>
        public UOPakFile(string filePath, string game) : base(filePath, game, null) => Open();
    }
}