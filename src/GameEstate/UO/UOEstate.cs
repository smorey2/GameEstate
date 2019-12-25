using GameEstate.Core;
using System;

namespace GameEstate.UO
{
    /// <summary>
    /// UOEstate
    /// </summary>
    /// <seealso cref="GameEstate.Core.CoreEstate" />
    public class UOEstate : CoreEstate
    {
        /// <summary>
        /// Gets the type of the game.
        /// </summary>
        /// <value>
        /// The type of the game.
        /// </value>
        public override Type GameType => typeof(UOGame);

        /// <summary>
        /// Gets the file manager.
        /// </summary>
        /// <value>
        /// The file manager.
        /// </value>
        public override CoreFileManager FileManager => new UOFileManager();

        /// <summary>
        /// Opens the pak file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public override CorePakFile OpenPakFile(string filePath) => new UOPakFile(filePath);
    }
}