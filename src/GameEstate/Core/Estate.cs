using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace GameEstate.Core
{
    public class Estate
    {
        public struct Resource
        {
            public bool StreamPak;
            public Uri Host;
            public string[] Paths;
            public string Game;
        }

        static Estate()
        {
            var platform = UnsafeUtils.Platform;
            var assembly = Assembly.GetExecutingAssembly();
            Estate estate;
            foreach (var key in new[] { "Cry", "Red", "Rsi", "Tes", "U9", "UO" })
                using (var r = new StreamReader(assembly.GetManifestResourceStream($"GameEstate.Estates.{key}Estate.json")))
                    Estates.Add((estate = ParseEstate(r.ReadToEnd())).Id, estate);
        }

        /// <summary>
        /// Gets the estates.
        /// </summary>
        /// <value>
        /// The estates.
        /// </value>
        public static IDictionary<string, Estate> Estates { get; } = new Dictionary<string, Estate>(StringComparer.OrdinalIgnoreCase);

        public static Estate ParseEstate(string json)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var p = JObject.Parse(json);
            return new Estate
            {
                Id = (string)p["id"],
                Name = (string)p["name"],
                Description = (string)p["description"],
                PakFileType = assembly.GetType((string)p["pakFileType"], false) ?? throw new ArgumentOutOfRangeException("pakFileType", (string)p["pakFileType"]),
                Games = p["games"] != null ? p["games"].Cast<JProperty>().ToDictionary(x => x.Name, x => (string)x.Value, StringComparer.OrdinalIgnoreCase) : throw new ArgumentNullException("games"),
                Xforms = p["xforms"] != null ? p["xforms"].Cast<JProperty>().ToDictionary(x => x.Name, x => (object)(string)x.Value, StringComparer.OrdinalIgnoreCase) : new Dictionary<string, object>(),
                FileManager = p["fileManager"] != null ? FileManager.ParseFileManager((JObject)p["fileManager"]) : new FileManager(),
            };
        }

        /// <summary>
        /// Gets the specified estate.
        /// </summary>
        /// <param name="estateName">Name of the estate.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">estateName</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">name</exception>
        public static Estate GetEstate(string estateName) => Estates.TryGetValue(estateName, out var estate) ? estate : throw new ArgumentOutOfRangeException(nameof(estateName), estateName);

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the type of the pak file.
        /// </summary>
        /// <value>
        /// The type of the pak file.
        /// </value>
        public Type PakFileType { get; private set; }

        /// <summary>
        /// Gets the game.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">game</exception>
        public (string game, string description) GetGame(string game) => Games.TryGetValue(game, out var description) ? (game, description) : throw new ArgumentOutOfRangeException(nameof(game), game);

        /// <summary>
        /// Gets the games.
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, string> Games { get; private set; }

        /// <summary>
        /// Gets the xforms.
        /// </summary>
        /// <value>
        /// The xforms.
        /// </value>
        public IDictionary<string, object> Xforms { get; private set; }

        /// <summary>
        /// Gets the file manager.
        /// </summary>
        /// <value>
        /// The file manager.
        /// </value>
        public FileManager FileManager { get; private set; }

        /// <summary>
        /// Opens the pak file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public CorePakFile OpenPakFile(string filePath) => (CorePakFile)Activator.CreateInstance(PakFileType, filePath);

        /// <summary>
        /// Opens the pak file.
        /// </summary>
        /// <param name="filePaths">The file paths.</param>
        /// <returns></returns>
        public MultiPakFile OpenPakFile(string[] filePaths) => new MultiPakFile(filePaths.Select(x => OpenPakFile(x)).ToArray());

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
            return new MultiPakFile(filePaths.Select(x => new StreamPakFile(x, resource.Game, resource.Host)).ToArray());
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
            var game = GetGame(fragment);
            var r = new Resource { Game = game.game };
            // file-scheme
            if (string.Equals(uri.Scheme, "game", StringComparison.OrdinalIgnoreCase))
                r.Paths = FileManager.GetGameFilePaths(r.Game, uri.LocalPath.Substring(1)) ?? throw new InvalidOperationException($"No {game} resources match.");
            // file-scheme
            else if (uri.IsFile)
                r.Paths = FileManager.GetLocalFilePaths(uri.LocalPath, out r.StreamPak) ?? throw new InvalidOperationException($"No {game} resources match.");
            // network-scheme
            else
                r.Paths = FileManager.GetHttpFilePaths(uri, out r.Host, out r.StreamPak) ?? throw new InvalidOperationException($"No {game} resources match.");
            return r;
        }
    }
}