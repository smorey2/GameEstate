using System;
using System.IO;
using System.Threading.Tasks;

namespace GameEstate.Core.DataFormat
{
    public class DatFormat
    {
        public readonly static DatFormat Default = new DatFormat00();

        public virtual Task<byte[]> ReadAsync(CorePakFile source, BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception = null) => throw new NotSupportedException();

        public virtual Task WriteAsync(CorePakFile source, BinaryWriter w, FileMetadata file, byte[] data, Action<FileMetadata, string> exception = null) => throw new NotSupportedException();
    }
}