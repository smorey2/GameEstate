﻿using GameEstate.Formats.Binary;
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
        public uint Version;
        public readonly string Name;
        public readonly string FilePath;
        internal readonly PakFormat PakFormat;
        internal readonly DatFormat DatFormat;
        //
        public IList<FileMetadata> Files;
        internal HashSet<string> FilesRawSet;
        internal ILookup<string, FileMetadata> FilesByPath;
        internal GenericPool<BinaryReader> Pool;
        internal object Tag;
        // meta
        internal bool HasExtra;
        internal bool HasNamePrefix;

        /// <summary>
        /// Initializes a new instance of the <see cref="CorePakFile"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="pakFormat">The pak format.</param>
        /// <param name="datFormat">The dat format.</param>
        /// <exception cref="ArgumentNullException">
        /// filePath
        /// or
        /// pakFormat
        /// or
        /// datFormat
        /// </exception>
        public CorePakFile(string filePath, PakFormat pakFormat, DatFormat datFormat)
        {
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            PakFormat = pakFormat ?? throw new ArgumentNullException(nameof(pakFormat));
            DatFormat = datFormat ?? throw new ArgumentNullException(nameof(datFormat));
            Name = Path.GetFileName(FilePath);
            if (!File.Exists(FilePath))
                return;
            Pool = new GenericPool<BinaryReader>(() => new BinaryReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)));
            var r = Pool.Get();
            try { ReadAsync(r, PakFormat.ReadStage.File).GetAwaiter().GetResult(); }
            finally { Pool.Release(r); }
            Process();
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
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            Files?.Clear();
            FilesRawSet?.Clear();
            FilesByPath = null;
            Pool?.Dispose();
            Pool = null;
            if (Tag is IDisposable disposableTag)
                disposableTag.Dispose();
            Tag = null;
        }

        /// <summary>
        /// Determines whether the specified file path contains file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>
        ///   <c>true</c> if the specified file path contains file; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsFile(string filePath) => FilesByPath.Contains(filePath.Replace("/", "\\"));

        /// <summary>
        /// Loads the file data asynchronous.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public Task<byte[]> LoadFileDataAsync(string filePath, Action<FileMetadata, string> exception)
        {
            var files = FilesByPath[filePath.Replace("/", "\\")].ToArray();
            if (files.Length == 1)
            {
                var r = Pool.Get();
                try { return ReadFileDataAsync(r, files[0], exception); }
                finally { Pool.Release(r); }
            }
            exception?.Invoke(null, $"LoadFileDataAsync: {filePath} @ {files.Length}"); //CoreDebug.Log($"LoadFileDataAsync: {filePath} @ {files.Length}");
            if (files.Length == 0)
                throw new FileNotFoundException(filePath);
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Reads the file data asynchronous.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="file">The file.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        internal protected virtual Task<byte[]> ReadFileDataAsync(BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception) => DatFormat.ReadAsync(this, r, file, exception);

        /// <summary>
        /// Reads the extra data asynchronous.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="file">The file.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        internal protected virtual Task<byte[]> ReadExtraDataAsync(BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception) => DatFormat.ReadExtraAsync(this, r, file, exception);

        /// <summary>
        /// Writes the file data asynchronous.
        /// </summary>
        /// <param name="w">The w.</param>
        /// <param name="file">The file.</param>
        /// <param name="data">The data.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        internal protected virtual Task WriteFileDataAsync(BinaryWriter w, FileMetadata file, byte[] data, Action<FileMetadata, string> exception) => DatFormat.WriteAsync(this, w, file, data, exception);

        /// <summary>
        /// Writes the extra data asynchronous.
        /// </summary>
        /// <param name="w">The w.</param>
        /// <param name="file">The file.</param>
        /// <param name="data">The data.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        internal protected virtual Task WriteExtraDataAsync(BinaryWriter w, FileMetadata file, byte[] data, Action<FileMetadata, string> exception) => DatFormat.WriteExtraAsync(this, w, file, data, exception);

        /// <summary>
        /// Reads the asynchronous.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="stage">The stage.</param>
        /// <returns></returns>
        protected virtual Task ReadAsync(BinaryReader r, PakFormat.ReadStage stage) => PakFormat.ReadAsync(this, r, stage);

        /// <summary>
        /// Writes the asynchronous.
        /// </summary>
        /// <param name="w">The w.</param>
        /// <param name="stage">The stage.</param>
        /// <returns></returns>
        protected virtual Task WriteAsync(BinaryWriter w, PakFormat.WriteStage stage) => PakFormat.WriteAsync(this, w, stage);

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal protected virtual void Process() => FilesByPath = Files.Where(x => x != null).ToLookup(x => x.Path, StringComparer.OrdinalIgnoreCase);

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