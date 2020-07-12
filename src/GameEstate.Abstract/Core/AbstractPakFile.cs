using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats.Binary;
using GameEstate.Graphics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameEstate.Core
{
    /// <summary>
    /// AbstractPakFile
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public abstract class AbstractPakFile : IDisposable
    {
        public readonly string Game;
        public readonly string Name;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractPakFile" /> class.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <exception cref="ArgumentNullException">filePaths
        /// or
        /// game</exception>
        public AbstractPakFile(string game, string name)
        {
            Game = game ?? throw new ArgumentNullException(nameof(game));
            Name = name;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public abstract void Close();

        /// <summary>
        /// Determines whether this instance contains the object.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified file path]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool Contains(string filePath);

        /// <summary>
        /// Loads the file data asynchronous.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public abstract Task<byte[]> LoadFileDataAsync(string filePath, Action<FileMetadata, string> exception);

        #region Explorer

        /// <summary>
        /// Gets the explorer item nodes.
        /// </summary>
        /// <param name="manager">The resource.</param>
        /// <returns></returns>
        public abstract Task<List<ExplorerItemNode>> GetExplorerItemNodesAsync(ExplorerManager manager);

        /// <summary>
        /// Gets the explorer information nodes.
        /// </summary>
        /// <param name="manager">The resource.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public abstract Task<List<ExplorerInfoNode>> GetExplorerInfoNodesAsync(ExplorerManager manager, ExplorerItemNode item);

        #endregion
    }
}