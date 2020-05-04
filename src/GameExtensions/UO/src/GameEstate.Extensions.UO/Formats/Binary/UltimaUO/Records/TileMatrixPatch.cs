using GameEstate.Core;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using static GameEstate.Formats.Binary.UltimaUO.Records.TileMatrix;

namespace GameEstate.Formats.Binary.UltimaUO.Records
{
    public class TileMatrixPatch
    {
        public static bool Enabled = true;

        public TileMatrixPatch(BinaryDatFile source, TileMatrix matrix, int index)
        {
            if (!Enabled)
                return;
            using (var mapData = source.GetReader($"mapdif{index}.mul"))
            using (var mapIndex = source.GetReader($"mapdifl{index}.mul"))
                if (mapData != null && mapIndex != null)
                    LandBlocks = PatchLand(matrix, mapData, mapIndex);

            using (var staData = source.GetReader($"stadif{index}.mul"))
            using (var staIndex = source.GetReader($"stadifl{index}.mul"))
            using (var staLookup = source.GetReader($"stadifi{index}.mul"))
                if (staData != null && staIndex != null && staLookup != null)
                    StaticBlocks = PatchStatics(matrix, staData, staIndex, staLookup);
        }

        public int LandBlocks { get; private set; }

        public int StaticBlocks { get; private set; }

        [MethodImpl(MethodImplOptions.Synchronized)]
        int PatchLand(TileMatrix matrix, BinaryReader data, BinaryReader index)
        {
            var count = (int)(index.BaseStream.Length / 4);
            for (var i = 0; i < count; ++i)
            {
                var blockID = index.ReadInt32();
                var x = blockID / matrix.BlockHeight;
                var y = blockID % matrix.BlockHeight;
                data.Skip(4);
                var tiles = new LandTile[64];
                data.ReadTMany(tiles, 192);
                matrix.SetLandBlock(x, y, tiles);
            }
            return count;
        }

        StaticTile[] TileBuffer = new StaticTile[128];

        [MethodImpl(MethodImplOptions.Synchronized)]
        unsafe int PatchStatics(TileMatrix matrix, BinaryReader data, BinaryReader index, BinaryReader lookup)
        {
            var count = (int)(index.BaseStream.Length / 4);

            var lists = new List<StaticTile>[8][];
            for (var x = 0; x < 8; ++x)
            {
                lists[x] = new List<StaticTile>[8];
                for (var y = 0; y < 8; ++y)
                    lists[x][y] = new List<StaticTile>();
            }

            for (var i = 0; i < count; ++i)
            {
                var blockID = index.ReadInt32();
                var blockX = blockID / matrix.BlockHeight;
                var blockY = blockID % matrix.BlockHeight;

                var offset = lookup.ReadInt32();
                var length = lookup.ReadInt32();
                lookup.ReadInt32(); // Extra

                if (offset < 0 || length <= 0)
                {
                    matrix.SetStaticBlock(blockX, blockY, matrix.EmptyStaticBlock);
                    continue;
                }
                data.Position(offset);

                var tileCount = length / 7;
                if (TileBuffer.Length < tileCount)
                    TileBuffer = new StaticTile[tileCount];
                var staTiles = TileBuffer;

                data.ReadTMany(staTiles, length);
                fixed (StaticTile* pTiles = staTiles)
                {
                    StaticTile* pCur = pTiles, pEnd = pTiles + tileCount;
                    while (pCur < pEnd)
                    {
                        lists[pCur->X & 0x7][pCur->Y & 0x7].Add(new StaticTile((ushort)pCur->ID, pCur->Z));
                        pCur++;
                    }

                    var tiles = new StaticTile[8][][];
                    for (var x = 0; x < 8; ++x)
                    {
                        tiles[x] = new StaticTile[8][];
                        for (var y = 0; y < 8; ++y)
                            tiles[x][y] = lists[x][y].ToArray();
                    }
                    matrix.SetStaticBlock(blockX, blockY, tiles);
                }
            }
            return count;
        }
    }
}