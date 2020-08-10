using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats.Binary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameEstate.Core
{
    [DebuggerDisplay("{Name}")]
    public abstract class BinaryPakMultiFile : BinaryPakFile
    {
        public IList<FileMetadata> Files;
        public HashSet<string> FilesRawSet;
        public ILookup<string, FileMetadata> FilesByPath { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryPakMultiFile"/> class.
        /// </summary>
        /// <param name="estate">The estate.</param>
        /// <param name="game">The game.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="pakBinary">The pak binary.</param>
        /// <param name="tag">The tag.</param>
        public BinaryPakMultiFile(Estate estate, string game, string filePath, PakBinary pakBinary, object tag = null) : base(estate, game, filePath, pakBinary, tag) { }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public override void Close()
        {
            Files = null;
            FilesRawSet = null;
            FilesByPath = null;
            base.Close();
        }

        /// <summary>
        /// Determines whether the pak contains the specified file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>
        ///   <c>true</c> if the specified file path contains file; otherwise, <c>false</c>.
        /// </returns>
        public override bool Contains(string filePath) => FilesByPath.Contains(filePath.Replace('\\', '/'));

        /// <summary>
        /// Loads the file data asynchronous.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public override Task<Stream> LoadFileDataAsync(string filePath, Action<FileMetadata, string> exception = null)
        {
            var files = FilesByPath[filePath.Replace('\\', '/')].ToArray();
            if (files.Length == 1)
                return LoadFileDataAsync(files[0], exception);
            exception?.Invoke(null, $"LoadFileDataAsync: {filePath} @ {files.Length}"); //CoreDebug.Log($"LoadFileDataAsync: {filePath} @ {files.Length}");
            if (files.Length == 0)
                throw new FileNotFoundException(filePath);
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Loads the file data asynchronous.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public Task<Stream> LoadFileDataAsync(FileMetadata file, Action<FileMetadata, string> exception = null) =>
            UseBinaryReader
                ? GetBinaryReader().Func(r => ReadFileDataAsync(r, file, exception))
                : ReadFileDataAsync(null, file, exception);

        /// <summary>
        /// Processes this instance.
        /// </summary>
        public override void Process() { FilesByPath = Files?.Where(x => x != null).ToLookup(x => x.Path, StringComparer.OrdinalIgnoreCase); base.Process(); }

        /// <summary>
        /// Adds the raw file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="message">The message.</param>
        public void AddRawFile(FileMetadata file, string message)
        {
            lock (this)
            {
                if (FilesRawSet == null)
                    FilesRawSet = new HashSet<string>();
                FilesRawSet.Add(file.Path);
            }
        }

        #region Explorer

        /// <summary>
        /// Gets the explorer item nodes.
        /// </summary>
        /// <param name="manager">The resource.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override async Task<List<ExplorerItemNode>> GetExplorerItemNodesAsync(ExplorerManager manager) => ExplorerItems != null ? await ExplorerItems(manager, this) : null;

        ///// <summary>
        ///// Gets the explorer item filters.
        ///// </summary>
        ///// <param name="manager">The resource.</param>
        ///// <returns></returns>
        ///// <exception cref="NotImplementedException"></exception>
        //public override Task<List<ExplorerItemNode.Filter>> GetExplorerItemFiltersAsync(ExplorerManager manager) => null;

        /// <summary>
        /// Gets the explorer information nodes.
        /// </summary>
        /// <param name="manager">The resource.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override async Task<List<ExplorerInfoNode>> GetExplorerInfoNodesAsync(ExplorerManager manager, ExplorerItemNode item)
        {
            var ext = Path.GetExtension(item.Name).ToLowerInvariant();
            var file = item.Tag as FileMetadata;
            if (ExplorerInfos.TryGetValue(ext, out var info))
                return await info(manager, this, file);
            else if (ExplorerInfos.TryGetValue("_default", out info))
                return await info(manager, this, file);
            return null;
        }

        #endregion
    }
}