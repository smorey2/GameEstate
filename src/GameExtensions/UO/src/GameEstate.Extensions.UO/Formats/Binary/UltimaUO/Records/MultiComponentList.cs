using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static GameEstate.Formats.Binary.UltimaUO.Records.TileDataRecord;
using static GameEstate.Formats.Binary.UltimaUO.Records.TileMatrix;

namespace GameEstate.Formats.Binary.UltimaUO.Records
{
    public class MultiComponentList
    {
        public static readonly MultiComponentList Empty = new MultiComponentList();
        public static bool PostHSFormat;
        readonly TileDataRecord TileData;

        MultiComponentList()
        {
            Tiles = new StaticTile[0][][];
            List = new MultiTileEntry[0];
        }
        public MultiComponentList(TileDataRecord tileData, BinaryReader r)
        {
            TileData = tileData;
            var version = r.ReadInt32();

            Min = r.ReadVector2Int();
            Max = r.ReadVector2Int();
            Center = r.ReadVector2Int();
            Width = r.ReadInt32();
            Height = r.ReadInt32();

            var length = r.ReadInt32();

            var allTiles = List = new MultiTileEntry[length];
            for (var i = 0; i < length; ++i)
            {
                var id = r.ReadUInt16();
                if (version == 0 && id >= 0x4000)
                    id -= 0x4000;
                allTiles[i].ItemID = id;
                allTiles[i].OffsetX = r.ReadInt16();
                allTiles[i].OffsetY = r.ReadInt16();
                allTiles[i].OffsetZ = r.ReadInt16();
                allTiles[i].Flags = r.ReadInt32();
            }
            //else
            //    for (var i = 0; i < length; ++i)
            //    {
            //        allTiles[i].ItemID = r.ReadUInt16();
            //        allTiles[i].OffsetX = r.ReadInt16();
            //        allTiles[i].OffsetY = r.ReadInt16();
            //        allTiles[i].OffsetZ = r.ReadInt16();
            //        allTiles[i].Flags = r.ReadInt32();
            //    }

            var tiles = new List<StaticTile>[Width][];
            Tiles = new StaticTile[Width][][];
            for (var x = 0; x < Width; ++x)
            {
                tiles[x] = new List<StaticTile>[Height];
                Tiles[x] = new StaticTile[Height][];
                for (var y = 0; y < Height; ++y)
                    tiles[x][y] = new List<StaticTile>();
            }

            for (var i = 0; i < allTiles.Length; ++i)
                if (i == 0 || allTiles[i].Flags != 0)
                {
                    var xOffset = allTiles[i].OffsetX + Center.x;
                    var yOffset = allTiles[i].OffsetY + Center.y;
                    tiles[xOffset][yOffset].Add(new StaticTile(allTiles[i].ItemID, (sbyte)allTiles[i].OffsetZ));
                }

            for (var x = 0; x < Width; ++x)
                for (var y = 0; y < Height; ++y)
                    Tiles[x][y] = tiles[x][y].ToArray();
        }
        public MultiComponentList(TileDataRecord tileData, BinaryReader r, int count)
        {
            TileData = tileData;
            var allTiles = List = new MultiTileEntry[count];
            for (var i = 0; i < count; ++i)
            {
                allTiles[i].ItemID = r.ReadUInt16();
                allTiles[i].OffsetX = r.ReadInt16();
                allTiles[i].OffsetY = r.ReadInt16();
                allTiles[i].OffsetZ = r.ReadInt16();
                allTiles[i].Flags = r.ReadInt32();
                if (PostHSFormat)
                    r.ReadInt32(); // ??
                var e = allTiles[i];
                if (i == 0 || e.Flags != 0)
                {
                    if (e.OffsetX < Min.x)
                        Min.x = e.OffsetX;
                    if (e.OffsetY < Min.y)
                        Min.y = e.OffsetY;
                    if (e.OffsetX > Max.x)
                        Max.x = e.OffsetX;
                    if (e.OffsetY > Max.y)
                        Max.y = e.OffsetY;
                }
            }

            Center = new Vector2Int(-Min.x, -Min.y);
            Width = Max.x - Min.x + 1;
            Height = Max.y - Min.y + 1;

            var tiles = new List<StaticTile>[Width][];
            Tiles = new StaticTile[Width][][];
            for (var x = 0; x < Width; ++x)
            {
                tiles[x] = new List<StaticTile>[Height];
                Tiles[x] = new StaticTile[Height][];
                for (var y = 0; y < Height; ++y)
                    tiles[x][y] = new List<StaticTile>();
            }

            for (var i = 0; i < allTiles.Length; ++i)
                if (i == 0 || allTiles[i].Flags != 0)
                {
                    var xOffset = allTiles[i].OffsetX + Center.x;
                    var yOffset = allTiles[i].OffsetY + Center.y;
                    tiles[xOffset][yOffset].Add(new StaticTile(allTiles[i].ItemID, (sbyte)allTiles[i].OffsetZ));
                }

            for (var x = 0; x < Width; ++x)
                for (var y = 0; y < Height; ++y)
                    Tiles[x][y] = tiles[x][y].ToArray();
        }

