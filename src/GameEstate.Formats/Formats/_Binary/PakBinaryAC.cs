using GameEstate.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    public class PakBinaryAC : PakBinary
    {
        public static readonly PakBinary Instance = new PakBinaryAC();
        PakBinaryAC() { }

        #region Headers

        const uint DAT_HEADER_OFFSET = 0x140;

        public enum DataType : uint
        {
            Portal = 1, // client_portal.dat and client_highres.dat both have this value
            Cell = 2, // client_cell_1.dat
            Language = 3 // client_local_English.dat
        }

        public enum FileType : uint
        {
            /// <summary>
            /// File Format:
            ///     DWORD LandblockId
            ///     DWORD LandblockInfoId
            ///     WORD x 81 = Terrain Map
            ///     WORD x 81 = Height Map
            /// </summary>
            [DataType(DataType.Cell)] LandBlock = 1, // DB_TYPE_LANDBLOCK
            /// <summary>
            /// File Format:
            ///     DWORD LandblockId
            ///     DWORD Number of Cells
            ///     DWORD Number of static objects (numObj)
            ///         numObj of:
            ///             DWORD Static Object id
            ///             POSITION Object Position
            ///     WORD Building Count (numBldg)
            ///     WORD Building Flags
            ///         numBldg of:
            ///             DWORD MeshId
            ///             POSITION Building Position
            ///     DWORD unknown
            ///     DWORD numPortals
            ///         numPortals of:
            ///             WORD Flags
            ///             WORD CellID
            ///             WORD Unknown
            ///             WORD numCells
            ///                 numCells of:
            ///                     WORD CellID
            ///             
            /// POSITION ::
            ///     FLOAT Vector.X
            ///     FLOAT Vector.Y
            ///     FLOAT Vector.Z
            ///     FLOAT Quat.W
            ///     FLOAT Quat.X
            ///     FLOAT Quat.Y
            ///     FLOAT Quat.Z
            /// </summary>
            [DataType(DataType.Cell), FileExt("lbi")] LandBlockInfo = 2, // DB_TYPE_LBI
            [DataType(DataType.Cell), FileIdRange(0x01010000, 0x013EFFFF)] EnvCell = 3, // DB_TYPE_ENVCELL
            /// <summary>
            /// usage of this is currently unknown.  exists in the client, but has no discernable
            /// source dat file.  appears to be a server file not distributed to clients.
            /// </summary>
            [FileExt("lbo")] LandBlockObjects = 4, // DB_TYPE_LBO
            /// <summary>
            /// usage of this is currently unknown.  exists in the client, but has no discernable
            /// source dat file.  appears to be a server file not distributed to clients.
            /// </summary>
            [FileExt("ins")] Instantiation = 5, // DB_TYPE_INSTANTIATION
            [DataType(DataType.Portal), FileExt("obj"), FileIdRange(0x01000000, 0x0100FFFF)] GraphicsObject = 6, // DB_TYPE_GFXOBJ
            [DataType(DataType.Portal), FileExt("set"), FileIdRange(0x02000000, 0x0200FFFF)] Setup = 7, // DB_TYPE_SETUP
            [DataType(DataType.Portal), FileExt("anm"), FileIdRange(0x03000000, 0x0300FFFF)] Animation = 8, // DB_TYPE_ANIM
            /// <summary>
            /// usage of this is currently unknown.  exists in the client, but has no discernable
            /// source dat file.  appears to be a server file not distributed to clients.
            /// </summary>
            [FileExt("hk")] AnimationHook = 9, // DB_TYPE_ANIMATION_HOOK
            [DataType(DataType.Portal), FileExt("pal"), FileIdRange(0x04000000, 0x0400FFFF)] Palette = 10, // DB_TYPE_PALETTE
            [DataType(DataType.Portal), FileExt("texture"), FileIdRange(0x05000000, 0x05FFFFFF)] SurfaceTexture = 11, // DB_TYPE_SURFACETEXTURE
            /// <summary>
            /// the 5th dword of these files has values from the following enum:
            /// https://msdn.microsoft.com/en-us/library/windows/desktop/bb172558(v=vs.85).aspx
            /// plus the additional values:
            ///     0x000000F4 - Same as 0x00000001C / D3DFMT_A8
            ///     0x000001F4 - JPEG
            ///     
            /// all the files contain a 6-DWORD header (offset indices):
            ///     0: objectId
            ///     4: unknown
            ///     8: width
            ///     12: height
            ///     16: format (see above)
            ///     20: length
            /// </summary>
            [DataType(DataType.Portal), FileExt("jpg"), FileExt("dds"), FileExt("tga"), FileExt("iff"), FileExt("256"), FileExt("csi"), FileExt("alp"), FileIdRange(0x06000000, 0x07FFFFFF)] Texture = 12, // DB_TYPE_RENDERSURFACE
            /// <summary>
            /// indexed in client as "materials" for some reason
            /// </summary>
            [DataType(DataType.Portal), FileExt("surface"), FileIdRange(0x08000000, 0x0800FFFF)] Surface = 13, // DB_TYPE_SURFACE
            [DataType(DataType.Portal), FileExt("dsc"), FileIdRange(0x09000000, 0x0900FFFF)] MotionTable = 14, // DB_TYPE_MTABLE
            /// <summary>
            /// indexed as "sound" in the client
            /// </summary>
            [DataType(DataType.Portal), FileExt("wav"), FileIdRange(0x0A000000, 0x0A00FFFF)] Wave = 15, // DB_TYPE_WAVE
            /// <summary>
            /// File content structure:
            /// 0: DWORD LandblockId
            /// 4: DWORD CellCount
            /// 8: CellCount blocks of Cells
            /// 
            /// Cell content:
            /// 0: DWORD Cell Index
            /// 4: DWORD Polygon Count (List1)
            /// 8: DWORD Polygon Count (List2)
            /// 12: DWORD Polygon Pointer Count (???)
            /// 16: Vertex Array
            /// ??: List1 Polygons
            /// 
            /// Vertex Array content:
            /// 0: DWORD Vertex Type
            /// 4: DWORD Vertex Count
            /// 8: VertexCount block of Vertecis (whole thing aligned to 32-byte boundary
            /// 
            /// Vertex Content:
            /// 0: WORD Vertex Index
            /// 2: WORD Count
            /// 4: FLOAT Origin X
            /// 8: FLOAT Origin Y
            /// 12: FLOAT Origin Z
            /// 16: FLOAT Normal X
            /// 20: FLOAT Normal Y
            /// 24: FLOAT Normal Z
            /// 28: VertexUV Array of Vertex.Count length
            ///     0: FLOAT Vertex U
            ///     4: FLOAT Vertex V
            /// 
            /// Polygon Content:
            /// 0: WORD Polygon Index
            /// 2: BYTE Vertex Count
            /// 3: BYTE Poly Type
            /// 4: DWORD Cull Mode - https://msdn.microsoft.com/en-us/library/microsoft.xna.framework.graphics.cullmode(v=xnagamestudio.31).aspx
            ///     NOTE: AC doesn't seem to fully respect these Cull mode values.  1 appears to be None instead of CCW, and 2 is None.
            /// 8: WORD Front Texture Index
            /// 10: WORD Back Texture Index
            /// 12: VertexCount of:
            ///     0: WORD Vertex Index in previous Vertex Content
            /// if ((PolyType & 4) == 0)
            ///     VertexCount of:
            ///     BYTE Front-Face Vertex Index
            /// if ((PolyType & 8) == 0 && CullMode == 2)
            ///     VertexCount of:
            ///     BYTE Back-Face Vertex Index
            /// 
            /// Note: If CullMode is 1, copy Front-Face data to Back-face
            /// </summary>
            [DataType(DataType.Portal), FileExt("env"), FileIdRange(0x0D000000, 0x0D00FFFF)] Environment = 16, // DB_TYPE_ENVIRONMENT
            /// <summary>
            /// indexed as "ui" in the client
            /// </summary>
            [DataType(DataType.Portal), FileExt("cps"), FileIdRange(0x0E000007, 0x0E000007)] ChatPoseTable = 17, // DB_TYPE_CHAT_POSE_TABLE
            /// <summary>
            /// indexed as "DungeonCfgs" in the client
            /// </summary>
            [DataType(DataType.Portal), FileExt("hrc"), FileIdRange(0x0E00000D, 0x0E00000D)] ObjectHierarchy = 18, // DB_TYPE_OBJECT_HIERARCHY
            /// <summary>
            /// indexed as "weenie" in the client
            /// </summary>
            [DataType(DataType.Portal), FileExt("bad"), FileIdRange(0x0E00001A, 0x0E00001A)] BadData = 19, // DB_TYPE_BADDATA
            /// <summary>
            /// indexed as "weenie" in the client
            /// </summary>
            [DataType(DataType.Portal), FileExt("taboo"), FileIdRange(0x0E00001E, 0x0E00001E)] TabooTable = 20, // DB_TYPE_TABOO_TABLE
            [DataType(DataType.Portal), FileIdRange(0x0E00001F, 0x0E00001F)] FileToId = 21, // DB_TYPE_FILE2ID_TABLE
            /// <summary>
            /// indexed as "namefilter" in the client
            /// </summary>
            [DataType(DataType.Portal), FileExt("nft"), FileIdRange(0x0E000020, 0x0E000020)] NameFilterTable = 22, // DB_TYPE_NAME_FILTER_TABLE
            /// <summary>
            /// indexed as "properties" in the client
            /// </summary>
            [DataType(DataType.Portal), FileExt("monprop"), FileIdRange(0x0E020000, 0x0E02FFFF)] MonitoredProperties = 23, // DB_TYPE_MONITOREDPROPERTIES
            [DataType(DataType.Portal), FileExt("pst"), FileIdRange(0x0F000000, 0x0F00FFFF)] PaletteSet = 24, // DB_TYPE_PAL_SET
            [DataType(DataType.Portal), FileExt("clo"), FileIdRange(0x10000000, 0x1000FFFF)] Clothing = 25, // DB_TYPE_CLOTHING
            [DataType(DataType.Portal), FileExt("deg"), FileIdRange(0x11000000, 0x1100FFFF)] DegradeInfo = 26, // DB_TYPE_DEGRADEINFO
            [DataType(DataType.Portal), FileExt("scn"), FileIdRange(0x12000000, 0x1200FFFF)] Scene = 27, // DB_TYPE_SCENE 
            /// <summary>
            /// indexed as "landscape" by the client
            /// </summary>
            [DataType(DataType.Portal), FileExt("rgn"), FileIdRange(0x13000000, 0x1300FFFF)] Region = 28, // DB_TYPE_REGION
            [DataType(DataType.Portal), FileExt("keymap"), FileIdRange(0x14000000, 0x1400FFFF)] KeyMap = 29, // DB_TYPE_KEYMAP
            /// <summary>
            /// indexed as "textures" in the client
            /// </summary>
            [DataType(DataType.Portal), FileExt("rtexture"), FileIdRange(0x15000000, 0x15FFFFFF)] RenderTexture = 30, // DB_TYPE_RENDERTEXTURE 
            /// <summary>
            /// indexed as "materials" in the client
            /// </summary>
            [DataType(DataType.Portal), FileExt("mat"), FileIdRange(0x16000000, 0x16FFFFFF)] RenderMaterial = 31, // DB_TYPE_RENDERMATERIAL 
            /// <summary>
            /// indexed as "materials" in the client
            /// </summary>
            [DataType(DataType.Portal), FileExt("mm"), FileIdRange(0x17000000, 0x17FFFFFF)] MaterialModifier = 32, // DB_TYPE_MATERIALMODIFIER 
            /// <summary>
            /// indexed as "materials" in the client
            /// </summary>
            [DataType(DataType.Portal), FileExt("mi"), FileIdRange(0x18000000, 0x18FFFFFF)] MaterialInstance = 33, // DB_TYPE_MATERIALINSTANCE
            /// <summary>
            /// SoundTable
            /// </summary>
            [DataType(DataType.Portal), FileExt("stb"), FileIdRange(0x20000000, 0x2000FFFF)] SoundTable = 34, // DB_TYPE_STABLE
            /// <summary>
            /// This is in the Language dat (client_local_English.dat)
            /// </summary>
            [DataType(DataType.Portal), FileExt("uil"), FileIdRange(0x21000000, 0x21FFFFFF)] UiLayout = 35, // DB_TYPE_UI_LAYOUT
            /// <summary>
            /// indexed as "emp" in the client
            /// </summary>
            [DataType(DataType.Portal), FileExt("emp"), FileIdRange(0x22000000, 0x22FFFFFF)] EnumMapper = 36, // DB_TYPE_ENUM_MAPPER
            /// <summary>
            /// This is in the Language dat (client_local_English.dat)
            /// </summary>
            [DataType(DataType.Portal), FileExt("stt"), FileIdRange(0x23000000, 0x24FFFFFF)] StringTable = 37, // DB_TYPE_STRING_TABLE 
            /// <summary>
            /// indexed as "emp/idmap" in the client
            /// </summary>
            [DataType(DataType.Portal), FileExt("imp"), FileIdRange(0x25000000, 0x25FFFFFF)] DidMapper = 38, // DB_TYPE_DID_MAPPER 
            [DataType(DataType.Portal), FileExt("actionmap"), FileIdRange(0x26000000, 0x2600FFFF)] ActionMap = 39, // DB_TYPE_ACTIONMAP 
            /// <summary>
            /// indexed as "emp/idmap" in the client
            /// </summary>
            [DataType(DataType.Portal), FileExt("dimp"), FileIdRange(0x27000000, 0x27FFFFFF)] DualDidMapper = 40, // DB_TYPE_DUAL_DID_MAPPER
            [DataType(DataType.Portal), FileExt("str"), FileIdRange(0x31000000, 0x3100FFFF)] String = 41, // DB_TYPE_STRING
            /// <summary>
            /// inedexed as "emt" in the client
            /// </summary>
            [DataType(DataType.Portal), FileExt("emt"), FileIdRange(0x32000000, 0x3200FFFF)] ParticleEmitter = 42, // DB_TYPE_PARTICLE_EMITTER 
            /// <summary>
            /// inedexed as "pes" in the client
            /// </summary>
            [DataType(DataType.Portal), FileExt("pes"), FileIdRange(0x33000000, 0x3300FFFF)] PhysicsScript = 43, // DB_TYPE_PHYSICS_SCRIPT 
            /// <summary>
            /// inedexed as "pet" in the client
            /// </summary>
            [DataType(DataType.Portal), FileExt("pet"), FileIdRange(0x34000000, 0x3400FFFF)] PhysicsScriptTable = 44, // DB_TYPE_PHYSICS_SCRIPT_TABLE 
            /// <summary>
            /// inedexed as "emt/property" in the client
            /// </summary>
            [DataType(DataType.Portal), FileExt("mpr"), FileIdRange(0x39000000, 0x39FFFFFF)] MasterProperty = 45, // DB_TYPE_MASTER_PROPERTY 
            [DataType(DataType.Portal), FileExt("font"), FileIdRange(0x40000000, 0x40000FFF)] Font = 46, // DB_TYPE_FONT 
            [DataType(DataType.Portal), FileExt("font_local"), FileIdRange(0x40001000, 0x400FFFFF)] FontLocal = 47, // DB_TYPE_FONT_LOCAL 
            /// <summary>
            /// This is located in the Language dat (client_local_English.dat)
            /// "stringtable" in the client
            /// </summary>
            [DataType(DataType.Portal), FileExt("sti"), FileIdRange(0x41000000, 0x41FFFFFF)] StringState = 48, // DB_TYPE_STRING_STATE
            [DataType(DataType.Portal), FileExt("dbpc"), FileExt("pmat"), FileIdRange(0x78000000, 0x7FFFFFFF)] DbProperties = 49, // DB_TYPE_DBPROPERTIES
            /// <summary>
            /// indexed as "mesh" in the client
            /// </summary>
            [DataType(DataType.Portal), FileExt("rendermesh"), FileIdRange(0x19000000, 0x19FFFFFF)] RenderMesh = 50, // DB_TYPE_RENDER_MESH
            // the following special files are called out in a different section of the decompiled client:
            [DataType(DataType.Portal), FileIdRange(0x0E000001, 0x0E000001)] WeenieDefaults = 97, // DB_TYPE_WEENIE_DEF
            [DataType(DataType.Portal), FileIdRange(0x0E000002, 0x0E000002)] CharacterGenerator = 98, // DB_TYPE_CHAR_GEN_0
            [DataType(DataType.Portal), FileIdRange(0x0E000003, 0x0E000003)] SecondaryAttributeTable = 99, // DB_TYPE_ATTRIBUTE_2ND_TABLE_0
            [DataType(DataType.Portal), FileIdRange(0x0E000004, 0x0E000004)] SkillTable = 100, // DB_TYPE_SKILL_TABLE_0
            [DataType(DataType.Portal), FileIdRange(0x0E00000E, 0x0E00000E)] SpellTable = 101, // DB_TYPE_SPELL_TABLE_0
            [DataType(DataType.Portal), FileIdRange(0x0E00000F, 0x0E00000F)] SpellComponentTable = 102, // DB_TYPE_SPELLCOMPONENT_TABLE_0
            [DataType(DataType.Portal), FileIdRange(0x0E000001, 0x0E000001)] TreasureTable = 103, // DB_TYPE_W_TREASURE_SYSTEM
            [DataType(DataType.Portal), FileIdRange(0x0E000019, 0x0E000019)] CraftTable = 104, // DB_TYPE_W_CRAFT_TABLE
            [DataType(DataType.Portal), FileIdRange(0x0E000018, 0x0E000018)] XpTable = 105, // DB_TYPE_XP_TABLE_0
            [DataType(DataType.Portal), FileIdRange(0x0E00001B, 0x0E00001B)] Quests = 106, // DB_TYPE_QUEST_DEF_DB_0
            [DataType(DataType.Portal), FileIdRange(0x0E00001C, 0x0E00001C)] GameEventTable = 107, // DB_TYPE_GAME_EVENT_DB
            [DataType(DataType.Portal), FileIdRange(0x0E010000, 0x0E01FFFF)] QualityFilter = 108, // DB_TYPE_QUALITY_FILTER_0
            [DataType(DataType.Portal), FileIdRange(0x30000000, 0x3000FFFF)] CombatTable = 109, // DB_TYPE_COMBAT_TABLE_0
            [DataType(DataType.Portal), FileIdRange(0x38000000, 0x3800FFFF)] ItemMutation = 110, // DB_TYPE_MUTATE_FILTER
            [DataType(DataType.Portal), FileIdRange(0x0E00001D, 0x0E00001D)] ContractTable = 111, // DB_TYPE_CONTRACT_TABLE_0
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        unsafe struct Header
        {
            public uint FileType;
            public uint BlockSize;
            public uint FileSize;
            [MarshalAs(UnmanagedType.U4)] public DataType DataSet;
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
            internal const int SizeOf = (sizeof(uint) * 0x3E) + sizeof(uint) + (File.SizeOf * 0x3D);
            public fixed uint Branches[0x3E];
            public uint EntryCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x3D)]
            public File[] Entries;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        unsafe struct File
        {
            internal const int SizeOf = sizeof(uint) * 6;
            public uint BitFlags; // not-used
            public uint ObjectId;
            public uint FileOffset;
            public uint FileSize;
            public uint Date; // not-used
            public uint Iteration; // not-used

            public FileType? GetFileType(DataType dataType)
            {
                if (dataType == DataType.Cell)
                {
                    if ((ObjectId & 0xFFFF) == 0xFFFF) return FileType.LandBlock;
                    else if ((ObjectId & 0xFFFF) == 0xFFFE) return FileType.LandBlockInfo;
                    else return FileType.EnvCell;
                }
                switch (ObjectId >> 24)
                {
                    case 0x01: return FileType.GraphicsObject;
                    case 0x02: return FileType.Setup;
                    case 0x03: return FileType.Animation;
                    case 0x04: return FileType.Palette;
                    case 0x05: return FileType.SurfaceTexture;
                    case 0x06: return FileType.Texture;
                    case 0x08: return FileType.Surface;
                    case 0x09: return FileType.MotionTable;
                    case 0x0A: return FileType.Wave;
                    case 0x0D: return FileType.Environment;
                    case 0x0F: return FileType.PaletteSet;
                    case 0x10: return FileType.Clothing;
                    case 0x11: return FileType.DegradeInfo;
                    case 0x12: return FileType.Scene;
                    case 0x13: return FileType.Region;
                    case 0x20: return FileType.SoundTable;
                    case 0x22: return FileType.EnumMapper;
                    case 0x23: return FileType.StringTable;
                    case 0x25: return FileType.DidMapper;
                    case 0x27: return FileType.DualDidMapper;
                    case 0x30: return FileType.CombatTable;
                    case 0x32: return FileType.ParticleEmitter;
                    case 0x33: return FileType.PhysicsScript;
                    case 0x34: return FileType.PhysicsScriptTable;
                    case 0x40: return FileType.Font;
                }
                if (ObjectId == 0x0E000002) return FileType.CharacterGenerator;
                else if (ObjectId == 0x0E000007) return FileType.ChatPoseTable;
                else if (ObjectId == 0x0E00000D) return FileType.ObjectHierarchy;
                else if (ObjectId == 0xE00001A) return FileType.BadData;
                else if (ObjectId == 0x0E00001E) return FileType.TabooTable;
                else if (ObjectId == 0x0E00001F) return FileType.FileToId;
                else if (ObjectId == 0x0E000020) return FileType.NameFilterTable;
                else if (ObjectId == 0x0E020000) return FileType.MonitoredProperties;
                Console.WriteLine($"Unknown file type: {ObjectId:X8}");
                return null;
            }
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

            public void AddFiles(DataType dataType, IList<FileMetadata> files, string path)
            {
                var did = 0;
                Directories.ForEach(d => d.AddFiles(dataType, files, Path.Combine(path, did++.ToString())));
                for (var i = 0; i < Header.EntryCount; i++)
                {
                    var entry = Header.Entries[i];
                    //files[entry.ObjectId] = entry;
                    var fileType = entry.GetFileType(dataType);
                    files.Add(new FileMetadata
                    {
                        Path = Path.Combine(path, $"{fileType}{entry.ObjectId:X8}"),
                        Position = entry.FileOffset,
                        FileSize = entry.FileSize,
                        Tag = entry,
                    });
                }
            }
        }

        #endregion

        #region Attributes

        [AttributeUsage(AttributeTargets.Field)]
        public class DataTypeAttribute : Attribute
        {
            public DataTypeAttribute(DataType type) => Type = type;
            public DataType Type { get; set; }
        }

        [AttributeUsage(AttributeTargets.Class)]
        public class FileTypeAttribute : Attribute
        {
            public FileTypeAttribute(FileType fileType) => FileType = fileType;
            public FileType FileType { get; set; }
        }

        [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
        public class FileExtAttribute : Attribute
        {
            public FileExtAttribute(string fileExtension) => FileExtension = fileExtension;
            public string FileExtension { get; set; }
        }

        [AttributeUsage(AttributeTargets.Field)]
        public class FileIdRangeAttribute : Attribute
        {
            public FileIdRangeAttribute(uint beginRange, uint endRange) { BeginRange = beginRange; EndRange = endRange; }
            public uint BeginRange { get; set; }
            public uint EndRange { get; set; }
        }

        #endregion

        static T ReadT<T>(BinaryReader r, long offset, int size, int blockSize) => UnsafeUtils.MarshalT<T>(ReadBytes(r, offset, size, blockSize));

        static byte[] ReadBytes(BinaryReader r, long offset, int size, int blockSize)
        {
            r.Position(offset);
            var nextAddress = (long)r.ReadUInt32();
            var buffer = new byte[size];
            var bufferIdx = 0;
            int read;
            while (size > 0)
                if (size > blockSize)
                {
                    read = r.Read(buffer, bufferIdx, blockSize - 4);
                    bufferIdx += read;
                    size -= read;
                    r.Position(nextAddress); nextAddress = r.ReadUInt32();
                }
                else
                {
                    r.Read(buffer, bufferIdx, size);
                    return buffer;
                }
            return buffer;
        }

        public unsafe override Task ReadAsync(BinaryPakFile source, BinaryReader r, ReadStage stage)
        {
            var files = source.Files = new List<FileMetadata>();
            r.Position(DAT_HEADER_OFFSET);
            var header = r.ReadT<Header>(sizeof(Header));
            var directory = new Directory(r, header.BTree, (int)header.BlockSize);
            directory.AddFiles(header.DataSet, files, string.Empty);
            return Task.CompletedTask;
        }

        public override Task<byte[]> ReadFileAsync(BinaryPakFile source, BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception = null)
        {
            r.Position(file.Position);
            return Task.FromResult(r.ReadBytes((int)file.FileSize));
        }
    }
}