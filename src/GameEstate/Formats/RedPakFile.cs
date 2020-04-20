using GameEstate.Core;
using GameEstate.Formats.Binary;

namespace GameEstate.Formats
{
    /// <summary>
    /// RedPakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.CorePakFile" />
    public class RedPakFile : CorePakFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedPakFile" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="game">The game.</param>
        /// <param name="tag">The tag.</param>
        public RedPakFile(string filePath, string game, object tag = null) : base(filePath, game, new PakFormatRed(tag)) => Open();
    }
}