        public Vector2Int Min;
        public Vector2Int Max;

        public Vector2Int Center;

        public int Width;
        public int Height;

        public StaticTile[][][] Tiles;
        public MultiTileEntry[] List;

        public void Add(int itemID, int x, int y, int z)
        {
            var vx = x + Center.x;
            var vy = y + Center.y;

            if (vx >= 0 && vx < Width && vy >= 0 && vy < Height)
            {
                var oldTiles = Tiles[vx][vy];
                for (var i = oldTiles.Length - 1; i >= 0; --i)
                {
                    var data = TileData.Items[itemID & TileData.MaxItem];
                    if (oldTiles[i].Z == z && (oldTiles[i].Height(TileData) > 0 == data.Height > 0))
                    {
                        var newIsRoof = (data.Flags & TileFlag.Roof) != 0;
                        var oldIsRoof = (TileData.Items[oldTiles[i].ID & TileData.MaxItem].Flags & TileFlag.Roof) != 0;
                        if (newIsRoof == oldIsRoof)
                            Remove(oldTiles[i].ID, x, y, z);
                    }
                }

                oldTiles = Tiles[vx][vy];
                var newTiles = new StaticTile[oldTiles.Length + 1];
                for (var i = 0; i < oldTiles.Length; ++i)
                    newTiles[i] = oldTiles[i];
                newTiles[oldTiles.Length] = new StaticTile((ushort)itemID, (sbyte)z);
                Tiles[vx][vy] = newTiles;

                var oldList = List;
                var newList = new MultiTileEntry[oldList.Length + 1];
                for (var i = 0; i < oldList.Length; ++i)
                    newList[i] = oldList[i];
                newList[oldList.Length] = new MultiTileEntry((ushort)itemID, (short)x, (short)y, (short)z, 1);
                List = newList;
                if (x < Min.x)
                    Min.x = x;
                if (y < Min.y)
                    Min.y = y;
                if (x > Max.x)
                    Max.x = x;
                if (y > Max.y)
                    Max.y = y;
            }
        }

        public void RemoveXYZH(int x, int y, int z, int minHeight)
        {
            var vx = x + Center.x;
            var vy = y + Center.y;

            if (vx >= 0 && vx < Width && vy >= 0 && vy < Height)
            {
                var oldTiles = Tiles[vx][vy];
                for (var i = 0; i < oldTiles.Length; ++i)
                {
                    var tile = oldTiles[i];
                    if (tile.Z == z && tile.Height(TileData) >= minHeight)
                    {
                        var newTiles = new StaticTile[oldTiles.Length - 1];
                        for (var j = 0; j < i; ++j)
                            newTiles[j] = oldTiles[j];
                        for (var j = i + 1; j < oldTiles.Length; ++j)
                            newTiles[j - 1] = oldTiles[j];
                        Tiles[vx][vy] = newTiles;
                        break;
                    }
                }

                var oldList = List;
                for (var i = 0; i < oldList.Length; ++i)
                {
                    var tile = oldList[i];
                    if (tile.OffsetX == (short)x && tile.OffsetY == (short)y && tile.OffsetZ == (short)z && TileData.Items[tile.ItemID & TileData.MaxItem].Height >= minHeight)
                    {
                        var newList = new MultiTileEntry[oldList.Length - 1];
                        for (var j = 0; j < i; ++j)
                            newList[j] = oldList[j];
                        for (var j = i + 1; j < oldList.Length; ++j)
                            newList[j - 1] = oldList[j];
                        List = newList;
                        break;
                    }
                }
            }
        }

