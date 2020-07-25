using GameEstate.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    /// <summary>
    /// PakBinaryZip2
    /// </summary>
    /// <seealso cref="GameEstate.Formats.Binary.PakBinary" />
    public class PakBinaryZip2 : PakBinary
    {
        public static readonly PakBinary Instance = new PakBinaryZip2();
        PakBinaryZip2() { }

        public override Task ReadAsync(BinaryPakFile source, BinaryReader r, ReadStage stage)
        {
            if (stage != ReadStage.File)
                throw new ArgumentOutOfRangeException(nameof(stage), stage.ToString());

            source.UseBinaryReader = false;
            var files = source.Files = new List<FileMetadata>();
            var pak = (ZipArchive)(source.Tag = new ZipArchive(r.BaseStream, ZipArchiveMode.Read));
            foreach (ZipArchiveEntry entry in pak.Entries)
                files.Add(new FileMetadata
                {
                    Path = entry.Name.Replace('\\', '/'),
                    PackedSize = entry.CompressedLength,
                    FileSize = entry.Length,
                    Tag = entry,
                });
            return Task.CompletedTask;
        }

        public override Task<byte[]> ReadFileAsync(BinaryPakFile source, BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception = null)
        {
            var pak = (ZipArchive)source.Tag;
            var entry = (ZipArchiveEntry)file.Tag;
            try
            {
                using (var s = entry.Open())
                using (var ms = new MemoryStream())
                {
                    if (!s.CanRead)
                    {
                        exception?.Invoke(file, $"Unable to read stream.");
                        return Task.FromResult<byte[]>(null);
                    }
                    s.CopyTo(ms);
                    return Task.FromResult(ms.ToArray());
                }
            }
            catch (Exception e)
            {
                exception?.Invoke(file, $"Exception: {e.Message}");
                return Task.FromResult<byte[]>(null);
            }
        }
    }
}