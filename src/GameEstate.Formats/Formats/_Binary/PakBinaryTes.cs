using GameEstate.Core;
using GameEstate.Graphics.DirectX;
using ICSharpCode.SharpZipLib.Lzw;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    public class PakBinaryTes : PakBinary
    {
        public static readonly PakBinary Instance = new PakBinaryTes();
        PakBinaryTes() { }

        // Header : TES3
        #region Header : TES3
        // http://en.uesp.net/wiki/Tes3Mod:BSA_File_Format

        // Default header data
        const uint MW_BSAHEADER_FILEID = 0x00000100; // Magic for Morrowind BSA

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        struct MW_Header
        {
            public uint HashOffset;         // Offset of hash table minus header size (12)
            public uint FileCount;          // Number of files in the archive
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        struct MW_HeaderFile
        {
            public uint FileSize;           // File size
            public uint FileOffset;         // File offset relative to data position
            public uint Size => FileSize > 0 ? FileSize & 0x3FFFFFFF : 0; // The size of the file inside the BSA
        }

        #endregion

        // Header : TES4
        #region Header : TES4
        // http://en.uesp.net/wiki/Tes4Mod:BSA_File_Format

        // Default header data
        const uint OB_BSAHEADER_FILEID = 0x00415342; // Magic for Oblivion BSA, the literal string "BSA\0".
        const uint OB_BSAHEADER_VERSION = 0x67; // Version number of an Oblivion BSA
        const uint F3_BSAHEADER_VERSION = 0x68; // Version number of a Fallout 3 BSA
        const uint SSE_BSAHEADER_VERSION = 0x69; // Version number of a Skyrim SE BSA

        // Archive flags
        const ushort OB_BSAARCHIVE_PATHNAMES = 0x0001; // Whether the BSA has names for paths
        const ushort OB_BSAARCHIVE_FILENAMES = 0x0002; // Whether the BSA has names for files
        const ushort OB_BSAARCHIVE_COMPRESSFILES = 0x0004; // Whether the files are compressed
        const ushort F3_BSAARCHIVE_PREFIXFULLFILENAMES = 0x0100; // Whether the name is prefixed to the data?

        // File flags
        //const ushort OB_BSAFILE_NIF = 0x0001; // Set when the BSA contains NIF files (Meshes)
        //const ushort OB_BSAFILE_DDS = 0x0002; // Set when the BSA contains DDS files (Textures)
        //const ushort OB_BSAFILE_XML = 0x0004; // Set when the BSA contains XML files (Menus)
        //const ushort OB_BSAFILE_WAV = 0x0008; // Set when the BSA contains WAV files (Sounds)
        //const ushort OB_BSAFILE_MP3 = 0x0010; // Set when the BSA contains MP3 files (Voices)
        //const ushort OB_BSAFILE_TXT = 0x0020; // Set when the BSA contains TXT files (Shaders)
        //const ushort OB_BSAFILE_HTML = 0x0020; // Set when the BSA contains HTML files
        //const ushort OB_BSAFILE_BAT = 0x0020; // Set when the BSA contains BAT files
        //const ushort OB_BSAFILE_SCC = 0x0020; // Set when the BSA contains SCC files
        //const ushort OB_BSAFILE_SPT = 0x0040; // Set when the BSA contains SPT files (Trees)
        //const ushort OB_BSAFILE_TEX = 0x0080; // Set when the BSA contains TEX files
        //const ushort OB_BSAFILE_FNT = 0x0080; // Set when the BSA contains FNT files (Fonts)
        //const ushort OB_BSAFILE_CTL = 0x0100; // Set when the BSA contains CTL files (Miscellaneous)

        // Bitmasks for the size field in the header
        const uint OB_BSAFILE_SIZEMASK = 0x3FFFFFFF; // Bit mask with OB_HeaderFile:SizeFlags to get the size of the file
        const uint OB_BSAFILE_SIZECOMPRESS = 0xC0000000; // Bit mask with OB_HeaderFile:SizeFlags to get the compression status

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        struct OB_Header
        {
            public uint Version;            // 04
            public uint FolderRecordOffset; // Offset of beginning of folder records
            public uint ArchiveFlags;       // Archive flags
            public uint FolderCount;        // Total number of folder records (OBBSAFolderInfo)
            public uint FileCount;          // Total number of file records (OBBSAFileInfo)
            public uint FolderNameLength;   // Total length of folder names
            public uint FileNameLength;     // Total length of file names
            public uint FileFlags;          // File flags
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        struct OB_HeaderFolder
        {
            public ulong Hash;              // Hash of the folder name
            public uint FileCount;          // Number of files in folder
            public uint Unk;
            public ulong Offset;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        struct OB_HeaderFolder2
        {
            public ulong Hash;              // Hash of the folder name
            public uint FileCount;          // Number of files in folder
            public uint Offset;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        struct OB_HeaderFile
        {
            public ulong Hash;              // Hash of the filename
            public uint SizeFlags;          // Size of the data, possibly with OB_BSAFILE_SIZECOMPRESS set
            public uint Offset;             // Offset to raw file data
            // Whether the file is compressed inside the BSA
            public int GetCompressed(bool compressToggle) => SizeFlags > 0 && (SizeFlags & OB_BSAFILE_SIZECOMPRESS) != 0 ^ compressToggle ? 1 : 0;
            // The size of the file inside the BSA
            public long GetFileSize(FileMetadata file) => SizeFlags > 0 ?
                // Skyrim and earlier
                (long)(SizeFlags & OB_BSAFILE_SIZEMASK) :
                // Not correct for texture BA2s
                file.PackedSize == 0 ? file.FileSize : file.PackedSize;
        }

        #endregion

        // Header : TES5
        #region Header : TES5
        // http://en.uesp.net/wiki/Tes5Mod:Archive_File_Format

        // Default header data
        const uint F4_BSAHEADER_FILEID = 0x58445442; // Magic for Fallout 4 BA2, the literal string "BTDX".
        const uint F4_BSAHEADER_VERSION = 0x01; // Version number of a Fallout 4 BA2
        const uint F4_HEADERTYPE_GNRL = 0x4c524e47;
        const uint F4_HEADERTYPE_DX10 = 0x30315844;

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        struct F4_Header
        {
            public uint Version;            // 04
            public uint Type;               // 08 GNRL=General, DX10=Textures
            public uint NumFiles;           // 0C
            public ulong NameTableOffset;   // 10 - relative to start of file
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        unsafe struct F4_HeaderFile
        {
            public uint NameHash;           // 00
            public fixed byte Ext[4];       // 04 - extension
            public uint DirHash;            // 08
            public uint Unk0C;              // 0C - flags? 00100100
            public ulong Offset;            // 10 - relative to start of file
            public uint PackedSize;         // 18 - packed length (zlib)
            public uint UnpackedSize;       // 1C - unpacked length
            public uint Unk20;              // 20 - BAADF00D
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        unsafe struct F4_HeaderFile2
        {
            public uint NameHash;           // 00
            public fixed byte Ext[4];       // 04 - extension
            public uint DirHash;            // 08
            public byte Unk0C;              // 0C
            public byte NumChunks;          // 0D
            public ushort ChunkHeaderSize;  // 0E - size of one chunk header
            public ushort Height;           // 10
            public ushort Width;            // 12
            public byte NumMips;            // 14
            public byte Format;             // 15 - DXGI_FORMAT
            public ushort Unk16;            // 16 - 0800
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        struct F4_HeaderTexChunk
        {
            public ulong Offset;            // 00
            public uint PackedSize;         // 08
            public uint UnpackedSize;       // 0C
            public ushort StartMip;         // 10
            public ushort EndMip;           // 12
            public uint Unk14;              // 14 - BAADFOOD
        }

        #endregion

        public unsafe override Task ReadAsync(BinaryPakFile source, BinaryReader r, ReadStage stage)
        {
            if (!(source is BinaryPakMultiFile multiSource))
                throw new NotSupportedException();
            if (stage != ReadStage.File)
                throw new ArgumentOutOfRangeException(nameof(stage), stage.ToString());

            FileMetadata[] files;
            var Magic = r.ReadUInt32();
            if (Magic == F4_BSAHEADER_FILEID)
            {
                var header = r.ReadT<F4_Header>(sizeof(F4_Header));
                if (header.Version != F4_BSAHEADER_VERSION)
                    throw new FileFormatException("BAD MAGIC");
                source.Version = header.Version;
                multiSource.Files = files = new FileMetadata[header.NumFiles];
                if (header.Type == F4_HEADERTYPE_GNRL) // General BA2 Format
                {
                    var headerFiles = r.ReadTArray<F4_HeaderFile>(sizeof(F4_HeaderFile), (int)header.NumFiles);
                    for (var i = 0; i < headerFiles.Length; i++)
                    {
                        var headerFile = headerFiles[i];
                        files[i] = new FileMetadata
                        {
                            PackedSize = headerFile.PackedSize,
                            FileSize = headerFile.UnpackedSize,
                            Position = (long)headerFile.Offset,
                        };
                    }
                }
                else if (header.Type == F4_HEADERTYPE_DX10) // Texture BA2 Format
                    for (var i = 0; i < header.NumFiles; i++)
                    {
                        var headerFile = r.ReadT<F4_HeaderFile2>(sizeof(F4_HeaderFile2));
                        var headerTexChunks = r.ReadTArray<F4_HeaderTexChunk>(sizeof(F4_HeaderTexChunk), headerFile.NumChunks);
                        var firstChunk = headerTexChunks[0];
                        files[i] = new FileMetadata
                        {
                            FileInfo = headerFile,
                            PackedSize = firstChunk.PackedSize,
                            FileSize = firstChunk.UnpackedSize,
                            Position = (long)firstChunk.Offset,
                            Tag = headerTexChunks,
                        };
                    }
                r.Position((long)header.NameTableOffset);
                for (var i = 0; i < header.NumFiles; i++)
                    files[i].Path = r.ReadL16ASCII().Replace('\\', '/');
            }
            else if (Magic == OB_BSAHEADER_FILEID)
            {
                var header = r.ReadT<OB_Header>(sizeof(OB_Header));
                if (header.Version != OB_BSAHEADER_VERSION && header.Version != F3_BSAHEADER_VERSION && header.Version != SSE_BSAHEADER_VERSION)
                    throw new FileFormatException("BAD MAGIC");
                source.Version = header.Version;

                // Calculate some useful values
                if ((header.ArchiveFlags & OB_BSAARCHIVE_PATHNAMES) == 0 || (header.ArchiveFlags & OB_BSAARCHIVE_FILENAMES) == 0)
                    throw new FileFormatException("HEADER FLAGS");
                var compressToggle = (header.ArchiveFlags & OB_BSAARCHIVE_COMPRESSFILES) != 0;
                if (header.Version == F3_BSAHEADER_VERSION || header.Version == SSE_BSAHEADER_VERSION)
                    source.Params["namePrefix"] = (header.ArchiveFlags & F3_BSAARCHIVE_PREFIXFULLFILENAMES) != 0 ? "Y" : "N";
                var folderSize = header.Version != SSE_BSAHEADER_VERSION ? 16 : 24;

                // Create file metadatas
                multiSource.Files = files = new FileMetadata[header.FileCount];
                var filenamesPosition = header.FolderRecordOffset + header.FolderNameLength + header.FolderCount * (folderSize + 1) + header.FileCount * 16;
                r.Position(filenamesPosition);
                var buf = new List<byte>(100);
                for (var i = 0; i < header.FileCount; i++)
                    files[i] = new FileMetadata
                    {
                        Path = r.ReadZASCII(1000, buf).Replace('\\', '/'),
                    };
                if (r.Position() != filenamesPosition + header.FileNameLength)
                    throw new FileFormatException("HEADER FILENAMES");

                // read-all folders
                r.Position(header.FolderRecordOffset);
                var foldersFiles = new uint[header.FolderCount];
                if (header.Version == SSE_BSAHEADER_VERSION)
                {
                    var folders = r.ReadTArray<OB_HeaderFolder>(sizeof(OB_HeaderFolder), (int)header.FolderCount);
                    for (var i = 0; i < header.FolderCount; i++)
                        foldersFiles[i] = folders[i].FileCount;
                }
                else
                {
                    var folders = r.ReadTArray<OB_HeaderFolder2>(sizeof(OB_HeaderFolder2), (int)header.FolderCount);
                    for (var i = 0; i < header.FolderCount; i++)
                        foldersFiles[i] = folders[i].FileCount;
                }

                // add file
                var fileNameIndex = 0U;
                for (var i = 0; i < header.FolderCount; i++)
                {
                    var folder_name = r.ReadASCII(r.ReadByte(), ASCIIFormat.PossiblyNullTerminated).Replace('\\', '/');
                    var headerFiles = r.ReadTArray<OB_HeaderFile>(sizeof(OB_HeaderFile), (int)foldersFiles[i]);
                    foreach (var headerFile in headerFiles)
                    {
                        var file = files[fileNameIndex++];
                        file.Path = $"{folder_name}/{file.Path}";
                        file.Compressed = headerFile.GetCompressed(compressToggle);
                        file.FileSize = headerFile.GetFileSize(file);
                        file.Position = headerFile.Offset;
                    }
                }
            }
            else if (Magic == MW_BSAHEADER_FILEID)
            {
                var header = r.ReadT<MW_Header>(sizeof(MW_Header));

                // Calculate some useful values
                var headerSize = r.Position();
                var hashTablePosition = headerSize + header.HashOffset;
                var fileDataPostion = hashTablePosition + (8 * header.FileCount);

                // Create file metadatas
                multiSource.Files = files = new FileMetadata[header.FileCount];
                var headerFiles = r.ReadTArray<MW_HeaderFile>(sizeof(MW_HeaderFile), (int)header.FileCount);
                for (var i = 0; i < header.FileCount; i++)
                {
                    var headerFile = headerFiles[i];
                    files[i] = new FileMetadata
                    {
                        FileSize = headerFile.Size,
                        Position = fileDataPostion + headerFile.FileOffset,
                    };
                }

                // Read filename offsets
                var filenameOffsets = r.ReadTArray<uint>(sizeof(uint), (int)header.FileCount); // relative offset in filenames section

                // Read filenames
                var filenamesPosition = r.Position();
                var buf = new List<byte>(64);
                for (var i = 0; i < header.FileCount; i++)
                {
                    r.Position(filenamesPosition + filenameOffsets[i]);
                    files[i].Path = r.ReadZASCII(1000, buf).Replace('\\', '/');
                }
            }
            else throw new InvalidOperationException("BAD MAGIC");
            return Task.CompletedTask;
        }

        public unsafe override Task WriteAsync(BinaryPakFile source, BinaryWriter w, WriteStage stage) => throw new NotImplementedException();

        public unsafe override Task<byte[]> ReadFileAsync(BinaryPakFile source, BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception = null)
        {
            var fileSize = (int)file.FileSize;
            byte[] fileData;
            int newFileSize;
            r.Position(file.Position);
            if (source.Params.TryGetValue("namePrefix", out var z) && z == "Y")
            {
                var prefixLength = r.ReadByte() + 1;
                fileSize -= prefixLength;
                r.Position(file.Position + prefixLength);
            }
            fileData = r.ReadBytes(fileSize);
            // BSA
            if (file.Compressed != 0)
            {
                newFileSize = source.Version != SSE_BSAHEADER_VERSION ? r.ReadInt32() - 4 : fileSize;
                var newFileData = new byte[newFileSize];
                if (source.Version != SSE_BSAHEADER_VERSION)
                {
                    if (fileData.Length > 4)
                        using (var s = new MemoryStream(fileData, 4, fileSize - 4))
                        using (var gs = new InflaterInputStream(s))
                            gs.Read(newFileData, 0, newFileData.Length);
                    else newFileData = fileData;
                }
                else
                {
                    using (var s = new MemoryStream(fileData))
                    using (var gs = new Lzw​Input​Stream(s))
                        gs.Read(newFileData, 0, newFileData.Length);
                }
                fileData = newFileData;
            }
            // General BA2
            else if (file.PackedSize > 0 && file.Tag == null)
            {
                var newFileData = new byte[file.FileSize];
                using (var s = new MemoryStream(fileData))
                using (var gs = new InflaterInputStream(s))
                    gs.Read(newFileData, 0, newFileData.Length);
                fileData = newFileData;
            }
            // Fill DDS Header
            else if (file.Tag != null)
            {
                var info = (F4_HeaderFile2)file.FileInfo;
                var ddsHeader = new DDS_HEADER
                {
                    dwFlags = DDSFlags.HEADER_FLAGS_TEXTURE | DDSFlags.HEADER_FLAGS_LINEARSIZE | DDSFlags.HEADER_FLAGS_MIPMAP,
                    dwHeight = info.Height,
                    dwWidth = info.Width,
                    dwMipMapCount = info.NumMips,
                    dwCaps = DDSCaps.SURFACE_FLAGS_TEXTURE | DDSCaps.SURFACE_FLAGS_MIPMAP,
                    dwCaps2 = info.Unk16 == 2049 ? DDSCaps2.CUBEMAP_ALLFACES : 0,
                };
                var dx10Header = new DDS_HEADER_DXT10();
                var dx10 = false;
                // map tex format
                switch ((DXGI_FORMAT)info.Format)
                {
                    case DXGI_FORMAT.BC1_UNORM:
                        ddsHeader.ddspf.dwFlags = DDPF.FourCC;
                        ddsHeader.ddspf.dwFourCC = DDS_HEADER.Literal.DXT1;
                        ddsHeader.dwPitchOrLinearSize = (uint)info.Width * info.Height / 2U; // 4bpp
                        break;
                    case DXGI_FORMAT.BC2_UNORM:
                        ddsHeader.ddspf.dwFlags = DDPF.FourCC;
                        ddsHeader.ddspf.dwFourCC = DDS_HEADER.Literal.DXT3;
                        ddsHeader.dwPitchOrLinearSize = (uint)info.Width * info.Height; // 8bpp
                        break;
                    case DXGI_FORMAT.BC3_UNORM:
                        ddsHeader.ddspf.dwFlags = DDPF.FourCC;
                        ddsHeader.ddspf.dwFourCC = DDS_HEADER.Literal.DXT5;
                        ddsHeader.dwPitchOrLinearSize = (uint)info.Width * info.Height; // 8bpp
                        break;
                    case DXGI_FORMAT.BC5_UNORM:
                        ddsHeader.ddspf.dwFlags = DDPF.FourCC;
                        ddsHeader.ddspf.dwFourCC = DDS_HEADER.Literal.ATI2;
                        ddsHeader.dwPitchOrLinearSize = (uint)info.Width * info.Height; // 8bpp
                        break;
                    case DXGI_FORMAT.BC7_UNORM:
                        ddsHeader.ddspf.dwFlags = DDPF.FourCC;
                        ddsHeader.ddspf.dwFourCC = DDS_HEADER.Literal.DX10;
                        //fixed (byte* p = DDS_HEADER.Literal.DX10)
                        //    Buffer.MemoryCopy(ddsHeader.ddspf.dwFourCC, p, 4, 4);
                        ddsHeader.dwPitchOrLinearSize = (uint)info.Width * info.Height; // 8bpp
                        dx10 = true;
                        dx10Header.dxgiFormat = DXGI_FORMAT.BC7_UNORM;
                        break;
                    case DXGI_FORMAT.B8G8R8A8_UNORM:
                        ddsHeader.ddspf.dwFlags = DDPF.RGB | DDPF.AlphaPixels;
                        ddsHeader.ddspf.dwRGBBitCount = 32;
                        ddsHeader.ddspf.dwRBitMask = 0x00FF0000;
                        ddsHeader.ddspf.dwGBitMask = 0x0000FF00;
                        ddsHeader.ddspf.dwBBitMask = 0x000000FF;
                        ddsHeader.ddspf.dwABitMask = 0xFF000000;
                        ddsHeader.dwPitchOrLinearSize = (uint)info.Width * info.Height * 4; // 32bpp
                        break;
                    case DXGI_FORMAT.R8_UNORM:
                        ddsHeader.ddspf.dwFlags = DDPF.RGB;
                        ddsHeader.ddspf.dwRGBBitCount = 8;
                        ddsHeader.ddspf.dwRBitMask = 0xFF;
                        ddsHeader.dwPitchOrLinearSize = (uint)info.Width * info.Height; // 8bpp
                        break;
                    default: throw new InvalidOperationException("DDS FAILED");
                }
                if (dx10)
                {
                    dx10Header.resourceDimension = D3D10_RESOURCE_DIMENSION.Texture2D;
                    dx10Header.miscFlag = 0;
                    dx10Header.arraySize = 1;
                    dx10Header.miscFlags2 = 0;
                    //dx10Header.Write(null);
                    //char dds2[sizeof(dx10Header)];
                    //memcpy(dds2, &dx10Header, sizeof(dx10Header));
                    //content.append(QByteArray::fromRawData(dds2, sizeof(dx10Header)));
                }
            }
            return Task.FromResult(fileData);
        }

        public override Task WriteFileAsync(BinaryPakFile source, BinaryWriter w, FileMetadata file, byte[] data, Action<FileMetadata, string> exception = null) => throw new NotImplementedException();
    }
}