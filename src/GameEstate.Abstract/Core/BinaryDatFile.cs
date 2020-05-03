using GameEstate.Formats.Binary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace GameEstate.Core
{
    [DebuggerDisplay("{Name}")]
    public abstract class BinaryDatFile : AbstractDatFile
    {
        public readonly string FilePath;
        public DatBinary DatBinary;
        public readonly Dictionary<string, string> Params = new Dictionary<string, string>();
        public uint Version;
        //
        public GenericPool<BinaryReader> Pool;
        public bool UsePool = true;
        public object Tag;

        public BinaryDatFile(string filePath, string game, DatBinary datBinary) : base(game, !string.IsNullOrEmpty(Path.GetFileName(filePath)) ? Path.GetFileName(filePath) : Path.GetFileName(Path.GetDirectoryName(filePath)))
        {
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            DatBinary = datBinary ?? throw new ArgumentNullException(nameof(datBinary));
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }
        ~BinaryDatFile() => Close();

        /// <summary>
        /// Opens this instance.
        /// </summary>
        protected void Open()
        {
            var watch = new Stopwatch();
            watch.Start();
            Pool = UsePool && FilePath != null && File.Exists(FilePath) ? new GenericPool<BinaryReader>(() => new BinaryReader(File.Open(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))) : null;
            if (Pool != null) Pool.Action(async r => await ReadAsync(r, DatBinary.ReadStage.File));
            else ReadAsync(null, DatBinary.ReadStage.File).GetAwaiter().GetResult();
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
        public override void Close()
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
        public virtual Task ReadAsync(BinaryReader r, DatBinary.ReadStage stage) => DatBinary.ReadAsync(this, r, stage);

        /// <summary>
        /// Writes the asynchronous.
        /// </summary>
        /// <param name="w">The w.</param>
        /// <param name="stage">The stage.</param>
        /// <returns></returns>
        public virtual Task WriteAsync(BinaryWriter w, DatBinary.WriteStage stage) => DatBinary.WriteAsync(this, w, stage);

        /// <summary>
        /// Processes this instance.
        /// </summary>
        public virtual void Process() => DatBinary.Process(this);
    }
}