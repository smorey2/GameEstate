﻿using GameEstate.Core;
using GameEstate.Formats.Red;
using GameEstate.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    public class PakBinaryAurora : PakBinary
    {
        public static readonly PakBinary Instance = new PakBinaryAurora();
        PakBinaryAurora() { }

        // Headers : KEY/BIF
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

        class SubPakFile : BinaryPakManyFile
        {
            public SubPakFile(Estate estate, string game, string filePath, object tag = null) : base(estate, game, filePath, Instance, tag)
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

        // object factory
        static Func<BinaryReader, FileMetadata, Task<object>> ObjectFactory(string path)
        {
            Task<object> DdsFactory(BinaryReader r, FileMetadata f)
            {
                var tex = new TextureInfo();
                tex.ReadDds(r);
                return Task.FromResult((object)tex);
            }
            Task<object> BinaryPakFactory(BinaryReader r, FileMetadata f)
            {
                return Task.FromResult((object)new BinaryPak(r));   
            }
            switch (Path.GetExtension(path).ToLowerInvariant())
            {
                case ".dds": return DdsFactory;
                case ".dlg": case ".qdb": case ".qst": return BinaryPakFactory;
                default: return null;
            }
        }

        public unsafe override Task ReadAsync(BinaryPakFile source, BinaryReader r, ReadStage stage)
        {
            if (!(source is BinaryPakManyFile multiSource))
                throw new NotSupportedException();
            if (stage != ReadStage.File)
                throw new ArgumentOutOfRangeException(nameof(stage), stage.ToString());

            FileMetadata[] files; List<FileMetadata> files2;
            var magic = source.Magic = r.ReadUInt32();
            // KEY
            if (magic == KEY_MAGIC) // Signature("KEY ")
            {
                var sourceName = source.Name;
                var voiceKey = sourceName[0] == 'M' && char.IsNumber(sourceName[1]);

                var header = r.ReadT<KEY_Header>(sizeof(KEY_Header));
                if (header.Version != KEY_VERSION)
                    throw new FileFormatException("BAD MAGIC");
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
                var subPathFormat = Path.Combine(Path.GetDirectoryName(source.FilePath), !voiceKey ? "{0}" : "voices\\{0}");
                for (var i = 0; i < header.NumFiles; i++)
                {
                    var headerFile = headerFiles[i];
                    r.Position(headerFile.FileNameOffset);
                    var path = r.ReadASCII((int)headerFile.FileNameSize);
                    var subPath = string.Format(subPathFormat, path);
                    if (!File.Exists(subPath))
                        continue;
                    files[i] = new FileMetadata
                    {
                        Path = path,
                        FileSize = headerFile.FileSize,
                        Pak = new SubPakFile(source.Estate, source.Game, subPath, (keys, (uint)i)),
                    };
                }
            }
            // BIFF
            else if (magic == BIFF_MAGIC) // Signature("BIFF")
            {
                if (source.Tag == null)
                    throw new FileFormatException("BIFF files can only be processed through KEY files");
                var (keys, bifId) = ((Dictionary<(uint, uint), string> keys, uint bifId))source.Tag;
                var header = r.ReadT<BIFF_Header>(sizeof(BIFF_Header));
                if (header.Version != BIFF_VERSION)
                    throw new FileFormatException("BAD MAGIC");
                source.Version = header.Version;
                multiSource.Files = files2 = new List<FileMetadata>();

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
                    var path = $"{(keys.TryGetValue((bifId, headerFile.FileId), out var key) ? key : $"{i}")}{(fileTypes.TryGetValue(headerFile.FileType, out var z) ? $".{z}" : string.Empty)}".Replace('\\', '/');
                    files2.Add(new FileMetadata
                    {
                        Path = path,
                        ObjectFactory = ObjectFactory(path),
                        FileSize = headerFile.FileSize,
                        Position = headerFile.FilePosition,
                    });
                }
            }
            else throw new FileFormatException($"Unknown File Type {magic}");
            return Task.CompletedTask;
        }

        public unsafe override Task<Stream> ReadDataAsync(BinaryPakFile source, BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception = null)
        {
            Stream fileData = null;
            r.Position(file.Position);
            if (source.Version == BIFF_VERSION)
                fileData = new MemoryStream(r.ReadBytes((int)file.FileSize));
            else throw new ArgumentOutOfRangeException(nameof(source.Version), $"{source.Version}");
            return Task.FromResult(fileData);
        }
    }
}