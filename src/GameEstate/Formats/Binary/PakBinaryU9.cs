using GameEstate.Core;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    public class PakBinaryU9 : PakBinary
    {
        // Headers
        #region Headers
        // http://wiki.ultimacodex.com/wiki/Ultima_IX_Internal_Formats#FLX_Format

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        unsafe struct FLX_HeaderFile
        {
            public uint Position;
            public uint FileSize;
        }

        #endregion

        public unsafe override Task ReadAsync(BinaryPakFile source, BinaryReader r, ReadStage stage)
        {
            var fileName = Path.GetFileNameWithoutExtension(source.FilePath).ToLowerInvariant();
            var prefix
                = fileName.Contains("bitmap") ? "bitmap"
                : fileName.Contains("texture") ? "texture"
                : fileName.Contains("sdinfo") ? "sdinfo"
                : fileName;
            r.Position(0x50);
            var numFiles = r.ReadInt32();
            r.Position(0x80);
            var headerFiles = r.ReadTArray<FLX_HeaderFile>(sizeof(FLX_HeaderFile), numFiles);
            var files = source.Files = new FileMetadata[numFiles];
            for (var i = 0; i < files.Count; i++)
            {
                var headerFile = headerFiles[i];
                files[i] = new FileMetadata
                {
                    Path = $"{prefix}/{i}",
                    FileSize = headerFile.FileSize,
                    Position = headerFile.Position,
                };
            }
            return Task.CompletedTask;
        }

        public override Task<byte[]> ReadFileAsync(BinaryPakFile source, BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception = null)
        {
            r.Position(file.Position);
            return Task.FromResult(r.ReadBytes((int)file.FileSize));
        }
    }
}