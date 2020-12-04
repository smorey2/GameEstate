using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameEstate
{
    /// <summary>
    /// AbstractPakFile
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public abstract class EstatePakFile : IDisposable
    {
        public virtual bool Valid => true;
        public readonly Estate Estate;
        public readonly string Game;
        public readonly string Name;

        /// <summary>
        /// Initializes a new instance of the <see cref="EstatePakFile" /> class.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <exception cref="ArgumentNullException">filePaths
        /// or
        /// game</exception>
        public EstatePakFile(Estate estate, string game, string name)
        {
            Estate = estate ?? throw new ArgumentNullException(nameof(estate));
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
        /// Finds the texture.
        /// </summary>
        /// <param name="texturePath">The texture path.</param>
        /// <returns></returns>
        public virtual string FindTexture(string texturePath) => Contains(texturePath) ? texturePath : null;

        /// <summary>
        /// Loads the file data asynchronous.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public abstract Task<Stream> LoadFileDataAsync(string filePath, Action<FileMetadata, string> exception = null);

        /// <summary>
        /// Loads the object asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">The file path.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public abstract Task<T> LoadFileObjectAsync<T>(string filePath, Action<FileMetadata, string> exception = null);

        /// <summary>
        /// Gets the graphic.
        /// </summary>
        /// <value>
        /// The graphic.
        /// </value>
        public IEstateGraphic Graphic { get; internal set; }

        #region Explorer

        /// <summary>
        /// Gets the explorer item nodes.
        /// </summary>
        /// <param name="manager">The resource.</param>
        /// <returns></returns>
        public virtual Task<List<ExplorerItemNode>> GetExplorerItemNodesAsync(ExplorerManager manager) => throw new NotImplementedException();

        /// <summary>
        /// Gets the explorer item filters.
        /// </summary>
        /// <param name="manager">The resource.</param>
        /// <returns></returns>
        public virtual Task<List<ExplorerItemNode.Filter>> GetExplorerItemFiltersAsync(ExplorerManager manager) =>
            Task.FromResult(Estate.FileManager.Filters.TryGetValue(Game, out var z) ? z.Select(x => new ExplorerItemNode.Filter(x.Key, x.Value)).ToList() : null);

        /// <summary>
        /// Gets the explorer information nodes.
        /// </summary>
        /// <param name="manager">The resource.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public virtual Task<List<ExplorerInfoNode>> GetExplorerInfoNodesAsync(ExplorerManager manager, ExplorerItemNode item) => throw new NotImplementedException();

        #endregion
    }
}