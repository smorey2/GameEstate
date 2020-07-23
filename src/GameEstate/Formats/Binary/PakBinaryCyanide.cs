using GameEstate.Core;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    public class PakBinaryCyanide : PakBinary
    {
        // Headers
        #region Headers

        const uint CPK_MAGIC = 0x01439855;

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        unsafe struct CPK_Header
        {
            public uint NumFiles;               // Number of files
            public fixed byte Root[512];        // Root name
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        unsafe struct CPK_HeaderFile
        {
            public uint FileSize;               // File size
            public ulong Offset;                // File position
            public fixed byte FileName[512];    // File name
        }

        #endregion

        public unsafe override Task ReadAsync(BinaryPakFile source, BinaryReader r, ReadStage stage)
        {
            var magic = r.ReadUInt32();
            if (magic != CPK_MAGIC)
                throw new FileFormatException($"Unknown File Type {magic}");
            var header = r.ReadT<CPK_Header>(sizeof(CPK_Header));
            var headerFiles = r.ReadTArray<CPK_HeaderFile>(sizeof(CPK_HeaderFile), (int)header.NumFiles);
            var files = source.Files = new FileMetadata[header.NumFiles];
            var root = UnsafeUtils.ReadZASCII(header.Root, 512);
            for (var i = 0; i < files.Count; i++)
            {
                var headerFile = headerFiles[i];
                files[i] = new FileMetadata
                {
                    Path = UnsafeUtils.ReadZASCII(headerFile.FileName, 512),
                    FileSize = headerFile.FileSize,
                    Position = (long)headerFile.Offset,
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