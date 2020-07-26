using GameEstate.Core;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    public class PakBinaryArkane : PakBinary
    {
        public static readonly PakBinary Instance = new PakBinaryArkane();
        PakBinaryArkane() { }

        public unsafe override Task ReadAsync(BinaryPakFile source, BinaryReader r, ReadStage stage)
        {
            if (!(source is BinaryPakMultiFile multiSource))
                throw new NotSupportedException();
            if (stage != ReadStage.File)
                throw new ArgumentOutOfRangeException(nameof(stage), stage.ToString());

            if (Path.GetExtension(source.FilePath) != ".index")
                throw new ArgumentOutOfRangeException();
            var resourcePath = $"{source.FilePath.Substring(0, source.FilePath.Length - 6)}.resources";
            if (!File.Exists(resourcePath))
                throw new ArgumentOutOfRangeException();
            var sharedResourcePath = Path.Combine(Path.GetDirectoryName(resourcePath), "shared_2_3.sharedrsc");
            if (!File.Exists(sharedResourcePath))
                throw new ArgumentOutOfRangeException();

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
                r.Position(file.Position);
                var fileData = r2.ReadBytes((int)file.PackedSize);
                if (file.Compressed != 0)
                {
                    var newFileData = new byte[file.FileSize];
                    using (var s = new MemoryStream(fileData))
                    using (var gs = new InflaterInputStream(s))
                        gs.Read(newFileData, 0, newFileData.Length);
                    fileData = newFileData;
                }
                return fileData;
            }));
        }
    }
}