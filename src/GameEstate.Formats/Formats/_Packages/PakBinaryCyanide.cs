﻿using GameEstate.Core;
using GameEstate.Formats.All;
using GameEstate.Graphics;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace GameEstate.Formats._Packages
{
    public class PakBinaryCyanide : PakBinary
    {
        public static readonly PakBinary Instance = new PakBinaryCyanide();
        PakBinaryCyanide() { }

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

        // object factory
        static Func<BinaryReader, FileMetadata, Task<object>> ObjectFactory(string path)
        {
            switch (Path.GetExtension(path).ToLowerInvariant())
            {
                case ".dds": return BinaryDds.Factory;
                default: return null;
            }
        }

        public unsafe override Task ReadAsync(BinaryPakFile source, BinaryReader r, ReadStage stage)
        {
            if (!(source is BinaryPakManyFile multiSource))
                throw new NotSupportedException();
            if (stage != ReadStage.File)
                throw new ArgumentOutOfRangeException(nameof(stage), stage.ToString());

            var magic = source.Magic = r.ReadUInt32();
            if (magic != CPK_MAGIC)
                throw new FileFormatException($"Unknown File Type {magic}");
            var header = r.ReadT<CPK_Header>(sizeof(CPK_Header));
            var headerFiles = r.ReadTArray<CPK_HeaderFile>(sizeof(CPK_HeaderFile), (int)header.NumFiles);
            var files = multiSource.Files = new FileMetadata[header.NumFiles];
            UnsafeUtils.ReadZASCII(header.Root, 512);
            for (var i = 0; i < files.Count; i++)
            {
                var headerFile = headerFiles[i];
                var path = UnsafeUtils.ReadZASCII(headerFile.FileName, 512).Replace('\\', '/');
                files[i] = new FileMetadata
                {
                    Path = path,
                    ObjectFactory = ObjectFactory(path),
                    FileSize = headerFile.FileSize,
                    Position = (long)headerFile.Offset,
                };
            }
            return Task.CompletedTask;
        }

        public override Task<Stream> ReadDataAsync(BinaryPakFile source, BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception = null)
        {
            r.Position(file.Position);
            return Task.FromResult((Stream)new MemoryStream(r.ReadBytes((int)file.FileSize)));
        }
    }
}