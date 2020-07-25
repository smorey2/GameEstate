using GameEstate.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    /// <summary>
    /// PakBinaryZip
    /// </summary>
    /// <seealso cref="GameEstate.Formats.Binary.PakBinary" />
    public class PakBinaryZip : PakBinary
    {
        public static readonly PakBinary Instance = new PakBinaryZip();
        public PakBinaryZip(byte[] key = null) => Key = key;

        readonly byte[] Key;

        public override Task ReadAsync(BinaryPakFile source, BinaryReader r, ReadStage stage)
        {
            if (stage != ReadStage.File)
                throw new ArgumentOutOfRangeException(nameof(stage), stage.ToString());

            source.UseBinaryReader = false;
            var files = source.Files = new List<FileMetadata>();
            var pak = (ZipFile)(source.Tag = new ZipFile(r.BaseStream) { Key = Key });
            foreach (ZipEntry entry in pak)
                files.Add(new FileMetadata
                {
                    Path = entry.Name.Replace('\\', '/'),
                    Crypted = entry.IsAesCrypted,
                    PackedSize = entry.CompressedSize,
                    FileSize = entry.Size,
                    Tag = entry,
                });
            return Task.CompletedTask;
        }

        public override Task WriteAsync(BinaryPakFile source, BinaryWriter w, WriteStage stage)
        {
            source.UseBinaryReader = false;
            var files = source.Files;
            var pak = (ZipFile)(source.Tag = new ZipFile(w.BaseStream) { Key = Key });
            pak.BeginUpdate();
            foreach (var file in files)
            {
                var entry = (ZipEntry)(file.Tag = new ZipEntry(Path.GetFileName(file.Path)));
                pak.Add(entry);
                source.PakBinary.WriteFileAsync(source, w, file, null, null);
            }
            pak.CommitUpdate();
            return Task.CompletedTask;
        }

        public override Task<byte[]> ReadFileAsync(BinaryPakFile source, BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception = null)
        {
            var pak = (ZipFile)source.Tag;
            var entry = (ZipEntry)file.Tag;
            try
            {
                using (var s = pak.GetInputStream(entry))
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

        public override Task WriteFileAsync(BinaryPakFile source, BinaryWriter w, FileMetadata file, byte[] data, Action<FileMetadata, string> exception = null)
        {
            var pak = (ZipFile)source.Tag;
            var entry = (ZipEntry)file.Tag;
            try
            {
                using (var s = pak.GetInputStream(entry))
                    s.Write(data, 0, data.Length);
            }
            catch (Exception e)
            {
                exception?.Invoke(file, $"Exception: {e.Message}");
            }
            return Task.CompletedTask;
        }
    }
}