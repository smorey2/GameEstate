using GameEstate.Formats.Binary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace GameEstate.Core
{
    [DebuggerDisplay("{Name}")]
    public abstract class CoreDatFile : IDisposable
    {
        public readonly string FilePath;
        public readonly string Game;
        public DatFormat DatFormat;
        public readonly string Name;
        public readonly Dictionary<string, string> Params = new Dictionary<string, string>();
        public uint Version;
        //
        public GenericPool<BinaryReader> Pool;
        public bool UsePool = true;
        public object Tag;

        public CoreDatFile(string filePath, string game, DatFormat datFormat)
        {
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            Game = game ?? throw new ArgumentNullException(nameof(game));
            DatFormat = datFormat ?? throw new ArgumentNullException(nameof(datFormat));
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
        ~CoreDatFile() => Close();

        /// <summary>
        /// Opens this instance.
        /// </summary>
        protected void Open()
        {
            var watch = new Stopwatch();
            watch.Start();
            Pool = UsePool && FilePath != null && File.Exists(FilePath) ? new GenericPool<BinaryReader>(() => new BinaryReader(File.Open(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))) : null;
            if (Pool != null) Pool.Action(async r => await ReadAsync(r, DatFormat.ReadStage.File));
            else ReadAsync(null, DatFormat.ReadStage.File).GetAwaiter().GetResult();
            Process();
            CoreDebug.Log($"Opening: {Name} @ {watch.ElapsedMilliseconds}ms");
            watch.Stop();
        }

        // SKY:Smell
        public BinaryReader GetReader(string filePath) => File.Exists(FilePath) ? new BinaryReader(File.Open(Path.Combine(FilePath, filePath), FileMode.Open, FileAccess.Read, FileShare.Read)) : null;
        public string GetFilePath(string filePath) => File.Exists(FilePath) ? Path.Combine(FilePath, filePath) : null;

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            Pool?.Dispose();
            Pool = null;
            if (Tag is IDisposable disposableTag)
                disposableTag.Dispose();
            Tag = null;
        }

        /// <summary>
        /// Reads the asynchronous.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="stage">The stage.</param>
        /// <returns></returns>
        public virtual Task ReadAsync(BinaryReader r, DatFormat.ReadStage stage) => DatFormat.ReadAsync(this, r, stage);

        /// <summary>
        /// Writes the asynchronous.
        /// </summary>
        /// <param name="w">The w.</param>
        /// <param name="stage">The stage.</param>
        /// <returns></returns>
        public virtual Task WriteAsync(BinaryWriter w, DatFormat.WriteStage stage) => DatFormat.WriteAsync(this, w, stage);

        /// <summary>
        /// Processes this instance.
        /// </summary>
        public virtual void Process() => DatFormat.Process(this);
    }
}