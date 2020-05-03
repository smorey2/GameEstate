//// SKY: Should be replaced with List<StaticTile>
//namespace GameEstate.Formats.Binary.UltimaUO.Records
//{
//    public class TileList
//    {
//        StaticTile[] _Tiles = new StaticTile[8];
//        static StaticTile[] Empty = new StaticTile[0];

//        public int Count { get; private set; }

//        public void AddRange(StaticTile[] tiles)
//        {
//            if ((Count + tiles.Length) > _Tiles.Length)
//            {
//                var old = _Tiles;
//                _Tiles = new StaticTile[(Count + tiles.Length) * 2];
//                for (var i = 0; i < old.Length; ++i)
//                    _Tiles[i] = old[i];
//            }
//            for (var i = 0; i < tiles.Length; ++i)
//                _Tiles[Count++] = tiles[i];
//        }

//        public void Add(ushort id, sbyte z)
//        {
//            if ((Count + 1) > _Tiles.Length)
//            {
//                var old = _Tiles;
//                _Tiles = new StaticTile[old.Length * 2];
//                for (var i = 0; i < old.Length; ++i)
//                    _Tiles[i] = old[i];
//            }
//            _Tiles[Count].ID = id;
//            _Tiles[Count].Z = z;
//            ++Count;
//        }

//        public StaticTile[] ToArray()
//        {
//            if (Count == 0)
//                return Empty;
//            var tiles = new StaticTile[Count];
//            for (var i = 0; i < Count; ++i)
//                tiles[i] = _Tiles[i];
//            Count = 0;
//            return tiles;
//        }
//    }
//}