using System;
using System.Collections.Generic;

namespace GameEstate.Core
{
    /// <summary>
    /// AbstractFileManager
    /// </summary>
    public abstract class AbstractFileManager
    {
        /// <summary>
        /// Gets the host factory.
        /// </summary>
        /// <value>
        /// The host factory.
        /// </value>
        public abstract Func<Uri, string, AbstractHost> HostFactory { get; }

        /// <summary>
        /// Parses the resource.
        /// </summary>
        /// <param name="estate">The estate.</param>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public abstract Estate.Resource ParseResource(Estate estate, Uri uri);

        /// <summary>
        /// Gets a value indicating whether this instance has locations.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is data present; otherwise, <c>false</c>.
        /// </value>
        public bool FoundGames => Locations.Count != 0;

        /// <summary>
        /// The locations
        /// </summary>
        public IDictionary<string, string> Locations = new Dictionary<string, string>();

        /// <summary>
        /// The ignored
        /// </summary>
        public HashSet<string> Ignore = new HashSet<string>();

        /// <summary>
        /// Gets the game file paths.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="pathOrPattern">The path or pattern.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">pathOrPattern</exception>
        /// <exception cref="ArgumentOutOfRangeException">pathOrPattern</exception>
        public abstract string[] GetGameFilePaths(string game, string pathOrPattern);

        /// <summary>
        /// Gets the local file paths.
        /// </summary>
        /// <param name="pathOrPattern">The path or pattern.</param>
        /// <param name="streamPak">if set to <c>true</c> [file pak].</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">pathOrPattern</exception>
        public abstract string[] GetLocalFilePaths(string pathOrPattern, out bool streamPak);

        /// <summary>
        /// Gets the host file paths.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="host">The host.</param>
        /// <param name="streamPak">if set to <c>true</c> [file pak].</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">uri</exception>
        /// <exception cref="ArgumentOutOfRangeException">pathOrPattern</exception>
        /// <exception cref="NotSupportedException">Web wildcard access to supported</exception>
        public abstract string[] GetHttpFilePaths(Uri uri, out Uri host, out bool streamPak);
    }
}

