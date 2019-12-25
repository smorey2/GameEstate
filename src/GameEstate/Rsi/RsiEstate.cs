using GameEstate.Core;
using System;

namespace GameEstate.Rsi
{
    /// <summary>
    /// RsiEstate
    /// </summary>
    /// <seealso cref="GameEstate.Core.CoreEstate" />
    public class RsiEstate : CoreEstate
    {
        /// <summary>
        /// Gets the type of the game.
        /// </summary>
        /// <value>
        /// The type of the game.
        /// </value>
        public override Type GameType => typeof(RsiGame);

        /// <summary>
        /// Gets the file manager.
        /// </summary>
        /// <value>
        /// The file manager.
        /// </value>
        public override CoreFileManager FileManager => new RsiFileManager();

        /// <summary>
        /// Opens the pak file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public override CorePakFile OpenPakFile(string filePath) => new RsiPakFile(filePath);
    }
}