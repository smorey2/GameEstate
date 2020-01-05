using GameEstate.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    // https://github.com/dolkensp/unp4k/releases
    public class PakFormatP4k : PakFormat
    {
        //static readonly FieldInfo ZipFileKeyField = typeof(ZipFile).GetField("key", BindingFlags.NonPublic | BindingFlags.Instance);
        static readonly byte[] P4kKey = new byte[] { 0x5E, 0x7A, 0x20, 0x02, 0x30, 0x2E, 0xEB, 0x1A, 0x3B, 0xB6, 0x17, 0xC3, 0x0F, 0xDE, 0x1E, 0x47 };

        public override Task ReadAsync(CorePakFile source, BinaryReader r, ReadStage stage)
        {
            if (stage != ReadStage.File)
                throw new ArgumentOutOfRangeException(nameof(stage), stage.ToString());

            source.UsePool = false;
            var files = source.Files = new List<FileMetadata>();
            var pak = (ZipFile)(source.Tag = new ZipFile(r.BaseStream) { Key = P4kKey }); //: ZipFileKeyField.SetValue(pak, P4kKey);
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

        public override Task WriteAsync(CorePakFile source, BinaryWriter w, WriteStage stage)
        {
            source.UsePool = false;
            var files = source.Files;
            var pak = (ZipFile)(source.Tag = new ZipFile(w.BaseStream) { Key = P4kKey }); //: ZipFileKeyField.SetValue(pak, P4kKey);
            pak.BeginUpdate();
            foreach (var file in files)
            {
                var entry = (ZipEntry)(file.Tag = new ZipEntry(Path.GetFileName(file.Path)));
                pak.Add(entry);
                source.PakFormat.WriteFileAsync(source, w, file, null, null);
            }
            pak.CommitUpdate();
            return Task.CompletedTask;
        }

        public override Task<byte[]> ReadFileAsync(CorePakFile source, BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception = null)
        {
            var pak = (ZipFile)source.Tag;
            var entry = (ZipEntry)file.Tag;
            //var stream = pak.GetInputStream(entry);
            //if (!stream.CanRead)
            //{
            //    exception?.Invoke(file, $"Stream Closed.");
            //    return null;
            //}
            try
            {
                using (var s = pak.GetInputStream(entry))
                    return Task.FromResult(s.ReadAllBytes());
            }
            catch (Exception e)
            {
                exception?.Invoke(file, $"Exception: {e.Message}");
                return null;
            }
        }

        public override Task WriteFileAsync(CorePakFile source, BinaryWriter w, FileMetadata file, byte[] data, Action<FileMetadata, string> exception = null)
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