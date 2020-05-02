using GameEstate.Core;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    public class DatBinary
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
        public readonly static DatBinary Stream = new DatBinaryStream();

        /// <summary>
        /// Reads the asynchronous.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="r">The r.</param>
        /// <param name="stage">The stage.</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public virtual Task ReadAsync(BinaryDatFile source, BinaryReader r, ReadStage stage) => throw new NotSupportedException();

        /// <summary>
        /// Writes the asynchronous.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="w">The w.</param>
        /// <param name="stage">The stage.</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public virtual Task WriteAsync(BinaryDatFile source, BinaryWriter w, WriteStage stage) => throw new NotSupportedException();

        /// <summary>
        /// Processes this instance.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <exception cref="NotSupportedException"></exception>
        public virtual void Process(BinaryDatFile source) => throw new NotSupportedException();
    }
}