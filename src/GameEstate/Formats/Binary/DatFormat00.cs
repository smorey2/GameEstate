using GameEstate.Core;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    public class DatFormat00 : DatFormat
    {
        public override Task<byte[]> ReadAsync(CorePakFile source, BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception = null) => Task.FromResult(r.ReadBytes((int)file.FileSize));

        public override Task WriteAsync(CorePakFile source, BinaryWriter w, FileMetadata file, byte[] data, Action<FileMetadata, string> exception = null) { w.Write(data); return Task.CompletedTask; }
    }
}