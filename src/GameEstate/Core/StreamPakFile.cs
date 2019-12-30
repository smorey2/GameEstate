using GameEstate.Formats.Binary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GameEstate.Core
{
    /// <summary>
    /// StreamPakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.CorePakFile" />
    public class StreamPakFile : CorePakFile
    {
        readonly HttpCache Host;

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamPakFile" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="host">The host.</param>
        public StreamPakFile(string filePath, Uri host = null) : base(filePath, new PakFormatStream(), null)
        {
            UsePool = false;
            Host = host != null ? new HttpCache(host) : null;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="StreamPakFile"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="filePath">The file path.</param>
        public StreamPakFile(CorePakFile parent, string filePath) : base(filePath, new PakFormatStream(), null)
        {
            UsePool = false;
            Files = parent.Files;
            Process();
        }

        /// <summary>
        /// Reads the asynchronous.
        /// </summary>
        /// <param name="_">The .</param>
        /// <param name="stage">The stage.</param>
        internal protected override async Task ReadAsync(BinaryReader _, PakFormat.ReadStage stage)
        {
            // http pak
            if (Host != null)
            {
                var files = Files = new List<FileMetadata>();
                var set = await Host.GetSetAsync();
                foreach (var item in set)
                    files.Add(new FileMetadata
                    {
                        Path = item
                    });
                return;
            }

            // read pak
            var path = FilePath;
            if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
                return;
            var setPath = Path.Combine(path, ".set");
            if (File.Exists(setPath))
                using (var r = new BinaryReader(File.Open(setPath, FileMode.Open, FileAccess.Read, FileShare.Read)))
                    await PakFormat.Stream.ReadAsync(this, r, PakFormat.ReadStage._Set);
            var metaPath = Path.Combine(path, ".meta");
            if (File.Exists(metaPath))
                using (var r = new BinaryReader(File.Open(setPath, FileMode.Open, FileAccess.Read, FileShare.Read)))
                    await PakFormat.Stream.ReadAsync(this, r, PakFormat.ReadStage._Meta);
            var rawPath = Path.Combine(path, ".raw");
            if (File.Exists(rawPath))
                using (var r = new BinaryReader(File.Open(rawPath, FileMode.Open, FileAccess.Read, FileShare.Read)))
                    await PakFormat.Stream.ReadAsync(this, r, PakFormat.ReadStage._Raw);
        }

        /// <summary>
        /// Writes the asynchronous.
        /// </summary>
        /// <param name="_">The .</param>
        /// <param name="stage">The stage.</param>
        /// <exception cref="NotSupportedException"></exception>
        internal protected override async Task WriteAsync(BinaryWriter _, PakFormat.WriteStage stage)
        {
            // http pak
            if (Host != null)
                throw new NotSupportedException();

            // write pak
            var path = FilePath;
            if (!string.IsNullOrEmpty(path) && !Directory.Exists(path))
                Directory.CreateDirectory(path);
            var setPath = Path.Combine(path, ".set");
            using (var w = new BinaryWriter(new FileStream(setPath, FileMode.Create, FileAccess.Write)))
                await PakFormat.Stream.WriteAsync(this, w, PakFormat.WriteStage._Set);
            var metaPath = Path.Combine(path, ".meta");
            using (var w = new BinaryWriter(new FileStream(metaPath, FileMode.Create, FileAccess.Write)))
                await PakFormat.Stream.WriteAsync(this, w, PakFormat.WriteStage._Meta);
            var rawPath = Path.Combine(path, ".raw");
            if (FilesRawSet != null && FilesRawSet.Count > 0)
                using (var w = new BinaryWriter(new FileStream(rawPath, FileMode.Create, FileAccess.Write)))
                    await PakFormat.Stream.WriteAsync(this, w, PakFormat.WriteStage._Raw);
            else if (File.Exists(rawPath))
                File.Delete(rawPath);
        }

        /// <summary>
        /// Reads the file data asynchronous.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="file">The file.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        protected override async Task<byte[]> ReadFileDataAsync(BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception)
        {
            var path = file.Path;
            // http pak
            if (Host != null)
                return await Host.GetFileAsync(path);

            // read pak
            path = Path.Combine(FilePath, path);
            if (!File.Exists(path))
                return null;
            using (var s = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                return s.ReadAllBytes();
        }

        /// <summary>
        /// Writes the file data asynchronous.
        /// </summary>
        /// <param name="w">The w.</param>
        /// <param name="file">The file.</param>
        /// <param name="data">The data.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        protected internal override Task WriteFileDataAsync(BinaryWriter w, FileMetadata file, byte[] data, Action<FileMetadata, string> exception) => throw new NotSupportedException();
    }
}