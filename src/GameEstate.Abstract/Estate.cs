using GameEstate.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GameEstate
{
    /// <summary>
    /// Estate
    /// </summary>
    public class Estate
    {
        public enum PakMultiType
        {
            SingleBinary,
            Full,
        }

        public enum DatMultiType
        {
            SingleBinary,
            Full,
        }

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
            public string NameWithFound => $"{Name}{(Found ? " - found" : null)}";
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
        /// Gets or sets the pak multi.
        /// </summary>
        /// <value>
        /// The pak multi.
        /// </value>
        public PakMultiType PakMulti { get; set; }

        /// <summary>
        /// Gets the type of the dat file.
        /// </summary>
        /// <value>
        /// The type of the dat file.
        /// </value>
        public Type DatFileType { get; set; }

        /// <summary>
        /// Gets or sets the dat multi.
        /// </summary>
        /// <value>
        /// The dat multi.
        /// </value>
        public DatMultiType DatMulti { get; set; }

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
        /// <param name="filePaths">The file paths.</param>
        /// <param name="game">The game.</param>
        /// <returns></returns>
        public AbstractPakFile OpenPakFile(string[] filePaths, string game)
        {
            if (game == null)
                throw new ArgumentNullException(nameof(game));
            if (PakFileType == null || filePaths.Length == 0)
                return null;
            switch (PakMulti)
            {
                case PakMultiType.SingleBinary:
                    return filePaths.Length == 1
                        ? (AbstractPakFile)Activator.CreateInstance(PakFileType, filePaths[0], game, null)
                        : new MultiPakFile(game, "Many", filePaths.Select(x => (AbstractPakFile)Activator.CreateInstance(PakFileType, x, game, null)).ToArray());
                case PakMultiType.Full: return (AbstractPakFile)Activator.CreateInstance(PakFileType, filePaths, game);
                default: throw new ArgumentOutOfRangeException(nameof(PakMulti), PakMulti.ToString());
            }
        }

        /// <summary>
        /// Opens the pak file.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns></returns>
        public AbstractPakFile OpenPakFile(Resource resource)
        {
            if (!resource.StreamPak)
                return OpenPakFile(resource.Paths, resource.Game);
            return new MultiPakFile(resource.Game, "Many", resource.Paths.Select(x => new StreamPakFile(FileManager.HostFactory, x, resource.Game, resource.Host)).ToArray());
        }

        /// <summary>
        /// Opens the pak file.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="many">if set to <c>true</c> [many].</param>
        /// <returns></returns>
        public AbstractPakFile OpenPakFile(Uri uri) => OpenPakFile(FileManager.ParseResource(this, uri));

        #endregion

        #region Dat

        /// <summary>
        /// Opens the dat file.
        /// </summary>
        /// <param name="filePaths">The file paths.</param>
        /// <returns></returns>
        public AbstractDatFile OpenDatFile(string[] filePaths, string game)
        {
            if (game == null)
                throw new ArgumentNullException(nameof(game));
            if (DatFileType == null || filePaths.Length == 0)
                return null;
            switch (DatMulti)
            {
                case DatMultiType.SingleBinary:
                    return filePaths.Length == 1
                        ? (AbstractDatFile)Activator.CreateInstance(PakFileType, filePaths[0], game)
                        : new MultiDatFile(game, "Many", filePaths.Select(x => (AbstractDatFile)Activator.CreateInstance(DatFileType, x, game)).ToArray());
                case DatMultiType.Full: return (AbstractDatFile)Activator.CreateInstance(DatFileType, filePaths, game);
                default: throw new ArgumentOutOfRangeException(nameof(PakMulti), PakMulti.ToString());
            }
        }

        /// <summary>
        /// Opens the dat file.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns></returns>
        public AbstractDatFile OpenDatFile(Resource resource)
        {
            if (!resource.StreamPak)
                return OpenDatFile(resource.Paths, resource.Game);
            return new MultiDatFile(resource.Game, "Many", resource.Paths.Select(x => new StreamDatFile(FileManager.HostFactory, x, resource.Game, resource.Host)).ToArray());
        }

        /// <summary>
        /// Opens the dat file.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="many">if set to <c>true</c> [many].</param>
        /// <returns></returns>
        public AbstractDatFile OpenDatFile(Uri uri) => OpenDatFile(FileManager.ParseResource(this, uri));

        #endregion
    }
}