using GameEstate.Core;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    public class DatFormat
    {
        /// <summary>
        /// ReadState
        /// </summary>
        public enum ReadStage { File }

        /// <summary>
        /// WriteState
        /// </summary>
        public enum WriteStage { File }

        /// <summary>
        /// The file
        /// </summary>
        public readonly static DatFormat Stream = new DatFormatStream();

        /// <summary>
        /// Reads the asynchronous.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="r">The r.</param>
        /// <param name="stage">The stage.</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public virtual Task ReadAsync(CoreDatFile source, BinaryReader r, ReadStage stage) => throw new NotSupportedException();

        /// <summary>
        /// Writes the asynchronous.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="w">The w.</param>
        /// <param name="stage">The stage.</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public virtual Task WriteAsync(CoreDatFile source, BinaryWriter w, WriteStage stage) => throw new NotSupportedException();

        /// <summary>
        /// Processes this instance.
        /// </summary>
        public virtual void Process(CoreDatFile source) => throw new NotSupportedException();
    }
}