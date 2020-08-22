using GameEstate.Formats.Binary;
using GameEstate.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameEstate.Core
{
    [DebuggerDisplay("{Name}")]
    public abstract class BinaryPakManyFile : BinaryPakFile
    {
        public IList<FileMetadata> Files;
        public HashSet<string> FilesRawSet;
        public ILookup<string, FileMetadata> FilesByPath { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryPakManyFile"/> class.
        /// </summary>
        /// <param name="estate">The estate.</param>
        /// <param name="game">The game.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="pakBinary">The pak binary.</param>
        /// <param name="tag">The tag.</param>
        public BinaryPakManyFile(Estate estate, string game, string filePath, PakBinary pakBinary, object tag = null) : base(estate, game, filePath, pakBinary, tag) { }

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
        /// Loads the object asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">The file path.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public override Task<T> LoadFileObjectAsync<T>(string filePath, Action<FileMetadata, string> exception = null)
        {
            if (typeof(T) == typeof(TextureInfo))
                filePath = FindTexture(filePath);
            var files = FilesByPath[filePath.Replace('\\', '/')].ToArray();
            if (files.Length == 1)
                return LoadFileObjectAsync<T>(files[0], exception);
            exception?.Invoke(null, $"LoadFileObjectAsync: {filePath} @ {files.Length}"); //CoreDebug.Log($"LoadFileObjectAsync: {filePath} @ {files.Length}");
            if (files.Length == 0)
                throw new FileNotFoundException(filePath);
            throw new InvalidOperationException();
        }

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
    }
}