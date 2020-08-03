using GameEstate.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    public class PakBinaryArkane : PakBinary
    {
        public static readonly PakBinary Instance = new PakBinaryArkane();
        PakBinaryArkane() { }

        const uint RES_MAGIC = 0x04534552;

        class SubPakFile : BinaryPakMultiFile
        {
            public SubPakFile(string filePath, string game, object tag = null) : base(filePath, game, Instance, tag)
            {
                Open();
            }
        }

        public unsafe override Task ReadAsync(BinaryPakFile source, BinaryReader r, ReadStage stage)
        {
            if (!(source is BinaryPakMultiFile multiSource))
                throw new NotSupportedException();
            if (stage != ReadStage.File)
                throw new ArgumentOutOfRangeException(nameof(stage), stage.ToString());

            if (Path.GetExtension(source.FilePath) != ".index")
                throw new ArgumentOutOfRangeException("must be index");
            if (Path.GetFileName(source.FilePath) == "master.index")
            {
                const uint SubMarker = 0x18000000;
                const uint EndMarker = 0x01000000;

                var magic = Utility.Reverse(r.ReadUInt32());
                if (magic != RES_MAGIC)
                    throw new FileFormatException("BAD MAGIC");
                r.Skip(4);
                var files2 = multiSource.Files = new List<FileMetadata>();
                var state = 0;
                do
                {
                    var nameSize = r.ReadUInt32();
                    if (nameSize == SubMarker) { state++; nameSize = r.ReadUInt32(); }
                    else if (nameSize == EndMarker) break;
                    var newPath = r.ReadASCII((int)nameSize);
                    var packId = state > 0 ? r.ReadUInt16() : 0;
                    files2.Add(new FileMetadata
                    {
                        Path = newPath,
                        Pak = new SubPakFile(newPath, source.Game),
                    });
                }
                while (true);
                return Task.CompletedTask;
            }

            var pathFile = Path.GetFileName(source.FilePath);
            var pathDir = Path.GetDirectoryName(source.FilePath);
            var resourcePath = Path.Combine(pathDir, $"{pathFile.Substring(0, pathFile.Length - 6)}.resources");
            if (!File.Exists(resourcePath))
                throw new ArgumentOutOfRangeException("Unable to find resources extension");
            var sharedResourcePath = new[] {
                "shared_2_3.sharedrsc",
                "shared_2_3_4.sharedrsc",
                "shared_1_2_3.sharedrsc",
                "shared_1_2_3_4.sharedrsc" }
                .Select(x => Path.Combine(pathDir, x)).FirstOrDefault(File.Exists);
            if (sharedResourcePath == null)
                throw new ArgumentOutOfRangeException("Unable to find Sharedrsc");

            r.Position(4);
            var mainFileSize = Utility.Reverse(r.ReadUInt32()); // mainFileSize
            r.Skip(24);
            var numFiles = Utility.Reverse(r.ReadUInt32());
            var files = multiSource.Files = new FileMetadata[numFiles];
            for (var i = 0; i < numFiles; i++)
            {
                var id = Utility.Reverse(r.ReadUInt32());
                var tag1 = r.ReadL32ASCII();
                var tag2 = r.ReadL32ASCII();
                var fileName = r.ReadL32ASCII();
                var position = Utility.Reverse(r.ReadUInt64());
                var fileSize = Utility.Reverse(r.ReadUInt32());
                var packedSize = Utility.Reverse(r.ReadUInt32());
                r.Skip(4);
                var flags = Utility.Reverse(r.ReadUInt32());
                var flags2 = Utility.Reverse(r.ReadUInt16());
                var useSharedResources = (flags & 32) != 0 && flags2 == 0x8000;
                var newPath = !useSharedResources ? resourcePath : sharedResourcePath;
                files[i] = new FileMetadata
                {
                    Id = (int)id,
                    Path = fileName,
                    Compressed = fileSize != packedSize ? 1 : 0,
                    FileSize = fileSize,
                    PackedSize = packedSize,
                    Position = (long)position,
                    Tag = (newPath, tag1, tag2),
                };
            }
            return Task.CompletedTask;
        }

        public override Task<byte[]> ReadFileAsync(BinaryPakFile source, BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception = null)
        {
            if (file.FileSize == 0)
                return Task.FromResult(new byte[0]);
            var (path, tag1, tag2) = ((string, string, string))file.Tag;
            return Task.FromResult(source.GetBinaryReader(path).Func(r2 =>
            {
                r2.Position(file.Position);
                return file.Compressed != 0
                    ? r2.DecompressZlib((int)file.PackedSize, (int)file.FileSize)
                    : r2.ReadBytes((int)file.PackedSize);
            }));
        }
    }
}