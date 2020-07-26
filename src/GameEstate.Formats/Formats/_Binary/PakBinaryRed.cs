using Compression;
using Compression.Doboz;
using Dolkens.Framework.Extensions;
using GameEstate.Core;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using K4os.Compression.LZ4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    // https://witcher.fandom.com/wiki/File_format
    // https://witcher.fandom.com/wiki/Extracting_the_original_files
    // https://witcher.fandom.com/wiki/Extracting_The_Witcher_2_files
    // https://github.com/JLouis-B/RedTools
    // https://github.com/yole/Gibbed.RED
    public class PakBinaryRed : PakBinary
    {
        public static readonly PakBinary Instance = new PakBinaryRed();
        PakBinaryRed() { }

        // Headers : KEY/BIF (Witcher)
        #region Headers : KEY/BIF

        // https://witcher.fandom.com/wiki/KEY_BIF_V1.1_format

        const uint KEY_MAGIC = 0x2059454b;
        const uint KEY_VERSION = 0x312e3156;

        const uint BIFF_MAGIC = 0x46464942;
        internal const uint BIFF_VERSION = 0x312e3156;

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        unsafe struct KEY_Header
        {
            public uint Version;            // Version ("V1.1")
            public uint NumFiles;           // Number of entries in FILETABLE
            public uint NumKeys;            // Number of entries in KEYTABLE.
            public uint NotUsed01;          // Not used
            public uint FilesPosition;      // Offset to FILETABLE (0x440000).
            public uint KeysPosition;       // Offset to KEYTABLE.
            public uint BuildYear;          // Build year (less 1900).
            public uint BuildDay;           // Build day
            public fixed byte NotUsed02[32]; // Not used
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        struct KEY_HeaderFile
        {
            public uint FileSize;           // BIF Filesize
            public uint FileNameOffset;     // Offset To BIF name
            public uint FileNameSize;       // Size of BIF name
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        unsafe struct KEY_HeaderKey
        {
            public fixed byte Name[16];     // Null-padded string Resource Name (sans extension).
            public ushort ResourceType;     // Resource Type
            public uint ResourceId;         // Resource ID
            public uint Flags;              // Flags (BIF index is in this value, (flags & 0xFFF00000) >> 20). The rest appears to define 'fixed' index.
            public uint Id => (Flags & 0xFFF00000) >> 20; // BIF index
        }

        class SubPakFile : BinaryPakMultiFile
        {
            public SubPakFile(string filePath, string game, object tag = null) : base(filePath, game, Instance, tag)
            {
                Open();
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        struct BIFF_Header
        {
            public uint Version;            // Version ("V1.1")
            public uint NumFiles;           // Resource Count
            public uint NotUsed01;          // Not used
            public uint FilesPosition;      // Offset to RESOURCETABLE (0x14000000).
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        struct BIFF_HeaderFile
        {
            public uint FileId;             // Resource ID
            public uint Flags;              // Flags (BIF index is now in this value, (flags & 0xFFF00000) >> 20). The rest appears to define 'fixed' index.
            public uint FilePosition;       // Offset to Resource Data.
            public uint FileSize;           // Size of Resource Data.
            public ushort FileType;         // Resource Type
            public ushort NotUsed01;        // Not used
            public uint Id => (Flags & 0xFFF00000) >> 20; // BIF index
        }

        static Dictionary<int, string> BIFF_FileTypes_Cache;
        Dictionary<int, string> BIFF_FileTypes => BIFF_FileTypes_Cache ?? (BIFF_FileTypes_Cache = new Dictionary<int, string> {
            {0x0000, "res"}, // Misc. GFF resources
            {0x0001, "bmp"}, // Microsoft Windows Bitmap
            {0x0002, "mve"},
            {0x0003, "tga"}, // Targa Graphics Format
            {0x0004, "wav"}, // Wave

            {0x0006, "plt"}, // Bioware Packed Layer Texture
            {0x0007, "ini"}, // Windows INI
            {0x0008, "mp3"}, // MP3
            {0x0009, "mpg"}, // MPEG
            {0x000A, "txt"}, // Text file
            {0x000B, "xml"},

            {0x07D0, "plh"},
            {0x07D1, "tex"},
            {0x07D2, "mdl"}, // Model
            {0x07D3, "thg"},

            {0x07D5, "fnt"}, // Font

            {0x07D7, "lua"}, // Lua script source code
            {0x07D8, "slt"},
            {0x07D9, "nss"}, // NWScript source code
            {0x07DA, "ncs"}, // NWScript bytecode
            {0x07DB, "mod"}, // Module
            {0x07DC, "are"}, // Area (GFF)
            {0x07DD, "set"}, // Tileset (unused in KOTOR?)
            {0x07DE, "ifo"}, // Module information
            {0x07DF, "bic"}, // Character sheet (unused)
            {0x07E0, "wok"}, // Walk-mesh
            {0x07E1, "2da"}, // 2-dimensional array
            {0x07E2, "tlk"}, // conversation file

            {0x07E6, "txi"}, // Texture information
            {0x07E7, "git"}, // Dynamic area information, game instance file, all area and objects that are scriptable
            {0x07E8, "bti"},
            {0x07E9, "uti"}, // item blueprint
            {0x07EA, "btc"},
            {0x07EB, "utc"}, // Creature blueprint

            {0x07ED, "dlg"}, // Dialogue
            {0x07EE, "itp"}, // tile blueprint pallet file
            {0x07EF, "btt"},
            {0x07F0, "utt"}, // trigger blueprint
            {0x07F1, "dds"}, // compressed texture file
            {0x07F2, "bts"},
            {0x07F3, "uts"}, // sound blueprint
            {0x07F4, "ltr"}, // letter combo probability info
            {0x07F5, "gff"}, // Generic File Format
            {0x07F6, "fac"}, // faction file
            {0x07F7, "bte"},
            {0x07F8, "ute"}, // encounter blueprint
            {0x07F9, "btd"},
            {0x07FA, "utd"}, // door blueprint
            {0x07FB, "btp"},
            {0x07FC, "utp"}, // placeable object blueprint
            {0x07FD, "dft"}, // default values file (text-ini)
            {0x07FE, "gic"}, // game instance comments
            {0x07FF, "gui"}, // GUI definition (GFF)
            {0x0800, "css"},
            {0x0801, "ccs"},
            {0x0802, "btm"},
            {0x0803, "utm"}, // store merchant blueprint
            {0x0804, "dwk"}, // door walkmesh
            {0x0805, "pwk"}, // placeable object walkmesh
            {0x0806, "btg"},

            {0x0808, "jrl"}, // Journal
            {0x0809, "sav"}, // Saved game (ERF)
            {0x080A, "utw"}, // waypoint blueprint
            {0x080B, "4pc"},
            {0x080C, "ssf"}, // sound set file

            {0x080F, "bik"}, // movie file (bik format)
            {0x0810, "ndb"}, // script debugger file
            {0x0811, "ptm"}, // plot manager/plot instance
            {0x0812, "ptt"}, // plot wizard blueprint
            {0x0813, "ncm"},
            {0x0814, "mfx"},
            {0x0815, "mat"},
            {0x0816, "mdb"}, // not the standard MDB, multiple file formats present despite same type
            {0x0817, "say"},
            {0x0818, "ttf"}, // standard .ttf font files
            {0x0819, "ttc"},
            {0x081A, "cut"}, // cutscene? (GFF)
            {0x081B, "ka"},  // karma file (XML)
            {0x081C, "jpg"}, // jpg image
            {0x081D, "ico"}, // standard windows .ico files
            {0x081E, "ogg"}, // ogg vorbis sound file
            {0x081F, "spt"},
            {0x0820, "spw"},
            {0x0821, "wfx"}, // woot effect class (XML)
            {0x0822, "ugm"}, // 2082 ?? [textures00.bif]
            {0x0823, "qdb"}, // quest database (GFF v3.38)
            {0x0824, "qst"}, // quest (GFF)
            {0x0825, "npc"}, // spawn point? (GFF)
            {0x0826, "spn"},
            {0x0827, "utx"},
            {0x0828, "mmd"},
            {0x0829, "smm"},
            {0x082A, "uta"}, // uta (GFF)
            {0x082B, "mde"},
            {0x082C, "mdv"},
            {0x082D, "mda"},
            {0x082E, "mba"},
            {0x082F, "oct"},
            {0x0830, "bfx"},
            {0x0831, "pdb"},
            {0x0832, "TheWitcherSave"},
            {0x0833, "pvs"},
            {0x0834, "cfx"},
            {0x0835, "luc"}, // compiled lua script

            {0x0837, "prb"},
            {0x0838, "cam"},
            {0x0839, "vds"},
            {0x083A, "bin"},
            {0x083B, "wob"},
            {0x083C, "api"},
            {0x083D, "properties"},
            {0x083E, "png"},

            {0x270B, "big"},

            {0x270D, "erf"}, // Encapsulated Resource Format
            {0x270E, "bif"},
            {0x270F, "key"},
        });

        #endregion

        // Headers : DZIP (Witcher 2)
        #region Headers : DZIP

        const uint DZIP_MAGIC = 0x50495a44;
        internal const uint DZIP_VERSION = 0x50495a44;

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        struct DZIP_Header
        {
            public uint Version;            //
            public uint NumFiles;           //
            public uint Unk02;              //
            public ulong FilesPosition;     //
            public ulong Hash;              //
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        struct DZIP_HeaderFile
        {
            public DateTime Time => DateTime.FromFileTime((long)Timestamp);
            public ulong Timestamp;          // 
            public ulong FileSize;          // 
            public ulong Position;          // 
            public ulong PackedSize;        // 
        }

        #endregion

        // Headers : BUNDLE (Witcher 3)
        #region Headers : BUNDLE

        const uint BUNDLE_MAGIC = 0x41544f50; const uint BUNDLE_MAGIC2 = 0x30374f54; // POTATO70

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        unsafe struct BUNDLE_Header
        {
            public uint FileSize;           // 
            public uint NotUsed01;          // 
            public uint DataPosition;       // 
            public int NumFiles => (int)(DataPosition / sizeof(BUNDLE_HeaderFile));
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        unsafe struct BUNDLE_HeaderFile
        {
            public fixed byte Name[0x100];  //
            public fixed byte Hash[16];     //
            public uint NotUsed01;          //
            public uint FileSize;           //
            public uint PackedSize;         //
            public uint FilePosition;       //
            public ulong Timestamp;         //
            public fixed byte NotUsed02[16]; //
            public uint NotUsed03;          //
            public uint Compressed;         //
        }

        #endregion

        // Headers : CACHE (Witcher 3)
        #region Headers : CACHE

        const uint CS3W_MAGIC = 0x57335343;

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        struct CACHE_CS3W_Header
        {
            public ulong NotUsed01;         // Not used
            public ulong InfoPosition;      // Offset to Info
            public ulong NumFiles;          // Number of entries in FILETABLE
            public ulong NameOffset;        // Offset to Names
            public ulong NameSize;          // Size of to Names
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        struct CACHE_CS3W_HeaderV1
        {
            public ulong NotUsed01;         // Not used
            public uint InfoPosition;       // Offset to Info
            public uint NumFiles;           // Number of entries in FILETABLE
            public uint NameOffset;         // Offset to Names
            public uint NameSize;           // Size of to Names

            public CACHE_CS3W_Header ToHeader() => new CACHE_CS3W_Header
            {
                NotUsed01 = NotUsed01,
                InfoPosition = InfoPosition,
                NumFiles = NumFiles,
                NameOffset = NameOffset,
                NameSize = NameSize,
            };
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        struct CS3W_HeaderFileV1
        {
            public uint NamePosition;       // 
            public uint Position;           // 
            public uint Size;               // 
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        struct CS3W_HeaderFileV2
        {
            public ulong NamePosition;      // 
            public ulong Position;          // 
            public ulong Size;              // 
        }

        #endregion

        //readonly object Tag;
        //public PakBinaryRed(object tag = null) => Tag = tag;

        public unsafe override Task ReadAsync(BinaryPakFile source, BinaryReader r, ReadStage stage)
        {
            if (!(source is BinaryPakMultiFile multiSource))
                throw new NotSupportedException();
            if (stage != ReadStage.File)
                throw new ArgumentOutOfRangeException(nameof(stage), stage.ToString());

            FileMetadata[] files;
            var ext = Path.GetExtension(source.FilePath);
            var magic = r.ReadUInt32();
            // KEY
            if (magic == KEY_MAGIC) // Signature("KEY ")
            {
                var header = r.ReadT<KEY_Header>(sizeof(KEY_Header));
                if (header.Version != KEY_VERSION)
                    throw new InvalidOperationException("BAD MAGIC");
                source.Version = header.Version;
                multiSource.Files = files = new FileMetadata[header.NumFiles];

                // keys
                var keys = new Dictionary<(uint, uint), string>();
                r.Position(header.KeysPosition);
                var infoKeys = r.ReadTArray<KEY_HeaderKey>(sizeof(KEY_HeaderKey), (int)header.NumKeys);
                for (var i = 0; i < header.NumKeys; i++)
                {
                    var infoKey = infoKeys[i];
                    keys.Add((infoKey.Id, infoKey.ResourceId), UnsafeUtils.ReadZASCII(infoKey.Name, 16));
                }

                // files
                r.Position(header.FilesPosition);
                var headerFiles = r.ReadTArray<KEY_HeaderFile>(sizeof(KEY_HeaderFile), (int)header.NumFiles);
                var newPathPattern = Path.Combine(Path.GetDirectoryName(source.FilePath), "{0}");
                for (var i = 0; i < header.NumFiles; i++)
                {
                    var headerFile = headerFiles[i];
                    r.Position(headerFile.FileNameOffset);
                    var fileName = r.ReadASCII((int)headerFile.FileNameSize);
                    var newPath = string.Format(newPathPattern, fileName);
                    if (!File.Exists(newPath))
                        continue;
                    files[i] = new FileMetadata
                    {
                        Path = fileName,
                        FileSize = headerFile.FileSize,
                        Pak = new SubPakFile(newPath, source.Game, (keys, (uint)i)),
                    };
                }
            }
            // BIFF
            else if (magic == BIFF_MAGIC) // Signature("BIFF")
            {
                if (source.Tag == null)
                    throw new InvalidOperationException("BIFF files can only be processed through KEY files");
                var (keys, bifId) = ((Dictionary<(uint, uint), string> keys, uint bifId))source.Tag;
                var header = r.ReadT<BIFF_Header>(sizeof(BIFF_Header));
                if (header.Version != BIFF_VERSION)
                    throw new InvalidOperationException("BAD MAGIC");
                source.Version = header.Version;
                multiSource.Files = files = new FileMetadata[header.NumFiles];

                // files
                var fileTypes = BIFF_FileTypes;
                r.Position(header.FilesPosition);
                var headerFiles = r.ReadTArray<BIFF_HeaderFile>(sizeof(BIFF_HeaderFile), (int)header.NumFiles);
                for (var i = 0; i < header.NumFiles; i++)
                {
                    var headerFile = headerFiles[i];
                    // Curiously the last resource entry of djinni.bif seem to be missing
                    if (headerFile.FileId > i)
                        continue;
                    var path = $"{(keys.TryGetValue((bifId, headerFile.FileId), out var key) ? key : $"{i}")}{(fileTypes.TryGetValue(headerFile.FileType, out var extension) ? extension : string.Empty)}";
                    files[i] = new FileMetadata
                    {
                        Path = path.Replace('\\', '/'),
                        FileSize = headerFile.FileSize,
                        Position = headerFile.FilePosition,
                    };
                }
            }
            // DZIP
            else if (magic == DZIP_MAGIC) // Signature("DZIP")
            {
                var header = r.ReadT<DZIP_Header>(sizeof(DZIP_Header));
                if (header.Version < 2)
                    throw new FormatException("unsupported version");
                source.Version = DZIP_VERSION;
                multiSource.Files = files = new FileMetadata[header.NumFiles];
                var decryptKey = source.DecryptKey as ulong?;
                r.Position((long)header.FilesPosition);
                var hash = 0x00000000FFFFFFFFUL;
                for (var i = 0; i < header.NumFiles; i++)
                {
                    string fileName;
                    if (decryptKey == null)
                        fileName = r.ReadL16ASCII(true);
                    else
                    {
                        var nameBytes = r.ReadBytes(r.ReadUInt16());
                        for (var j = 0; j < nameBytes.Length; j++)
                        {
                            nameBytes[j] ^= (byte)(decryptKey >> j % 8);
                            decryptKey *= 0x00000100000001B3UL;
                        }
                        var nameBytesLength = nameBytes.Length - 1;
                        fileName = Encoding.ASCII.GetString(nameBytes, 0, nameBytes[nameBytesLength] == 0 ? nameBytesLength : nameBytes.Length);
                    }
                    var headerFile = r.ReadT<DZIP_HeaderFile>(sizeof(DZIP_HeaderFile));
                    files[i] = new FileMetadata
                    {
                        Path = fileName.Replace('\\', '/'),
                        Compressed = 1,
                        PackedSize = (long)headerFile.PackedSize,
                        FileSize = (long)headerFile.FileSize,
                        Position = (long)headerFile.Position,
                    };
                    // build digest
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        for (var j = 0; j < fileName.Length; j++)
                        {
                            hash ^= (byte)fileName[j];
                            hash *= 0x00000100000001B3UL;
                        }
                        hash ^= (ulong)fileName.Length;
                        hash *= 0x00000100000001B3UL;
                    }
                    hash ^= headerFile.Timestamp;
                    hash *= 0x00000100000001B3UL;
                    hash ^= headerFile.FileSize;
                    hash *= 0x00000100000001B3UL;
                    hash ^= headerFile.Position;
                    hash *= 0x00000100000001B3UL;
                    hash ^= headerFile.PackedSize;
                    hash *= 0x00000100000001B3UL;
                }
                if (hash != header.Hash)
                    throw new FormatException("bad entry table hash (wrong cdkey for DLC archives?)");
            }
            // BUNDLE
            else if (magic == BUNDLE_MAGIC) // Signature("POTATO70")
            {
                if (r.ReadUInt32() != BUNDLE_MAGIC2)
                    throw new InvalidOperationException("BAD MAGIC");
                var header = r.ReadT<BUNDLE_Header>(sizeof(BUNDLE_Header));
                source.Version = BUNDLE_MAGIC;
                multiSource.Files = files = new FileMetadata[header.NumFiles];

                // files
                r.Position(0x20);
                var headerFiles = r.ReadTArray<BUNDLE_HeaderFile>(sizeof(BUNDLE_HeaderFile), header.NumFiles);
                for (var i = 0; i < header.NumFiles; i++)
                {
                    var headerFile = headerFiles[i];
                    var path = UnsafeUtils.ReadZASCII(headerFile.Name, 0x100);
                    files[i] = new FileMetadata
                    {
                        Path = path.Replace('\\', '/'),
                        Compressed = (int)headerFile.Compressed,
                        FileSize = headerFile.FileSize,
                        PackedSize = headerFile.PackedSize,
                        Position = headerFile.FilePosition,
                    };
                }
            }
            // CACHE
            else if (ext == ".cache")
            {
                if (magic == CS3W_MAGIC)
                {
                    var version = r.ReadUInt32();
                    var header = version >= 2
                        ? r.ReadT<CACHE_CS3W_Header>(sizeof(CACHE_CS3W_Header))
                        : r.ReadT<CACHE_CS3W_HeaderV1>(sizeof(CACHE_CS3W_HeaderV1)).ToHeader();
                    r.Position((long)header.NameOffset);
                    var name = r.ReadASCII((int)header.NameSize);
                }
                else
                {
                    var size = r.ReadUInt32();
                    if (size == 0)
                    {

                    }
                }
            }
            else throw new FileFormatException($"Unknown File Type {magic}");
            return Task.CompletedTask;
        }

        public override Task<byte[]> ReadFileAsync(BinaryPakFile source, BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception = null)
        {
            byte[] fileData;
            r.Position(file.Position);
            if (source.Version == BIFF_VERSION)
                fileData = r.ReadBytes((int)file.FileSize);
            else if (source.Version == DZIP_VERSION)
            {
                var blocks = (int)((file.FileSize + 0xFFFF) >> 16);
                var positions = new long[blocks + 1];
                for (var i = 0; i < positions.Length; i++)
                    positions[i] = file.Position + r.ReadUInt32();
                positions[blocks] = file.Position + file.PackedSize;
                var uncompressed = new byte[0x10000];
                var bytesLeft = file.PackedSize;
                using (var s = new MemoryStream())
                {
                    for (var i = 0; i < blocks; i++)
                    {
                        r.Position(positions[i]);
                        var compressed = r.ReadBytes((int)(positions[i + 1] - positions[i + 0]));
                        var bytesRead = Lzf.Decompress(compressed, uncompressed);
                        if (i + 1 < blocks && bytesRead != uncompressed.Length)
                            throw new FileFormatException();
                        s.Write(uncompressed, 0, bytesRead);
                        bytesLeft -= bytesRead;
                    }
                    s.Position = 0;
                    if (s.Length != file.FileSize)
                        throw new InvalidOperationException();
                    fileData = s.ToArray();
                }
            }
            else if (source.Version == BUNDLE_MAGIC)
            {
                fileData = r.ReadBytes((int)file.PackedSize);
                var newFileSize = file.FileSize;
                switch (file.Compressed)
                {
                    case 0: break; // no compression
                    case 1: // zlib
                        {
                            var newFileData = new byte[newFileSize];
                            using (var s = new MemoryStream(fileData))
                            using (var gs = new InflaterInputStream(s))
                                gs.Read(newFileData, 0, newFileData.Length);
                            fileData = newFileData;
                        }
                        break;
                    case 2: // snappy
                        throw new NotImplementedException();
                    case 3: // doboz
                        fileData = DobozDecoder.Decode(fileData, 0, fileData.Length);
                        break;
                    case 4:
                    case 5: // lz4
                        {
                            var newFileData = new byte[newFileSize];
                            LZ4Codec.Decode(fileData, newFileData);
                            fileData = newFileData;
                        }
                        break;
                    default: throw new ArgumentOutOfRangeException(nameof(file.Compressed), file.Compressed.ToString());
                }
            }
            else throw new ArgumentOutOfRangeException(nameof(source.Version), $"{source.Version}");
            return Task.FromResult(fileData);
        }
    }
}