using GameEstate.Core;
using GameEstate.Formats;
using System;

namespace GameEstate.U9
{
    /// <summary>
    /// U9Estate
    /// </summary>
    /// <seealso cref="GameEstate.Core.CoreEstate" />
    public class U9Estate : CoreEstate
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public override string Name => "Ultima IX";

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public override string Description => @"Ultima IX file formats";

        /// <summary>
        /// Gets the type of the game.
        /// </summary>
        /// <value>
        /// The type of the game.
        /// </value>
        public override Type GameType => typeof(U9Game);

        /// <summary>
        /// Gets the file manager.
        /// </summary>
        /// <value>
        /// The file manager.
        /// </value>
        public override CoreFileManager FileManager => new U9FileManager();

        /// <summary>
        /// Opens the pak file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public override CorePakFile OpenPakFile(string filePath) => new U9PakFile(filePath);

        //public override MultiPakFile OpenPakFile(string[] filePath)
        //{
        //              .Where(x => Path.GetExtension(x) == ".bsa" || Path.GetExtension(x) == ".ba2");
        //    Packs.AddRange(files.Select(x => OpenPakFile(x)));
        //}
    }
}