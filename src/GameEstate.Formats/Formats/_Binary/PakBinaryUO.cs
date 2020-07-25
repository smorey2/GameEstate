using GameEstate.Core;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    public class PakBinaryUO : PakBinary
    {
        public static readonly PakBinary Instance = new PakBinaryUO();
        PakBinaryUO() { }

        public unsafe override Task ReadAsync(BinaryPakFile source, BinaryReader r, ReadStage stage)
        {
            return Task.CompletedTask;
        }

        public override Task<byte[]> ReadFileAsync(BinaryPakFile source, BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception = null)
        {
            r.Position(file.Position);
            return Task.FromResult(r.ReadBytes((int)file.FileSize));
        }
    }
}