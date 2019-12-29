using GameEstate.Core;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    public class PakFormat
    {
        /// <summary>
        /// ReadState
        /// </summary>
        public enum ReadStage { _Set, _Meta, _Raw, File }

        /// <summary>
        /// WriteState
        /// </summary>
        public enum WriteStage { _Set, _Meta, _Raw, Header, File }

        /// <summary>
        /// The file
        /// </summary>
        public readonly static PakFormat File = new PakFormatFile();

        /// <summary>
        /// Reads the asynchronous.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="r">The r.</param>
        /// <param name="stage">The stage.</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public virtual Task ReadAsync(CorePakFile source, BinaryReader r, ReadStage stage) => throw new NotSupportedException();

        /// <summary>
        /// Writes the asynchronous.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="w">The w.</param>
        /// <param name="stage">The stage.</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public virtual Task WriteAsync(CorePakFile source, BinaryWriter w, WriteStage stage) => throw new NotSupportedException();
    }
}