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
using System.Reflection;

namespace GameEstate.Core
{
    public class CoreEstate
    {
        public struct Resource
        {
            public bool StreamPak;
            public Uri Host;
            public string[] Paths;
            public int Game;
        }

        static CoreEstate()
        {
            var platform = UnsafeUtils.Platform;
            var assembly = Assembly.GetExecutingAssembly();
            CoreEstate estate;
            foreach (var key in new[] { "Cry", "Red", "Rsi", "Tes", "U9", "UO" })
                using (var r = new StreamReader(assembly.GetManifestResourceStream($"GameEstate.Estates.{key}.json")))
                    Estates.Add((estate = ParseEstate(r.ReadToEnd())).Id, estate);
        }

        public static CoreEstate ParseEstate(string body)
        {
            return new CoreEstate();
        }

        /// <summary>
        /// Gets the estates.
        /// </summary>
        /// <value>
        /// The estates.
        /// </value>
        public static IDictionary<string, CoreEstate> Estates { get; } = new Dictionary<string, CoreEstate>
        {
            //{ "Cry", new CryEstate() },
            //{ "Red", new RedEstate() },
            //{ "Rsi", new RsiEstate() },
            //{ "Tes", new TesEstate() },
            //{ "U9", new U9Estate() },
            //{ "UO", new UOEstate() },
        };

        /// <summary>
        /// Gets the specified estate.
        /// </summary>
        /// <param name="estateName">Name of the estate.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">estateName</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">name</exception>
        public static CoreEstate GetEstate(string estateName) => Estates.TryGetValue(estateName, out var estate) ? estate : throw new ArgumentOutOfRangeException(nameof(estateName), estateName);

        public virtual string Id { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public virtual string Name { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public virtual string Description { get; }

        /// <summary>
        /// Gets the type of the game.
        /// </summary>
        /// <value>
        /// The type of the game.
        /// </value>
        public virtual Type GameType { get; }

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
        public virtual CoreFileManager FileManager { get; }

        /// <summary>
        /// Opens the pak file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public virtual CorePakFile OpenPakFile(string filePath) => null;

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
            if (!resource.StreamPak)
                return OpenPakFile(resource.Paths);
            var filePaths = resource.Paths;
            return new MultiPakFile(filePaths.Select(x => new StreamPakFile(x, resource.Host)).ToArray());
        }

        /// <summary>
        /// Opens the pak file.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="many">if set to <c>true</c> [many].</param>
        /// <returns></returns>
        public MultiPakFile OpenPakFile(Uri uri) => OpenPakFile(ParseResource(uri));

        /// <summary>
        /// Parses the resource.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">fragment</exception>
        /// <exception cref="InvalidOperationException">
        /// No {gameName} resources match.
        /// or
        /// No {gameName} resources match.
        /// or
        /// No {gameName} resources match.
        /// </exception>
        public Resource ParseResource(Uri uri)
        {
            var fragment = uri.Fragment?.Substring(uri.Fragment.Length != 0 ? 1 : 0);
            var gameName = Enum.GetNames(GameType).FirstOrDefault(x => string.Equals(x, fragment, StringComparison.OrdinalIgnoreCase)) ?? throw new ArgumentOutOfRangeException(nameof(fragment), fragment);
            var r = new Resource { Game = (int)Enum.Parse(GameType, gameName) };
            // file-scheme
            if (string.Equals(uri.Scheme, "game", StringComparison.OrdinalIgnoreCase))
                r.Paths = FileManager.GetGameFilePaths(r.Game, uri.LocalPath.Substring(1)) ?? throw new InvalidOperationException($"No {gameName} resources match.");
            // file-scheme
            else if (uri.IsFile)
                r.Paths = FileManager.GetLocalFilePaths(uri.LocalPath, out r.StreamPak) ?? throw new InvalidOperationException($"No {gameName} resources match.");
            // network-scheme
            else
                r.Paths = FileManager.GetHttpFilePaths(uri, out r.Host, out r.StreamPak) ?? throw new InvalidOperationException($"No {gameName} resources match.");
            return r;
        }
    }
}