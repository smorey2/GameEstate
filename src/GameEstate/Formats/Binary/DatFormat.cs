using GameEstate.Core;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    /// <summary>
    /// DatFormat
    /// </summary>
    public class DatFormat
    {
        /// <summary>
        /// Reads the asynchronous.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="r">The r.</param>
        /// <param name="file">The file.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public virtual Task<byte[]> ReadAsync(CorePakFile source, BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception = null) => throw new NotSupportedException();

        /// <summary>
        /// Reads the extra asynchronous.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="r">The r.</param>
        /// <param name="file">The file.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public virtual Task<byte[]> ReadExtraAsync(CorePakFile source, BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception = null)
        {
            r.Position(file.ExtraPosition);
            return Task.FromResult(r.ReadBytes(file.ExtraSize));
        }

        /// <summary>
        /// Writes the asynchronous.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="w">The w.</param>
        /// <param name="file">The file.</param>
        /// <param name="data">The data.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public virtual Task WriteAsync(CorePakFile source, BinaryWriter w, FileMetadata file, byte[] data, Action<FileMetadata, string> exception = null) => throw new NotSupportedException();

        /// <summary>
        /// Writes the extra asynchronous.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="w">The w.</param>
        /// <param name="file">The file.</param>
        /// <param name="data">The data.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public virtual Task WriteExtraAsync(CorePakFile source, BinaryWriter w, FileMetadata file, byte[] data, Action<FileMetadata, string> exception = null) { w.Write(data); return Task.CompletedTask; }
    }
}