        public void Remove(int itemID, int x, int y, int z)
        {
            var vx = x + Center.x;
            var vy = y + Center.y;

            if (vx >= 0 && vx < Width && vy >= 0 && vy < Height)
            {
                var oldTiles = Tiles[vx][vy];
                for (var i = 0; i < oldTiles.Length; ++i)
                {
                    var tile = oldTiles[i];
                    if (tile.ID == itemID && tile.Z == z)
                    {
                        var newTiles = new StaticTile[oldTiles.Length - 1];
                        for (var j = 0; j < i; ++j)
                            newTiles[j] = oldTiles[j];
                        for (var j = i + 1; j < oldTiles.Length; ++j)
                            newTiles[j - 1] = oldTiles[j];
                        Tiles[vx][vy] = newTiles;
                        break;
                    }
                }

                var oldList = List;
                for (var i = 0; i < oldList.Length; ++i)
                {
                    var tile = oldList[i];
                    if (tile.ItemID == itemID && tile.OffsetX == (short)x && tile.OffsetY == (short)y && tile.OffsetZ == (short)z)
                    {
                        var newList = new MultiTileEntry[oldList.Length - 1];
                        for (var j = 0; j < i; ++j)
                            newList[j] = oldList[j];
                        for (var j = i + 1; j < oldList.Length; ++j)
                            newList[j - 1] = oldList[j];
                        List = newList;
                        break;
                    }
                }
            }
        }

        public void Resize(int newWidth, int newHeight)
        {
            int oldWidth = Width, oldHeight = Height;
            var oldTiles = Tiles;
            var totalLength = 0;
            var newTiles = new StaticTile[newWidth][][];
            for (var x = 0; x < newWidth; ++x)
            {
                newTiles[x] = new StaticTile[newHeight][];
                for (var y = 0; y < newHeight; ++y)
                {
                    if (x < oldWidth && y < oldHeight)
                        newTiles[x][y] = oldTiles[x][y];
                    else
                        newTiles[x][y] = new StaticTile[0];
                    totalLength += newTiles[x][y].Length;
                }
            }

            Tiles = newTiles;
            List = new MultiTileEntry[totalLength];
            Width = newWidth;
            Height = newHeight;
            Min = Vector2Int.zero;
            Max = Vector2Int.zero;
            var index = 0;
            for (var x = 0; x < newWidth; ++x)
                for (var y = 0; y < newHeight; ++y)
                {
                    var tiles = newTiles[x][y];
                    for (var i = 0; i < tiles.Length; ++i)
                    {
                        var tile = tiles[i];
                        var vx = x - Center.x;
                        var vy = y - Center.y;
                        if (vx < Min.x)
                            Min.x = vx;
                        if (vy < Min.y)
                            Min.y = vy;
                        if (vx > Max.x)
                            Max.x = vx;
                        if (vy > Max.y)
                            Max.y = vy;
                        List[index++] = new MultiTileEntry(tile.ID, (short)vx, (short)vy, tile.Z, 1);
                    }
                }
        }

        public MultiComponentList(MultiComponentList toCopy)
        {
            Min = toCopy.Min;
            Max = toCopy.Max;
            Center = toCopy.Center;
            Width = toCopy.Width;
            Height = toCopy.Height;

            Tiles = new StaticTile[Width][][];
            for (var x = 0; x < Width; ++x)
            {
                Tiles[x] = new StaticTile[Height][];
                for (var y = 0; y < Height; ++y)
                {
                    Tiles[x][y] = new StaticTile[toCopy.Tiles[x][y].Length];
                    for (var i = 0; i < Tiles[x][y].Length; ++i)
                        Tiles[x][y][i] = toCopy.Tiles[x][y][i];
                }
            }

            List = new MultiTileEntry[toCopy.List.Length];
            for (var i = 0; i < List.Length; ++i)
                List[i] = toCopy.List[i];
        }

        public void Serialize(BinaryWriter w)
        {
            w.Write(1); // version;

            w.Write(Min);
            w.Write(Max);
            w.Write(Center);

            w.Write(Width);
            w.Write(Height);

            w.Write(List.Length);
            for (var i = 0; i < List.Length; ++i)
            {
                var ent = List[i];
                w.Write(ent.ItemID);
                w.Write(ent.OffsetX);
                w.Write(ent.OffsetY);
                w.Write(ent.OffsetZ);
                w.Write(ent.Flags);
            }
        }
    }
}