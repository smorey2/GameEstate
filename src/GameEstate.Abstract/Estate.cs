using GameEstate.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GameEstate
{
    /// <summary>
    /// Estate
    /// </summary>
    public class Estate
    {
        /// <summary>
        /// Resource
        /// </summary>
        public struct Resource
        {
            /// <summary>
            /// The stream pak
            /// </summary>
            public bool StreamPak;
            /// <summary>
            /// The host
            /// </summary>
            public Uri Host;
            /// <summary>
            /// The paths
            /// </summary>
            public string[] Paths;
            /// <summary>
            /// The game
            /// </summary>
            public string Game;
        }

        /// <summary>
        /// EstateGame
        /// </summary>
        public class EstateGame
        {
            /// <summary>
            /// The identifier
            /// </summary>
            public string Game;
            /// <summary>
            /// Gets the name of the found.
            /// </summary>
            /// <value>
            /// The name of the found.
            /// </value>
            public string NameAndFound => $"{Name}{(Found ? " - found" : null)}";
            /// <summary>
            /// The name
            /// </summary>
            public string Name;
            /// <summary>
            /// The pak
            /// </summary>
            public Uri DefaultPak;
            /// <summary>
            /// The dat
            /// </summary>
            public Uri DefaultDat;
            /// <summary>
            /// The has location
            /// </summary>
            public bool Found;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => Name;

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets the type of the pak file.
        /// </summary>
        /// <value>
        /// The type of the pak file.
        /// </value>
        public Type PakFileType { get; set; }

        /// <summary>
        /// Gets the type of the dat file.
        /// </summary>
        /// <value>
        /// The type of the dat file.
        /// </value>
        public Type DatFileType { get; set; }

        /// <summary>
        /// Gets the game.
        /// </summary>
        /// <param name="id">The game id.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">game</exception>
        public (string id, EstateGame game) GetGame(string id) => Games.TryGetValue(id, out var game) ? (id, game) : throw new ArgumentOutOfRangeException(nameof(id), id);

        /// <summary>
        /// Gets the games.
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, EstateGame> Games { get; set; }

        /// <summary>
        /// Gets the xforms.
        /// </summary>
        /// <value>
        /// The xforms.
        /// </value>
        public IDictionary<string, object> Xforms { get; set; }

        /// <summary>
        /// Gets the file manager.
        /// </summary>
        /// <value>
        /// The file manager.
        /// </value>
        public AbstractFileManager FileManager { get; set; }

        /// <summary>
        /// Parses the resource.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public Resource ParseResource(Uri uri) => FileManager.ParseResource(this, uri);

        #region Pak

        /// <summary>
        /// Opens the pak file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="game">The game.</param>
        /// <returns></returns>
        public CorePakFile OpenPakFile(string filePath, string game) => PakFileType != null ? (CorePakFile)Activator.CreateInstance(PakFileType, filePath, game) : null;

        /// <summary>
        /// Opens the pak file.
        /// </summary>
        /// <param name="filePaths">The file paths.</param>
        /// <param name="game">The game.</param>
        /// <returns></returns>
        public MultiPakFile OpenPakFile(string[] filePaths, string game) => new MultiPakFile(filePaths.Select(x => OpenPakFile(x, game)).ToArray());

        /// <summary>
        /// Opens the pak file.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns></returns>
        public MultiPakFile OpenPakFile(Resource resource)
        {
            if (!resource.StreamPak)
                return OpenPakFile(resource.Paths, resource.Game);
            var filePaths = resource.Paths;
            return new MultiPakFile(filePaths.Select(x => new StreamPakFile(FileManager.HostFactory, x, resource.Game, resource.Host)).ToArray());
        }

        /// <summary>
        /// Opens the pak file.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="many">if set to <c>true</c> [many].</param>
        /// <returns></returns>
        public MultiPakFile OpenPakFile(Uri uri) => OpenPakFile(FileManager.ParseResource(this, uri));

        #endregion

        #region Dat

        /// <summary>
        /// Opens the dat file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public CoreDatFile OpenDatFile(string filePath, string game) => DatFileType != null ? (CoreDatFile)Activator.CreateInstance(DatFileType, filePath, game) : null;

        /// <summary>
        /// Opens the dat file.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns></returns>
        public CoreDatFile OpenDatFile(Resource resource)
        {
            var filePath = resource.Paths.SingleOrDefault();
            if (!resource.StreamPak)
                return OpenDatFile(filePath, resource.Game);
            return new StreamDatFile(FileManager.HostFactory, filePath, resource.Game, resource.Host);
        }

        /// <summary>
        /// Opens the dat file.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="many">if set to <c>true</c> [many].</param>
        /// <returns></returns>
        public CoreDatFile OpenDatFile(Uri uri) => OpenDatFile(FileManager.ParseResource(this, uri));

        #endregion
    }
}