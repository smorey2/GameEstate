using System;
using GameEstate.Core;

namespace GameEstate.Cry
{
    /// <summary>
    /// CryEstate
    /// </summary>
    /// <seealso cref="GameEstate.Core.CoreEstate" />
    public class CryEstate : CoreEstate
    {
        /// <summary>
        /// Gets the type of the game.
        /// </summary>
        /// <value>
        /// The type of the game.
        /// </value>
        public override Type GameType => typeof(CryGame);

        /// <summary>
        /// Gets the file manager.
        /// </summary>
        /// <value>
        /// The file manager.
        /// </value>
        public override CoreFileManager FileManager => new CryFileManager();

        /// <summary>
        /// Opens the pak file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public override CorePakFile OpenPakFile(string filePath) => new CryPakFile(filePath);
    }
}