using GameEstate.Formats.Binary;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace GameEstate.Core
{
    public interface IRecord { }

    public interface ICellRecord : IRecord { }

    [DebuggerDisplay("{Name}")]
    public abstract class CoreDatFile : IDisposable
    {
        public uint Version;
        public readonly string Name;
        public readonly string FilePath;
        public readonly string Game;
        internal readonly DatFormat DatFormat;
        //
        internal GenericPool<BinaryReader> Pool;
        internal bool UsePool = true;
        internal object Tag;

        public CoreDatFile(string filePath, string game, DatFormat datFormat)
        {
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            Game = game ?? throw new ArgumentNullException(nameof(game));
            DatFormat = datFormat ?? throw new ArgumentNullException(nameof(datFormat));
            Name = Path.GetFileName(FilePath);
            if (string.IsNullOrEmpty(Name))
                Name = Path.GetFileName(Path.GetDirectoryName(FilePath));
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
            Pool = UsePool && File.Exists(FilePath) ? new GenericPool<BinaryReader>(() => new BinaryReader(File.Open(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))) : null;
            if (Pool != null)
            {
                var r = Pool.Get();
                try { ReadAsync(r, DatFormat.ReadStage.File).GetAwaiter().GetResult(); }
                finally { Pool.Release(r); }
            }
            else ReadAsync(null, DatFormat.ReadStage.File).GetAwaiter().GetResult();
            var watch = new Stopwatch();
            watch.Start();
            Process();
            CoreDebug.Log($"Loading: {watch.ElapsedMilliseconds}");
            watch.Stop();
        }

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
        internal protected virtual Task ReadAsync(BinaryReader r, DatFormat.ReadStage stage) => DatFormat.ReadAsync(this, r, stage);

        /// <summary>
        /// Writes the asynchronous.
        /// </summary>
        /// <param name="w">The w.</param>
        /// <param name="stage">The stage.</param>
        /// <returns></returns>
        internal protected virtual Task WriteAsync(BinaryWriter w, DatFormat.WriteStage stage) => DatFormat.WriteAsync(this, w, stage);

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal protected virtual void Process() => DatFormat.Process(this);
    }
}