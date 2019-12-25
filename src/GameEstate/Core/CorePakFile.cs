using GameEstate.Core.DataFormat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameEstate.Core
{
    public abstract class CorePakFile : IDisposable
    {
        public uint Version;
        public readonly string FilePath;
        internal readonly PakFormat PakFormat;
        internal readonly DatFormat DatFormat;
        //
        internal IList<FileMetadata> Files;
        internal ILookup<string, FileMetadata> FilesByPath;
        internal bool HasNamePrefix;
        GenericPool<BinaryReader> _pool;

        public CorePakFile(string filePath, PakFormat pakFormat, DatFormat datFormat)
        {
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            PakFormat = pakFormat ?? throw new ArgumentNullException(nameof(pakFormat));
            DatFormat = datFormat ?? throw new ArgumentNullException(nameof(datFormat));
            if (!File.Exists(FilePath))
                return;
            _pool = new GenericPool<BinaryReader>(() => new BinaryReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)));
            var r = _pool.Get();
            try { ReadAsync(r).GetAwaiter().GetResult(); }
            finally { _pool.Release(r); }
            Process();
        }

        public void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }
        ~CorePakFile() => Close();

        public void Close()
        {
            _pool?.Dispose();
            _pool = null;
        }

        public bool ContainsFile(string filePath) => FilesByPath.Contains(filePath.Replace("/", "\\"));

        public Task<byte[]> LoadFileDataAsync(string filePath, Action<FileMetadata, string> exception)
        {
            var files = FilesByPath[filePath.Replace("/", "\\")].ToArray();
            if (files.Length == 1)
            {
                var r = _pool.Get();
                try { return LoadFileDataAsync(r, files[0], exception); }
                finally { _pool.Release(r); }
            }
            exception?.Invoke(null, $"LoadFileDataAsync: {filePath} @ {files.Length}"); //CoreDebug.Log($"LoadFileDataAsync: {filePath} @ {files.Length}");
            if (files.Length == 0)
                throw new FileNotFoundException(filePath);
            throw new InvalidOperationException();
        }

        protected virtual Task<byte[]> LoadFileDataAsync(BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception) => DatFormat.ReadAsync(this, r, file, exception);

        protected virtual Task WriteFileDataAsync(BinaryWriter w, FileMetadata file, byte[] data, Action<FileMetadata, string> exception) => DatFormat.WriteAsync(this, w, file, data, exception);

        protected virtual Task ReadAsync(BinaryReader r) => PakFormat.ReadAsync(this, r);

        protected virtual Task WriteAsync(BinaryWriter w, PakFormat.WriteState stage) => PakFormat.WriteAsync(this, w, stage);

        protected virtual void Process() => FilesByPath = Files.ToLookup(x => x.Path, StringComparer.OrdinalIgnoreCase);

        #region Extract / Insert

        public async Task ExtractAsync(string path, int from = 0, Action<FileMetadata, int> advance = null, Action<FileMetadata, string> exception = null)
        {
            // write pak
            var newPath = Path.Combine(path, ".set");
            using (var w = new BinaryWriter(new FileStream(newPath, FileMode.Create, FileAccess.Write)))
                await PakFormat.Default.WriteAsync(this, w, PakFormat.WriteState.Header);

            // write files
            Parallel.For(from, Files.Count, new ParallelOptions { }, async index =>
            {
                var file = Files[index];
                newPath = Path.Combine(path, file.Path);

                // create directory
                var directory = Path.GetDirectoryName(newPath);
                if (!string.IsNullOrEmpty(directory))
                    Directory.CreateDirectory(directory);

                // extract file
                var r = _pool.Get();
                try
                {
                    var b = await LoadFileDataAsync(r, file, exception);
                    using (var s = new FileStream(newPath, FileMode.Create, FileAccess.Write))
                        s.Write(b, 0, b.Length);
                    advance?.Invoke(file, index);
                }
                catch (Exception e) { exception?.Invoke(file, $"Exception: {e.Message}"); }
                finally { _pool.Release(r); }
            });
        }

        public async Task InsertAsync(BinaryWriter w, string path, int from = 0, Action<FileMetadata, int> advance = null, Action<FileMetadata, string> exception = null)
        {
            // read pak
            var newPath = Path.Combine(path, ".set");
            using (var r = new BinaryReader(File.Open(newPath, FileMode.Open, FileAccess.Read, FileShare.Read)))
                await PakFormat.Default.ReadAsync(this, r);

            if (from == 0)
                await PakFormat.WriteAsync(this, w, PakFormat.WriteState.Header);

            // write files
            Parallel.For(0, Files.Count, new ParallelOptions { MaxDegreeOfParallelism = 1 }, async index =>
            {
                var file = Files[index];
                newPath = Path.Combine(path, file.Path);

                // create directory
                var directory = Path.GetDirectoryName(newPath);
                if (string.IsNullOrEmpty(directory) || !Directory.Exists(directory))
                {
                    exception?.Invoke(file, $"Directory Missing: {directory}");
                    return;
                }

                // insert file
                try
                {
                    await PakFormat.WriteAsync(this, w, PakFormat.WriteState.File);
                    using (var r = File.Open(newPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                        await WriteFileDataAsync(w, file, r.ReadAllBytes(), exception);
                    advance?.Invoke(file, index);
                }
                catch (Exception e) { exception?.Invoke(file, $"Exception: {e.Message}"); }
            });
        }

        #endregion
    }
}