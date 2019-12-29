using GameEstate.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    // https://github.com/dolkensp/unp4k/releases
    public class PakFormatP4k : PakFormat
    {
        static readonly FieldInfo ZipFileKeyField = typeof(ZipFile).GetField("key", BindingFlags.NonPublic | BindingFlags.Instance);
        static readonly byte[] P4kKey = new byte[] { 0x5E, 0x7A, 0x20, 0x02, 0x30, 0x2E, 0xEB, 0x1A, 0x3B, 0xB6, 0x17, 0xC3, 0x0F, 0xDE, 0x1E, 0x47 };

        public override Task ReadAsync(CorePakFile source, BinaryReader r, ReadStage stage)
        {
            if (stage != ReadStage.File)
                throw new ArgumentOutOfRangeException(nameof(stage), stage.ToString());
            var files = source.Files = new List<FileMetadata>();
            var pak = (ZipFile)(source.Tag = new ZipFile(r.BaseStream));
            ZipFileKeyField.SetValue(pak, P4kKey);
            foreach (ZipEntry entry in pak)
            {
                //files.Add(new FileMetadata
                //{
                //    Path = entry.Name,
                //    Compressed = true,
                //    Crypted = entry.IsCrypted,
                //    PackedSize = entry.CompressedSize,
                //    FileSize = entry.Size,
                //    Tag = entry,
                //});
                using (var s = pak.GetInputStream(entry))
                {
                    var bytes = s.ReadAllBytes();
                }
            }
            return Task.CompletedTask;
        }
    }
}