using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameEstate.Core
{
    /// <summary>
    /// AbstractDatFile
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public abstract class AbstractDatFile : IDisposable
    {
        public readonly string Game;
        public readonly string Name;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractDatFile" /> class.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <exception cref="ArgumentNullException">filePaths
        /// or
        /// game</exception>
        public AbstractDatFile(string game, string name)
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