using GameEstate.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace GameEstate.Formats.Binary.UltimaUO.Records
{
    public class TileMatrix
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct StaticTile
        {
            public StaticTile(ushort id, sbyte z)
            {
                ID = id;
                Z = z;
                X = 0;
                Y = 0;
                Hue = 0;
            }
            public StaticTile(ushort id, byte x, byte y, sbyte z, short hue)
            {
                ID = id;
                X = x;
                Y = y;
                Z = z;
                Hue = hue;
            }

            public ushort ID;
            public byte X;
            public byte Y;
            public sbyte Z;
            public short Hue;

            public int Height(TileDataRecord tileData) => tileData.Items[ID & tileData.MaxItem].Height;

            public void Set(ushort id, sbyte z)
            {
                ID = id;
                Z = z;
            }
            public void Set(ushort id, byte x, byte y, sbyte z, short hue)
            {
                ID = id;
                X = x;
                Y = y;
                Z = z;
                Hue = hue;
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct LandTile
        {
            public LandTile(short id, sbyte z)
            {
                ID = id;
                Z = z;
            }

            public short ID;
            public sbyte Z;
            public int Height => 0;
            public bool Ignored => ID == 2 || ID == 0x1DB || (ID >= 0x1AE && ID <= 0x1B5);

            public void Set(short id, sbyte z)
            {
                ID = id;
                Z = z;
            }
        }

        public struct MultiTileEntry
        {
            public ushort ItemID;
            public short OffsetX, OffsetY, OffsetZ;
            public int Flags;

            public MultiTileEntry(ushort itemID, short xOffset, short yOffset, short zOffset, int flags)
            {
                ItemID = itemID;
                OffsetX = xOffset;
                OffsetY = yOffset;
                OffsetZ = zOffset;
                Flags = flags;
            }
        }

        StaticTile[][][][][] _staticTiles;
        LandTile[][][] _landTiles;
        int _fileIndex;
        int _width, _height;
        object _owner;
        int[][] _staticPatches;
        int[][] _landPatches;
        static List<TileMatrix> _instances = new List<TileMatrix>();
        List<TileMatrix> _fileShare = new List<TileMatrix>();

        public TileMatrix(BinaryDatFile source, object owner, int fileIndex, int mapID, int width, int height)
        {
            lock (_instances)
            {
                for (var i = 0; i < _instances.Count; ++i)
                {
                    var tm = _instances[i];
                    if (tm._fileIndex == fileIndex)
                        lock (_fileShare)
                            lock (tm._fileShare)
                            {
                                tm._fileShare.Add(this);
                                _fileShare.Add(tm);
                            }
                }
                _instances.Add(this);
            }

            _fileIndex = fileIndex;
            _width = width;
            _height = height;
            BlockWidth = width >> 3;
            BlockHeight = height >> 3;
            _owner = owner;

            if (fileIndex != 0x7F)
            {
                Map = source.GetReader($"map{fileIndex}.mul");
                if (Map == null)
                {
                    Map = source.GetReader($"map{fileIndex}LegacyMUL.uop");
                    MapIndex = Map != null ? new UOPIndex(Map) : null;
                }
                Index = source.GetReader($"staidx{fileIndex}.mul");
                Statics = source.GetReader($"statics{fileIndex}.mul");
            }

            EmptyStaticBlock = new StaticTile[8][][];
            for (var i = 0; i < 8; ++i)
            {
                EmptyStaticBlock[i] = new StaticTile[8][];
                for (var j = 0; j < 8; ++j)
                    EmptyStaticBlock[i][j] = new StaticTile[0];
            }

            _landTiles = new LandTile[BlockWidth][][];
            _staticTiles = new StaticTile[BlockWidth][][][][];
            _staticPatches = new int[BlockWidth][];
            _landPatches = new int[BlockWidth][];
            Patch = new TileMatrixPatch(source, this, mapID);
        }

        public void Dispose()
        {
            Map?.Close();
            Statics?.Close();
            Index?.Close();
        }

        public readonly TileMatrixPatch Patch;

        public readonly int BlockWidth;

        public readonly int BlockHeight;

        readonly BinaryReader Map;

        readonly UOPIndex MapIndex;

        readonly BinaryReader Index;

        readonly BinaryReader Statics; //: Data

        public bool Exists => Map != null && Index != null && Statics != null;

        public readonly LandTile[] InvalidLandBlock = new LandTile[196];

        public readonly StaticTile[][][] EmptyStaticBlock;

        //[MethodImpl(MethodImplOptions.Synchronized)]
        public void SetStaticBlock(int x, int y, StaticTile[][][] value)
        {
            if (x < 0 || y < 0 || x >= BlockWidth || y >= BlockHeight)
                return;
            if (_staticTiles[x] == null)
                _staticTiles[x] = new StaticTile[BlockHeight][][][];
            _staticTiles[x][y] = value;
            if (_staticPatches[x] == null)
                _staticPatches[x] = new int[(BlockHeight + 31) >> 5];
            _staticPatches[x][y >> 5] |= 1 << (y & 0x1F);
        }

        //[MethodImpl(MethodImplOptions.Synchronized)]
        public StaticTile[][][] GetStaticBlock(int x, int y)
        {
            if (x < 0 || y < 0 || x >= BlockWidth || y >= BlockHeight || Statics == null || Index == null)
                return EmptyStaticBlock;
            if (_staticTiles[x] == null)
                _staticTiles[x] = new StaticTile[BlockHeight][][][];
            var tiles = _staticTiles[x][y];
            if (tiles == null)
            {
                lock (_fileShare)
                    for (var i = 0; tiles == null && i < _fileShare.Count; ++i)
                    {
                        var shared = _fileShare[i];
                        lock (shared)
                            if (x >= 0 && x < shared.BlockWidth && y >= 0 && y < shared.BlockHeight)
                            {
                                var theirTiles = shared._staticTiles[x];
                                if (theirTiles != null)
                                    tiles = theirTiles[y];
                                if (tiles != null)
                                {
                                    var theirBits = shared._staticPatches[x];
                                    if (theirBits != null && (theirBits[y >> 5] & (1 << (y & 0x1F))) != 0)
                                        tiles = null;
                                }
                            }
                    }
                if (tiles == null)
                    tiles = ReadStaticBlock(x, y);
                _staticTiles[x][y] = tiles;
            }
            return tiles;
        }

        public StaticTile[] GetStaticTiles(int x, int y)
        {
            var tiles = GetStaticBlock(x >> 3, y >> 3);
            return tiles[x & 0x7][y & 0x7];
        }

        List<StaticTile> _tilesList = new List<StaticTile>();

        //[MethodImpl(MethodImplOptions.Synchronized)]
        public StaticTile[] GetStaticTiles(int x, int y, bool multis)
        {
            var tiles = GetStaticBlock(x >> 3, y >> 3);
            //if (multis)
            //{
            //    var eable = _owner.GetMultiTilesAt(x, y);
            //    if (eable == Map.NullEnumerable<StaticTile[]>.Instance)
            //        return tiles[x & 0x7][y & 0x7];
            //    var any = false;
            //    foreach (var multiTiles in eable)
            //    {
            //        if (!any)
            //            any = true;
            //        _tilesList.AddRange(multiTiles);
            //    }
            //    eable.Free();
            //    if (!any)
            //        return tiles[x & 0x7][y & 0x7];
            //    _tilesList.AddRange(tiles[x & 0x7][y & 0x7]);
            //    return _tilesList.ToArray();
            //}
            return tiles[x & 0x7][y & 0x7];
        }

        //[MethodImpl(MethodImplOptions.Synchronized)]
        public void SetLandBlock(int x, int y, LandTile[] value)
        {
            if (x < 0 || y < 0 || x >= BlockWidth || y >= BlockHeight)
                return;
            if (_landTiles[x] == null)
                _landTiles[x] = new LandTile[BlockHeight][];
            _landTiles[x][y] = value;
            if (_landPatches[x] == null)
                _landPatches[x] = new int[(BlockHeight + 31) >> 5];
            _landPatches[x][y >> 5] |= 1 << (y & 0x1F);
        }

        //[MethodImpl(MethodImplOptions.Synchronized)]
        public LandTile[] GetLandBlock(int x, int y)
        {
            if (x < 0 || y < 0 || x >= BlockWidth || y >= BlockHeight || Map == null)
                return InvalidLandBlock;
            if (_landTiles[x] == null)
                _landTiles[x] = new LandTile[BlockHeight][];
            var tiles = _landTiles[x][y];
            if (tiles == null)
            {
                lock (_fileShare)
                    for (var i = 0; tiles == null && i < _fileShare.Count; ++i)
                    {
                        var shared = _fileShare[i];
                        lock (shared)
                            if (x >= 0 && x < shared.BlockWidth && y >= 0 && y < shared.BlockHeight)
                            {
                                var theirTiles = shared._landTiles[x];
                                if (theirTiles != null)
                                    tiles = theirTiles[y];
                                if (tiles != null)
                                {
                                    var theirBits = shared._landPatches[x];
                                    if (theirBits != null && (theirBits[y >> 5] & (1 << (y & 0x1F))) != 0)
                                        tiles = null;
                                }
                            }
                    }
                if (tiles == null)
                    tiles = ReadLandBlock(x, y);
                _landTiles[x][y] = tiles;
            }
            return tiles;
        }

        public LandTile GetLandTile(int x, int y)
        {
            var tiles = GetLandBlock(x >> 3, y >> 3);
            return tiles[((y & 0x7) << 3) + (x & 0x7)];
        }

        List<StaticTile>[][] _lists;
        StaticTile[] _tileBuffer = new StaticTile[128];

        DateTime _nextStaticWarning;
        //[MethodImpl(MethodImplOptions.Synchronized)]
        unsafe StaticTile[][][] ReadStaticBlock(int x, int y)
        {
            try
            {
                Index.Position(((x * BlockHeight) + y) * 12);
                var lookup = Index.ReadInt32();
                var length = Index.ReadInt32();
                if (lookup < 0 || length <= 0)
                    return EmptyStaticBlock;

                var count = length / 7;
                Statics.Position(lookup);

                if (_tileBuffer.Length < count)
                    _tileBuffer = new StaticTile[count];

                var staTiles = _tileBuffer;
                fixed (StaticTile* pTiles = staTiles)
                {
                    Statics.ReadTMany(staTiles, length);

                    if (_lists == null)
                    {
                        _lists = new List<StaticTile>[8][];
                        for (var i = 0; i < 8; ++i)
                        {
                            _lists[i] = new List<StaticTile>[8];
                            for (var j = 0; j < 8; ++j)
                                _lists[i][j] = new List<StaticTile>();
                        }
                    }

                    var lists = _lists;
                    StaticTile* pCur = pTiles, pEnd = pTiles + count;
                    while (pCur < pEnd)
                    {
                        lists[pCur->X & 0x7][pCur->Y & 0x7].Add(new StaticTile(pCur->ID, pCur->Z));
                        pCur++;
                    }

                    var tiles = new StaticTile[8][][];
                    for (var i = 0; i < 8; ++i)
                    {
                        tiles[i] = new StaticTile[8][];
                        for (var j = 0; j < 8; ++j)
                            tiles[i][j] = lists[i][j].ToArray();
                    }
                    return tiles;
                }
            }
            catch (EndOfStreamException)
            {
                if (DateTime.UtcNow >= _nextStaticWarning)
                {
                    Console.WriteLine($"Warning: Static EOS for {_owner} ({x}, {y})");
                    _nextStaticWarning = DateTime.UtcNow.AddMinutes(1.0);
                }
                return EmptyStaticBlock;
            }
        }

        //public void Force()
        //{
        //    if (ScriptCompiler.Assemblies == null || ScriptCompiler.Assemblies.Length == 0)
        //        throw new Exception();
        //}

        DateTime _nextLandWarning;
        //[MethodImpl(MethodImplOptions.Synchronized)]
        LandTile[] ReadLandBlock(int x, int y)
        {
            try
            {
                var offset = ((x * BlockHeight) + y) * 196 + 4;
                if (MapIndex != null)
                    offset = MapIndex.Lookup(offset);
                Map.Position(offset);
                var tiles = new LandTile[64];
                Map.ReadTMany(tiles, 192);
                return tiles;
            }
            catch
            {
                if (DateTime.UtcNow >= _nextLandWarning)
                {
                    Console.WriteLine($"Warning: Land EOS for {_owner} ({x}, {y})");
                    _nextLandWarning = DateTime.UtcNow.AddMinutes(1.0);
                }
                return InvalidLandBlock;
            }
        }
    }
}