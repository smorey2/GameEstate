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
        internal IList<FileMetadata> Files;
        internal ILookup<string, FileMetadata> FilesByPath;
        internal bool HasNamePrefix;
        GenericPool<BinaryReader> _pool;

        public CorePakFile(string filePath)
        {
            if (filePath == null)
                return;
            FilePath = filePath;
            _pool = new GenericPool<BinaryReader>(() => new BinaryReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)));
            var r = _pool.Get();
            try { ReadMetadata(r); }
            finally { _pool.Release(r); }
            ProcessMetadata();
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

        public Task<byte[]> LoadFileDataAsync(string filePath)
        {
            var files = FilesByPath[filePath.Replace("/", "\\")].ToArray();
            if (files.Length == 1)
                return LoadFileDataAsync(files[0]);
            CoreDebug.Log($"LoadFileDataAsync: {filePath} @ {files.Length}");
            if (files.Length == 0)
                throw new FileNotFoundException(filePath);
            throw new InvalidOperationException();
        }

        protected abstract Task<byte[]> LoadFileDataAsync(FileMetadata file);

        protected abstract void ReadMetadata(BinaryReader r);

        protected virtual void ProcessMetadata()
        {
            FilesByPath = Files.ToLookup(x => x.Path, StringComparer.OrdinalIgnoreCase);
        }

        protected abstract void Write(BinaryWriter w);
    }
}