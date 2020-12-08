﻿using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEstate.Core
{
    [DebuggerDisplay("{Name}")]
    public abstract class BinaryPakFile : EstatePakFile
    {
        readonly ConcurrentDictionary<string, GenericPool<BinaryReader>> BinaryReaders = new ConcurrentDictionary<string, GenericPool<BinaryReader>>();
        public readonly string FilePath;
        public readonly PakBinary PakBinary;
        public object Tag;

        // state
        public bool UseBinaryReader = true;
        public Func<string, string> FileMask;
        public readonly Dictionary<string, string> Params = new Dictionary<string, string>();
        public uint Magic;
        public uint Version;
        public object DecryptKey;

        // explorer
        protected Func<ExplorerManager, BinaryPakFile, Task<List<ExplorerItemNode>>> ExplorerItems;
        protected Dictionary<string, Func<ExplorerManager, BinaryPakFile, FileMetadata, Task<List<ExplorerInfoNode>>>> ExplorerInfos = new Dictionary<string, Func<ExplorerManager, BinaryPakFile, FileMetadata, Task<List<ExplorerInfoNode>>>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryPakFile" /> class.
        /// </summary>
        /// <param name="estate">The estate.</param>
        /// <param name="game">The game.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="pakBinary">The pak binary.</param>
        /// <param name="tag">The tag.</param>
        /// <exception cref="ArgumentNullException">pakBinary</exception>
        public BinaryPakFile(Estate estate, string game, string filePath, PakBinary pakBinary, object tag = null) : base(estate, game, !string.IsNullOrEmpty(Path.GetFileName(filePath)) ? Path.GetFileName(filePath) : Path.GetFileName(Path.GetDirectoryName(filePath)))
        {
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            PakBinary = pakBinary ?? throw new ArgumentNullException(nameof(pakBinary));
            Tag = tag;
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
            if (UseBinaryReader) GetBinaryReader()?.Action(async r => await ReadAsync(r, PakBinary.ReadStage.File));
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
        public GenericPool<BinaryReader> GetBinaryReader(string path = null, int retainInPool = 10) => BinaryReaders.GetOrAdd(path ?? FilePath, filePath => File.Exists(filePath) ? new GenericPool<BinaryReader>(() => new BinaryReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)), retainInPool) : null);

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public override void Close()
        {
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
        /// <param name="path">The file path.</param>
        /// <returns>
        ///   <c>true</c> if the specified file path contains file; otherwise, <c>false</c>.
        /// </returns>
        public override bool Contains(string path) => throw new NotSupportedException();

        // string or bytes
        /// <summary>
        /// The get string or bytes encoding
        /// </summary>
        public Encoding GetStringOrBytesEncoding = Encoding.UTF8;

        /// <summary>
        /// Gets the string or bytes.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public virtual object GetStringOrBytes(Stream stream, bool dispose = true)
        {
            var bytes = stream.ReadAllBytes();
            if (dispose)
                stream.Dispose();
            return !bytes.Contains<byte>(0x00)
                ? (object)GetStringOrBytesEncoding.GetString(bytes)
                : bytes;
        }

        /// <summary>
        /// Finds the texture.
        /// </summary>
        /// <param name="path">The texture path.</param>
        /// <returns></returns>
        //public override string FindTexture(string path) => Contains(path) ? path : null;

        /// <summary>
        /// Loads the file data asynchronous.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public override Task<Stream> LoadFileDataAsync(string path, Action<FileMetadata, string> exception = null) => throw new NotSupportedException();

        /// <summary>
        /// Loads the object asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">The file path.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public override Task<T> LoadFileObjectAsync<T>(string path, Action<FileMetadata, string> exception = null) => throw new NotSupportedException();

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
        /// Loads the file data asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file">The file.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public async Task<T> LoadFileObjectAsync<T>(FileMetadata file, Action<FileMetadata, string> exception = null)
        {
            var type = typeof(T);
            var stream = await LoadFileDataAsync(file, exception = null);
            if (file.ObjectFactory == null)
                return type == typeof(Stream) || type == typeof(object)
                    ? (T)(object)stream
                    : throw new ArgumentOutOfRangeException(nameof(T), $"Stream not returned for {file.Path} with {type.Name}");
            var r = new BinaryReader(stream);
            object value = null;
            Task<object> task = null;
            try
            {
                task = file.ObjectFactory(r, file);
                if (task == null)
                    return type == typeof(Stream) || type == typeof(object)
                        ? (T)(object)stream
                        : throw new ArgumentOutOfRangeException(nameof(T), $"Stream not returned for {file.Path} with {type.Name}");
                value = await task;
                return value is T z ? z
                    : value is IRedirected<T> y ? y.Value
                    : throw new InvalidCastException();
            }
            finally { if (task != null && !(value != null && value is IDisposable)) r.Dispose(); }
        }

        /// <summary>
        /// Reads the file data asynchronous.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="file">The file.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public virtual Task<Stream> ReadFileDataAsync(BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception) => PakBinary.ReadDataAsync(this, r, file, exception);

        /// <summary>
        /// Writes the file data asynchronous.
        /// </summary>
        /// <param name="w">The w.</param>
        /// <param name="file">The file.</param>
        /// <param name="data">The data.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public virtual Task WriteFileDataAsync(BinaryWriter w, FileMetadata file, Stream data, Action<FileMetadata, string> exception) => PakBinary.WriteDataAsync(this, w, file, data, exception);

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
        public virtual void Process() => PakBinary.Process(this);

        #region Explorer

        /// <summary>
        /// Gets the explorer item nodes.
        /// </summary>
        /// <param name="manager">The resource.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override async Task<List<ExplorerItemNode>> GetExplorerItemNodesAsync(ExplorerManager manager) => Valid && ExplorerItems != null ? await ExplorerItems(manager, this) : null;

        /// <summary>
        /// Gets the explorer information nodes.
        /// </summary>
        /// <param name="manager">The resource.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override async Task<List<ExplorerInfoNode>> GetExplorerInfoNodesAsync(ExplorerManager manager, ExplorerItemNode item)
        {
            if (!(item.Tag is FileMetadata file))
                return null;
            List<ExplorerInfoNode> nodes = null;
            var obj = await LoadFileObjectAsync<object>(file);
            if (obj is IGetExplorerInfo info)
                nodes = info.GetInfoNodes(manager, file);
            else if (obj is Stream stream)
            {
                var value = GetStringOrBytes(stream);
                nodes = value is string text ? new List<ExplorerInfoNode> {
                        new ExplorerInfoNode(null, new ExplorerContentTab { Type = "Text", Name = "Text", Value = text }),
                        new ExplorerInfoNode("Text", items: new List<ExplorerInfoNode> {
                            new ExplorerInfoNode($"Length: {text.Length}"),
                        }) }
                    : value is byte[] bytes ? new List<ExplorerInfoNode> {
                        new ExplorerInfoNode(null, new ExplorerContentTab { Type = "Hex", Name = "Hex", Value = new MemoryStream(bytes) }),
                        new ExplorerInfoNode("Bytes", items: new List<ExplorerInfoNode> {
                            new ExplorerInfoNode($"Length: {bytes.Length}"),
                        }) }
                    : throw new ArgumentOutOfRangeException(nameof(value), value.GetType().Name);
            }
            else if (obj is IDisposable disposable)
                disposable.Dispose();
            if (nodes == null)
                return null;
            nodes.Add(new ExplorerInfoNode("File", items: new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"Path: {file.Path}"),
                new ExplorerInfoNode($"FileSize: {file.FileSize}"),
            }));
            //nodes.Add(new ExplorerInfoNode(null, new ExplorerContentTab { Type = "Hex", Name = "TEST", Value = new MemoryStream() }));
            //nodes.Add(new ExplorerInfoNode(null, new ExplorerContentTab { Type = "Image", Name = "TEST", MaxWidth = 500, MaxHeight = 500, Value = null }));
            return nodes;
        }

        #endregion
    }
}