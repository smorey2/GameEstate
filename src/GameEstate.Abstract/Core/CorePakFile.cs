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
    public abstract class CorePakFile : IDisposable
    {
        public readonly string FilePath;
        public readonly string Game;
        public readonly PakFormat PakFormat;
        public readonly string Name;
        public readonly Dictionary<string, string> Params = new Dictionary<string, string>();
        public uint Version;
        //
        public IList<FileMetadata> Files;
        public HashSet<string> FilesRawSet;
        public ILookup<string, FileMetadata> FilesByPath;
        public GenericPool<BinaryReader> Pool;
        public bool UsePool = true;
        public object Tag;

        /// <summary>
        /// Initializes a new instance of the <see cref="CorePakFile"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="game">The game.</param>
        /// <param name="pakFormat">The pak format.</param>
        /// <exception cref="ArgumentNullException">
        /// filePath
        /// or
        /// pakFormat
        /// or
        /// datFormat
        /// </exception>
        public CorePakFile(string filePath, string game, PakFormat pakFormat)
        {
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            Game = game ?? throw new ArgumentNullException(nameof(game));
            PakFormat = pakFormat ?? throw new ArgumentNullException(nameof(pakFormat));
            var name = Path.GetFileName(FilePath);
            Name = !string.IsNullOrEmpty(name) ? name : Path.GetFileName(Path.GetDirectoryName(FilePath));
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }
        ~CorePakFile() => Close();

        /// <summary>
        /// Opens this instance.
        /// </summary>
        protected void Open()
        {
            var watch = new Stopwatch();
            watch.Start();
            Pool = UsePool && File.Exists(FilePath) ? new GenericPool<BinaryReader>(() => new BinaryReader(File.Open(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))) : null;
            if (Pool != null) Pool.Action(async r => await ReadAsync(r, PakFormat.ReadStage.File));
            else ReadAsync(null, PakFormat.ReadStage.File).GetAwaiter().GetResult();
            Process();
            CoreDebug.Log($"Opening: {Name} @ {watch.ElapsedMilliseconds}ms");
            watch.Stop();
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            Files = null;
            FilesRawSet = null;
            FilesByPath = null;
            Pool?.Dispose();
            Pool = null;
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
        public bool Contains(string filePath) => FilesByPath.Contains(filePath.Replace('\\', '/'));

        /// <summary>
        /// Loads the file data asynchronous.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public Task<byte[]> LoadFileDataAsync(string filePath, Action<FileMetadata, string> exception = null)
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
            if (Pool != null) return Pool.Func(r => ReadFileDataAsync(r, file, exception));
            else return ReadFileDataAsync(null, file, exception);
        }

        /// <summary>
        /// Reads the file data asynchronous.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="file">The file.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public virtual Task<byte[]> ReadFileDataAsync(BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception) => PakFormat.ReadFileAsync(this, r, file, exception);

        /// <summary>
        /// Writes the file data asynchronous.
        /// </summary>
        /// <param name="w">The w.</param>
        /// <param name="file">The file.</param>
        /// <param name="data">The data.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public virtual Task WriteFileDataAsync(BinaryWriter w, FileMetadata file, byte[] data, Action<FileMetadata, string> exception) => PakFormat.WriteFileAsync(this, w, file, data, exception);

        /// <summary>
        /// Reads the asynchronous.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="stage">The stage.</param>
        /// <returns></returns>
        public virtual Task ReadAsync(BinaryReader r, PakFormat.ReadStage stage) => PakFormat.ReadAsync(this, r, stage);

        /// <summary>
        /// Writes the asynchronous.
        /// </summary>
        /// <param name="w">The w.</param>
        /// <param name="stage">The stage.</param>
        /// <returns></returns>
        public virtual Task WriteAsync(BinaryWriter w, PakFormat.WriteStage stage) => PakFormat.WriteAsync(this, w, stage);

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
    }
}