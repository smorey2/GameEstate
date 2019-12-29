using GameEstate.Cry;
using GameEstate.Red;
using GameEstate.Rsi;
using GameEstate.Tes;
using GameEstate.U9;
using GameEstate.UO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameEstate.Core
{
    public abstract class CoreEstate
    {
        public struct Resource
        {
            public string Host;
            public string[] Paths;
            public int Game;
        }

        static CoreEstate()
        {
            var platform = UnsafeUtils.Platform;
        }

        /// <summary>
        /// Gets the estates.
        /// </summary>
        /// <value>
        /// The estates.
        /// </value>
        public static IDictionary<string, CoreEstate> Estates { get; } = new Dictionary<string, CoreEstate>
        {
            { "Cry", new CryEstate() },
            { "Red", new RedEstate() },
            { "Rsi", new RsiEstate() },
            { "Tes", new TesEstate() },
            { "U9", new U9Estate() },
            { "UO", new UOEstate() },
        };

        /// <summary>
        /// Parses the specified name.
        /// </summary>
        /// <param name="estateName">Name of the estate.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">estateName</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">name</exception>
        public static CoreEstate Parse(string estateName) => Estates.TryGetValue(estateName, out var estate) ? estate : throw new ArgumentOutOfRangeException(nameof(estateName), estateName);

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public abstract string Description { get; }

        /// <summary>
        /// Gets the type of the game.
        /// </summary>
        /// <value>
        /// The type of the game.
        /// </value>
        public abstract Type GameType { get; }

        /// <summary>
        /// Gets the game.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns></returns>
        public (string name, string description) GetGame(int game) { var name = GameType.GetEnumName(game); return (name, GameType.GetEnumDescription(name)); }

        /// <summary>
        /// Gets the games.
        /// </summary>
        /// <returns></returns>
        public (string name, string description)[] GetGames()
            => GameType.GetEnumNames().Zip(
                Enum.GetValues(GameType).Cast<Enum>().Select(x => GameType.GetEnumDescription(x.ToString()))
            , (name, description) => (name, description)).ToArray();

        /// <summary>
        /// Gets the file manager.
        /// </summary>
        /// <value>
        /// The file manager.
        /// </value>
        public abstract CoreFileManager FileManager { get; }

        /// <summary>
        /// Opens the pak file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public abstract CorePakFile OpenPakFile(string filePath);

        /// <summary>
        /// Opens the pak file.
        /// </summary>
        /// <param name="filePaths">The file paths.</param>
        /// <returns></returns>
        public virtual MultiPakFile OpenPakFile(string[] filePaths) => new MultiPakFile(filePaths.Select(x => OpenPakFile(x)).ToArray());

        /// <summary>
        /// Opens the pak file.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns></returns>
        public MultiPakFile OpenPakFile(Resource resource)
        {
            return OpenPakFile(resource.Paths);
        }

        /// <summary>
        /// Parses the resource.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="many">if set to <c>true</c> [many].</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// uri
        /// or
        /// uri
        /// </exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        public Resource ParseResource(Uri uri, bool many = true)
        {
            // game
            var fragment = uri.Fragment?.Substring(uri.Fragment.Length != 0 ? 1 : 0);
            var gameName = Enum.GetNames(GameType).FirstOrDefault(x => string.Equals(x, fragment, StringComparison.OrdinalIgnoreCase)) ?? throw new ArgumentOutOfRangeException(nameof(fragment), fragment);
            var game = (int)Enum.Parse(GameType, gameName);

            // file-scheme
            if (string.Equals(uri.Scheme, "game", StringComparison.OrdinalIgnoreCase))
            {
                var path = uri.LocalPath.Substring(1);
                return new Resource
                {
                    Paths = FileManager.GetFilePaths(path, game, many) ?? throw new InvalidOperationException($"{gameName} not available"),
                    Game = game,
                };
            }
            // file-scheme
            else if (uri.IsFile)
            {
                var path = uri.LocalPath;
                return new Resource
                {
                    Paths = path.Contains('*') ? Directory.GetFiles(Path.GetDirectoryName(path), Path.GetFileName(path)) : File.Exists(path) ? new[] { path } : null,
                    Game = game,
                };
            }
            // network-scheme
            else if (!string.IsNullOrEmpty(uri.Host))
            {
                var path = uri.LocalPath;
                return new Resource
                {
                    Paths = new[] { path },
                    Game = game,
                };
            }
            else throw new ArgumentOutOfRangeException(nameof(uri), uri.OriginalString);
        }
    }
}