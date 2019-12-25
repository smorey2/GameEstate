using GameEstate.Cry;
using GameEstate.Rsi;
using GameEstate.Tes;
using GameEstate.U9;
using GameEstate.UO;
using System;
using System.IO;
using System.Linq;

namespace GameEstate.Core
{
    public abstract class CoreEstate
    {
        public struct Resource
        {
            public string[] Paths;
            public int Game;
        }

        /// <summary>
        /// Parses the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">name</exception>
        public static CoreEstate Parse(string name)
        {
            var platform = UnsafeUtils.Platform;
            switch (name)
            {
                case "Cry": return new CryEstate();
                case "Rsi": return new RsiEstate();
                case "Tes": return new TesEstate();
                case "U9": return new U9Estate();
                case "UO": return new UOEstate();
                default: throw new ArgumentOutOfRangeException(nameof(name), name);
            }
        }

        /// <summary>
        /// Gets the type of the game.
        /// </summary>
        /// <value>
        /// The type of the game.
        /// </value>
        public abstract Type GameType { get; }

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
        public MultiPakFile OpenPakFile(Resource resource) => OpenPakFile(resource.Paths);

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
            var gameName = Enum.GetNames(GameType).FirstOrDefault(x => string.Equals(x, fragment, StringComparison.OrdinalIgnoreCase)) ?? throw new ArgumentOutOfRangeException(nameof(uri), uri.ToString());
            var game = (int)Enum.Parse(GameType, gameName);

            // file-scheme
            if (string.Equals(uri.Scheme, "game", StringComparison.OrdinalIgnoreCase))
            {
                var path = uri.LocalPath.Substring(1);
                return new Resource
                {
                    Paths = FileManager.GetFilePaths(many, path, game) ?? throw new InvalidOperationException($"{gameName} not available"),
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