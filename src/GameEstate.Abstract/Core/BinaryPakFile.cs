using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats.Binary;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameEstate.Core
{
    [DebuggerDisplay("{Name}")]
    public abstract class BinaryPakFile : AbstractPakFile
    {
        public readonly string FilePath;
        public readonly PakBinary PakBinary;
        public readonly Dictionary<string, string> Params = new Dictionary<string, string>();
        public uint Version;
        //
        public IList<FileMetadata> Files;
        public HashSet<string> FilesRawSet;
        public ILookup<string, FileMetadata> FilesByPath { get; private set; }
        ConcurrentDictionary<string, GenericPool<BinaryReader>> BinaryReaders = new ConcurrentDictionary<string, GenericPool<BinaryReader>>();
        public bool UseBinaryReader = true;
        public object Tag;
        // explorer
        protected Func<ExplorerManager, BinaryPakFile, Task<List<ExplorerItemNode>>> ExplorerItem;
        protected Dictionary<string, Func<ExplorerManager, BinaryPakFile, FileMetadata, Task<List<ExplorerInfoNode>>>> ExplorerInfos = new Dictionary<string, Func<ExplorerManager, BinaryPakFile, FileMetadata, Task<List<ExplorerInfoNode>>>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryPakFile" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="game">The game.</param>
        /// <param name="pakBinary">The pak binary.</param>
        /// <exception cref="ArgumentNullException">pakBinary</exception>
        public BinaryPakFile(string filePath, string game, PakBinary pakBinary) : base(game, !string.IsNullOrEmpty(Path.GetFileName(filePath)) ? Path.GetFileName(filePath) : Path.GetFileName(Path.GetDirectoryName(filePath)))
        {
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            PakBinary = pakBinary ?? throw new ArgumentNullException(nameof(pakBinary));
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }
        ~BinaryPakFile() => Close();

        /// <summary>
        /// Opens this instance.
        /// </summary>
        protected void Open()
        {
            var watch = new Stopwatch();
            watch.Start();
            if (UseBinaryReader) GetBinaryReader().Action(async r => await ReadAsync(r, PakBinary.ReadStage.File));
            else ReadAsync(null, PakBinary.ReadStage.File).GetAwaiter().GetResult();
            Process();
            EstateDebug.Log($"Opening: {Name} @ {watch.ElapsedMilliseconds}ms");
            watch.Stop();
        }

        /// <summary>
        /// Gets the binary reader.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public GenericPool<BinaryReader> GetBinaryReader(string path = null, int retain = 10) => BinaryReaders.GetOrAdd(path ?? FilePath, filePath => File.Exists(filePath) ? new GenericPool<BinaryReader>(() => new BinaryReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))) : null);

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public override void Close()
        {
            Files = null;
            FilesRawSet = null;
            FilesByPath = null;
            foreach (var r in BinaryReaders.Values)
                r.Dispose();
            BinaryReaders.Clear();
            if (Tag is IDisposable disposableTag)
                disposableTag.Dispose();
            Tag = null;
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
        public override Task<byte[]> LoadFileDataAsync(string filePath, Action<FileMetadata, string> exception = null)
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
        public Task<byte[]> LoadFileDataAsync(FileMetadata file, Action<FileMetadata, string> exception = null)
        {
            if (UseBinaryReader) return GetBinaryReader().Func(r => ReadFileDataAsync(r, file, exception));
            else return ReadFileDataAsync(null, file, exception);
        }

        /// <summary>
        /// Reads the file data asynchronous.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="file">The file.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public virtual Task<byte[]> ReadFileDataAsync(BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception) => PakBinary.ReadFileAsync(this, r, file, exception);

        /// <summary>
        /// Writes the file data asynchronous.
        /// </summary>
        /// <param name="w">The w.</param>
        /// <param name="file">The file.</param>
        /// <param name="data">The data.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public virtual Task WriteFileDataAsync(BinaryWriter w, FileMetadata file, byte[] data, Action<FileMetadata, string> exception) => PakBinary.WriteFileAsync(this, w, file, data, exception);

        /// <summary>
        /// Reads the asynchronous.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="stage">The stage.</param>
        /// <returns></returns>
        public virtual Task ReadAsync(BinaryReader r, PakBinary.ReadStage stage) => PakBinary.ReadAsync(this, r, stage);

        /// <summary>
        /// Writes the asynchronous.
        /// </summary>
        /// <param name="w">The w.</param>
        /// <param name="stage">The stage.</param>
        /// <returns></returns>
        public virtual Task WriteAsync(BinaryWriter w, PakBinary.WriteStage stage) => PakBinary.WriteAsync(this, w, stage);

        /// <summary>
        /// Processes this instance.
        /// </summary>
        public virtual void Process() => FilesByPath = Files?.Where(x => x != null).ToLookup(x => x.Path, StringComparer.OrdinalIgnoreCase);

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
        public override async Task<List<ExplorerItemNode>> GetExplorerItemNodesAsync(ExplorerManager manager) => ExplorerItem != null ? await ExplorerItem(manager, this) : null;

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