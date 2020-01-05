using GameEstate.Core;
using GameEstate.Formats;
using System;

namespace GameEstate.Red
{
    /// <summary>
    /// RedEstate
    /// </summary>
    /// <seealso cref="GameEstate.Core.CoreEstate" />
    public class RedEstate : CoreEstate
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public override string Name => "REDengine";

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public override string Description => @"REDengine file formats including the Witcher Series";

        /// <summary>
        /// Gets the type of the game.
        /// </summary>
        /// <value>
        /// The type of the game.
        /// </value>
        public override Type GameType => typeof(RedGame);

        /// <summary>
        /// Gets the file manager.
        /// </summary>
        /// <value>
        /// The file manager.
        /// </value>
        public override CoreFileManager FileManager => new RedFileManager();

        /// <summary>
        /// Opens the pak file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public override CorePakFile OpenPakFile(string filePath) => new RedPakFile(filePath);
    }
}