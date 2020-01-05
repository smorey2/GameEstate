using System;
using GameEstate.Core;

// http://aluigi.altervista.org/quickbms.htm
namespace GameEstate.Bms
{
    /// <summary>
    /// BmsEstate
    /// </summary>
    /// <seealso cref="GameEstate.Core.CoreEstate" />
    public class BmsEstate : CoreEstate
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public override string Name => "QuickBMS";

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public override string Description => @"QuickBMS";

        /// <summary>
        /// Gets the type of the game.
        /// </summary>
        /// <value>
        /// The type of the game.
        /// </value>
        public override Type GameType => typeof(BmsGame);

        /// <summary>
        /// Gets the file manager.
        /// </summary>
        /// <value>
        /// The file manager.
        /// </value>
        public override CoreFileManager FileManager => new BmsFileManager();

        /// <summary>
        /// Opens the pak file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public override CorePakFile OpenPakFile(string filePath) => new BmsPakFile(filePath);
    }
}