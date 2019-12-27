using GameEstate.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    // https://witcher.fandom.com/wiki/KEY_BIF_V1.1_format
    public class PakFormatRed : PakFormat
    {
        #region Headers

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        struct KEY_Header
        {
            public uint Version;            // Version ("V1.1")
            public uint NumFiles;           // Number of entries in FILETABLE
            public uint NumKeys;            // Number of entries in KEYTABLE.
            public uint NotUsed01;          //
            public uint FilesPosition;      // Offset to FILETABLE (0x440000).
            public uint KeysPosition;       // Offset to KEYTABLE.
            public uint BuildYear;          // Build year (less 1900).
            public uint BuildDay;           // Build day
            public ulong NotUsed02;         //
            public ulong NotUsed03;         //
            public ulong NotUsed04;         //
            public ulong NotUsed05;         //
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
            public uint ResourceId;         // Number of entries in FILETABLE
            public uint Flags;              // Flags (BIF index is now in this value, (flags & 0xFFF00000) >> 20). The rest appears to define 'fixed' index.
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        struct BIF_Header
        {
            public uint Version;            // Version ("V1.1")
            public uint NumFiles;           // Resource Count
            public uint NotUsed01;          //
            public uint FilesPosition;      // Offset to KEYTABLE.
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        struct BIFF_HeaderFile
        {
            public uint ResourceId;         // Resource ID
            public uint Flags;              // Flags (BIF index is now in this value, (flags & 0xFFF00000) >> 20). The rest appears to define 'fixed' index.
            public uint ResourcePosition;   // Offset to Resource Data.
            public uint ResourceSize;       // Size of Resource Data.
            public ushort ResourceType;     // Resource Type
            public ushort NotUsed01;        //
        }

        // 
        const uint KEY_SIGNATURE = 0x01; // Version number of a Fallout 4 BA2
        const uint BIFF_SIGNATURE = 0x01; // Version number of a Fallout 4 BA2

        const uint KEY_VERSION = 0x01; // Version number of a Fallout 4 BA2
        const uint BIFF_VERSION = 0x01; // Version number of a Fallout 4 BA2

        #endregion

        readonly static Dictionary<int, string> ResourceExtensions = new Dictionary<int, string>
        {
            {0x0000, ".res" },
            {0x0001, ".bmp"},
            {0x0002, ".mve"},
            {0x0003, ".tga"},
            {0x0004, ".wav"},

            {0x0006, ".plt"},
            {0x0007, ".ini"},
            {0x0008, ".mp3"},
            {0x0009, ".mpg"},
            {0x000A, ".txt"},
            {0x000B, ".xml"},

            {0x07D0, ".plh"},
            {0x07D1, ".tex"},
            {0x07D2, ".mdl"},
            {0x07D3, ".thg"},

            {0x07D5, ".fnt"},

            {0x07D7, ".lua"},
            {0x07D8, ".slt"},
            {0x07D9, ".nss"},
            {0x07DA, ".ncs"},
            {0x07DB, ".mod"},
            {0x07DC, ".are"},
            {0x07DD, ".set"},
            {0x07DE, ".ifo"},
            {0x07DF, ".bic"},
            {0x07E0, ".wok"},
            {0x07E1, ".2da"},
            {0x07E2, ".tlk"},

            {0x07E6, ".txi"},
            {0x07E7, ".git"},
            {0x07E8, ".bti"},
            {0x07E9, ".uti"},
            {0x07EA, ".btc"},
            {0x07EB, ".utc"},

            {0x07ED, ".dlg"},
            {0x07EE, ".itp"},
            {0x07EF, ".btt"},
            {0x07F0, ".utt"},
            {0x07F1, ".dds"},
            {0x07F2, ".bts"},
            {0x07F3, ".uts"},
            {0x07F4, ".ltr"},
            {0x07F5, ".gff"},
            {0x07F6, ".fac"},
            {0x07F7, ".bte"},
            {0x07F8, ".ute"},
            {0x07F9, ".btd"},
            {0x07FA, ".utd"},
            {0x07FB, ".btp"},
            {0x07FC, ".utp"},
            {0x07FD, ".dft"},
            {0x07FE, ".gic"},
            {0x07FF, ".gui"},
            {0x0800, ".css"},
            {0x0801, ".ccs"},
            {0x0802, ".btm"},
            {0x0803, ".utm"},
            {0x0804, ".dwk"},
            {0x0805, ".pwk"},
            {0x0806, ".btg"},

            {0x0808, ".jrl"},
            {0x0809, ".sav"},
            {0x080A, ".utw"},
            {0x080B, ".4pc"},
            {0x080C, ".ssf"},

            {0x080F, ".bik"},
            {0x0810, ".ndb"},
            {0x0811, ".ptm"},
            {0x0812, ".ptt"},
            {0x0813, ".ncm"},
            {0x0814, ".mfx"},
            {0x0815, ".mat"},
            {0x0816, ".mdb"},
            {0x0817, ".say"},
            {0x0818, ".ttf"},
            {0x0819, ".ttc"},
            {0x081A, ".cut"},
            {0x081B, ".ka" },
            {0x081C, ".jpg"},
            {0x081D, ".ico"},
            {0x081E, ".ogg"},
            {0x081F, ".spt"},
            {0x0820, ".spw"},
            {0x0821, ".wfx"},
            {0x0822, ".ugm"},
            {0x0823, ".qdb"},
            {0x0824, ".qst"},
            {0x0825, ".npc"},
            {0x0826, ".spn"},
            {0x0827, ".utx"},
            {0x0828, ".mmd"},
            {0x0829, ".smm"},
            {0x082A, ".uta"},
            {0x082B, ".mde"},
            {0x082C, ".mdv"},
            {0x082D, ".mda"},
            {0x082E, ".mba"},
            {0x082F, ".oct"},
            {0x0830, ".bfx"},
            {0x0831, ".pdb"},
            {0x0832, ".TheWitcherSave"},
            {0x0833, ".pvs"},
            {0x0834, ".cfx"},
            {0x0835, ".luc"},

            {0x0837, ".prb"},
            {0x0838, ".cam"},
            {0x0839, ".vds"},
            {0x083A, ".bin"},
            {0x083B, ".wob"},
            {0x083C, ".api"},
            {0x083D, ".properties"},
            {0x083E, ".png"},

            {0x270B, ".big"},

            {0x270D, ".erf"},
            {0x270E, ".bif"},
            {0x270F, ".key"},


        };

        public unsafe override Task ReadAsync(CorePakFile source, BinaryReader r)
        {
            FileMetadata[] files;
            var Signature = r.ReadUInt32();
            if (Signature == KEY_SIGNATURE)
            {
                var header = r.ReadT<KEY_Header>(sizeof(KEY_Header));
                if (header.Version != KEY_VERSION)
                    throw new InvalidOperationException("BAD MAGIC");
                source.Version = header.Version;
                source.Files = files = new FileMetadata[header.NumFiles];

                // keys
                var keys = new Dictionary<(uint, uint), string>();
                r.Position(header.KeysPosition);
                var infoKeys = r.ReadTArray<KEY_HeaderKey>(sizeof(KEY_HeaderKey), (int)header.NumKeys);
                for (var i = 0; i < header.NumKeys; i++)
                {
                    var infoKey = infoKeys[i];
                    keys.Add(((infoKey.Flags & 0xFFF00000) >> 20, infoKey.ResourceId), UnsafeUtils.ReadZASCII(infoKey.Name));
                }

                // files
                r.Position(header.FilesPosition);
                var infos = r.ReadTArray<KEY_HeaderFile>(sizeof(KEY_HeaderFile), (int)header.NumFiles);
                for (var i = 0; i < header.NumFiles; i++)
                {
                    var info = infos[i];
                    r.Position(info.FileNameOffset);
                    files[i] = new FileMetadata
                    {
                        FileSize = info.FileSize,
                        Path = r.ReadASCII((int)info.FileNameSize).Replace('\\', '/'),
                    };
                }
            }
            else if (Signature == 0x12) // Signature ("BIFF")
            {
                var bifId = 1U;
                var keys = new Dictionary<(uint, uint), string>();

                var header = r.ReadT<BIF_Header>(sizeof(BIF_Header));
                if (header.Version != BIFF_VERSION)
                    throw new InvalidOperationException("BAD MAGIC");
                source.Version = header.Version;
                source.Files = files = new FileMetadata[header.NumFiles];

                // files
                r.Position(header.FilesPosition);
                var infos = r.ReadTArray<BIFF_HeaderFile>(sizeof(BIFF_HeaderFile), (int)header.NumFiles);
                for (var i = 0; i < header.NumFiles; i++)
                {
                    var info = infos[i];
                    if (info.ResourceId > i)
                        continue; // Curiously the last resource entry of djinni.bif seem to be missing
                    var path = (keys.TryGetValue((bifId, info.ResourceId), out var key) ? key : $"{i}") + (ResourceExtensions.TryGetValue(info.ResourceType, out var extension) ? extension : string.Empty);
                    files[i] = new FileMetadata
                    {
                        FileSize = info.ResourceSize,
                        Path = path.Replace('\\', '/'),
                        Position = info.ResourcePosition,
                    };

                    //r.Position(info.ResourcePosition);
                    //var resource = r.ReadBytes((int)info.ResourceSize);
                }
            }
            else throw new InvalidOperationException("BAD MAGIC");
            return Task.CompletedTask;
        }

        //public override Task WriteAsync(CorePakFile source, BinaryWriter w)
        //{
        //    var files = source.Files = new List<FileMetadata>();
        //    r.BaseStream.Seek(0, SeekOrigin.Begin);
        //    var chunk = new byte[16];
        //    var buf = new byte[100];
        //    // read in 16 bytes chunks
        //    while (r.Read(chunk, 0, 0x10) != 0)
        //    {
        //        if (chunk[0] != Magic[0] || chunk[1] != Magic[1] || chunk[2] != Magic[2] || chunk[3] != Magic[3])
        //            continue;
        //        // file found
        //        var compressed = BitConverter.ToInt16(chunk, 8) == 0x64;
        //        r.Read(chunk, 0, 14); //: minus 2
        //        var fileNameSize = BitConverter.ToInt16(chunk, 0xA);
        //        var extraFieldSize = BitConverter.ToInt16(chunk, 0xC);

        //        // file name
        //        var fileNameRead = 2 + ((fileNameSize - 2 + 15) & ~15) + 16;
        //        if (fileNameRead > buf.Length) buf = new byte[fileNameRead];
        //        r.Read(buf, 0, fileNameRead);
        //        var fileName = Encoding.ASCII.GetString(buf, 0, fileNameSize);
        //        var charIdx = (fileNameSize - 2) % 16;

        //        // file size
        //        var packedSize = BitConverter.ToUInt32(buf, fileNameSize + 12);

        //        // skip extra
        //        var extraFieldRead = ((extraFieldSize + 15) & ~15) - (charIdx != 0 ? 32 : 16);
        //        r.Skip(extraFieldRead); //: var extraField = new byte[extraFieldRead]; r.Read(extraField, 0, extraField.Length);

        //        // add
        //        files.Add(new FileMetadata
        //        {
        //            Position = r.BaseStream.Position,
        //            Compressed = compressed,
        //            Path = fileName,
        //            PackedSize = packedSize,
        //        });

        //        // file data
        //        r.Skip(packedSize + (16 - (packedSize % 16))); //: var file = new byte[fileSize]; _r.Read(file, 0, fileSize); _r.Position += 16 - (fileSize % 16);
        //    }
        //    return Task.CompletedTask;
        //}
    }
}