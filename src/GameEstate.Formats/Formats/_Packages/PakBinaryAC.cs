using GameEstate.Core;
using GameEstate.Formats.AC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace GameEstate.Formats._Packages
{
    public class PakBinaryAC : PakBinary
    {
        public static readonly PakBinary Instance = new PakBinaryAC();
        PakBinaryAC() { }

        #region Headers

        const uint DAT_HEADER_OFFSET = 0x140;

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        unsafe struct Header
        {
            public uint FileType;
            public uint BlockSize;
            public uint FileSize;
            [MarshalAs(UnmanagedType.U4)] public PakType DataSet;
            public uint DataSubset;

            public uint FreeHead;
            public uint FreeTail;
            public uint FreeCount;
            public uint BTree;

            public uint NewLRU;
            public uint OldLRU;
            public uint UseLRU; // UseLRU != 0

            public uint MasterMapID;

            public uint EnginePackVersion;
            public uint GamePackVersion;

            public fixed byte VersionMajor[16];
            public uint VersionMinor;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        unsafe struct DirectoryHeader
        {
            public const int SizeOf = (sizeof(uint) * 0x3E) + sizeof(uint) + (File.SizeOf * 0x3D);
            public fixed uint Branches[0x3E];
            public uint EntryCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x3D)]
            public File[] Entries;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        unsafe struct File
        {
            public const int SizeOf = sizeof(uint) * 6;
            public uint BitFlags; // not-used
            public uint ObjectId;
            public uint FileOffset;
            public uint FileSize;
            public uint Date; // not-used
            public uint Iteration; // not-used
        }

        class Directory
        {
            public readonly DirectoryHeader Header;
            public readonly List<Directory> Directories = new List<Directory>();

            public unsafe Directory(BinaryReader r, long offset, int blockSize)
            {
                Header = ReadT<DirectoryHeader>(r, offset, DirectoryHeader.SizeOf, blockSize);
                if (Header.Branches[0] != 0)
                    for (var i = 0; i < Header.EntryCount + 1; i++)
                        Directories.Add(new Directory(r, Header.Branches[i], blockSize));
            }

            public void AddFiles(PakType dataType, IList<FileMetadata> files, string path)
            {
                var did = 0;
                Directories.ForEach(d => d.AddFiles(dataType, files, Path.Combine(path, did++.ToString())));
                for (var i = 0; i < Header.EntryCount; i++)
                {
                    var entry = Header.Entries[i];
                    var fileName = PakFileTypeHelper.GetFileName(entry.ObjectId, dataType, 0);
                    files.Add(new FileMetadata
                    {
                        Path = Path.Combine(path, fileName),
                        Position = entry.FileOffset,
                        FileSize = entry.FileSize,
                        Tag = entry,
                    });
                }
            }
        }

        #endregion

        // object factory
        static Func<BinaryReader, FileMetadata, Task<object>> ObjectFactory(string path)
        {
            switch (Path.GetExtension(path).ToLowerInvariant())
            {
                default: return null;
            }
        }

        public unsafe override Task ReadAsync(BinaryPakFile source, BinaryReader r, ReadStage stage)
        {
            if (!(source is BinaryPakManyFile multiSource))
                throw new NotSupportedException();
            if (stage != ReadStage.File)
                throw new ArgumentOutOfRangeException(nameof(stage), stage.ToString());

            var files = multiSource.Files = new List<FileMetadata>();
            r.Position(DAT_HEADER_OFFSET);
            var header = r.ReadT<Header>(sizeof(Header));
            var directory = new Directory(r, header.BTree, (int)header.BlockSize);
            directory.AddFiles(header.DataSet, files, string.Empty);
            return Task.CompletedTask;
        }

        public override Task<Stream> ReadDataAsync(BinaryPakFile source, BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception = null)
        {
            r.Position(file.Position);
            return Task.FromResult((Stream)new MemoryStream(r.ReadBytes((int)file.FileSize)));
        }

        static T ReadT<T>(BinaryReader r, long offset, int size, int blockSize) => UnsafeUtils.MarshalT<T>(ReadBytes(r, offset, size, blockSize));

        static byte[] ReadBytes(BinaryReader r, long offset, int size, int blockSize)
        {
            r.Position(offset);
            var nextAddress = (long)r.ReadUInt32();
            var buffer = new byte[size];
            var bufferOffset = 0;
            int read;
            while (size > 0)
                if (size >= blockSize)
                {
                    read = r.Read(buffer, bufferOffset, blockSize - 4);
                    bufferOffset += read;
                    size -= read;
                    r.Position(nextAddress); nextAddress = r.ReadUInt32();
                }
                else
                {
                    r.Read(buffer, bufferOffset, size);
                    return buffer;
                }
            return buffer;
        }
    }
}