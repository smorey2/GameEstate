using GameEstate.Core;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary.UltimaUO.Records
{
    public class TileDataRecord
    {
        [Flags]
        public enum TileFlag : long
        {
            None = 0x00000000,
            Background = 0x00000001,
            Weapon = 0x00000002,
            Transparent = 0x00000004,
            Translucent = 0x00000008,
            Wall = 0x00000010,
            Damaging = 0x00000020,
            Impassable = 0x00000040,
            Wet = 0x00000080,
            Unknown1 = 0x00000100,
            Surface = 0x00000200,
            Bridge = 0x00000400,
            Generic = 0x00000800,
            Window = 0x00001000,
            NoShoot = 0x00002000,
            ArticleA = 0x00004000,
            ArticleAn = 0x00008000,
            Internal = 0x00010000,
            Foliage = 0x00020000,
            PartialHue = 0x00040000,
            Unknown2 = 0x00080000,
            Map = 0x00100000,
            Container = 0x00200000,
            Wearable = 0x00400000,
            LightSource = 0x00800000,
            Animation = 0x01000000,
            NoDiagonal = 0x02000000,
            Unknown3 = 0x04000000,
            Armor = 0x08000000,
            Roof = 0x10000000,
            Door = 0x20000000,
            StairBack = 0x40000000,
            StairRight = 0x80000000
        }

        [DebuggerDisplay("{Name}")]
        public struct LandData
        {
            public LandData(TileFlag flags, short texture, string name)
            {
                Flags = flags;
                Texture = texture;
                Name = name;
            }

            public TileFlag Flags;
            public short Texture;
            public string Name;
        }

        [DebuggerDisplay("{Name}")]
        public struct ItemData
        {
            public ItemData(TileFlag flags, byte weight, byte quality, byte quantity, byte value, byte height, string name)
            {
                Flags = flags;
                Weight = weight;
                Quality = quality;
                Quantity = quantity;
                Value = value;
                Height = height;
                Name = name;
            }

            public TileFlag Flags;
            public byte Weight;
            public byte Quality;
            public byte Quantity;
            public byte Value;
            public byte Height;
            public string Name;

            public int CalcHeight => (Flags & TileFlag.Bridge) != 0 ? Height / 2 : Height;
            public bool Bridge
            {
                get => (Flags & TileFlag.Bridge) != 0;
                set => Flags = value ? Flags | TileFlag.Bridge : Flags & ~TileFlag.Bridge;
            }
            public bool Impassable
            {
                get => (Flags & TileFlag.Impassable) != 0;
                set => Flags = value ? Flags | TileFlag.Impassable : Flags & ~TileFlag.Impassable;
            }
            public bool Surface
            {
                get => (Flags & TileFlag.Surface) != 0;
                set => Flags = value ? Flags | TileFlag.Surface : Flags & ~TileFlag.Surface;
            }
        }

        public LandData[] Lands;
        public ItemData[] Items;
        public int MaxLand;
        public int MaxItem;

        public Task ReadAsync(BinaryDatFile source)
        {
            var r = source.GetReader("tiledata.mul");
            int ver;
            switch (r.BaseStream.Length)
            {
                case 3188736: ver = 7090; Items = new ItemData[0x10000]; break; // 7.0.9.0
                case 1644544: ver = 7000; Items = new ItemData[0x8000]; break; // 7.0.0.0
                default: ver = 0; Items = new ItemData[0x4000]; break;
            }
            Lands = new LandData[0x4000];
            if (ver == 7090)
                for (var i = 0; i < Lands.Length; ++i)
                {
                    if (i == 1 || (i > 0 && (i & 0x1F) == 0))
                        r.ReadInt32(); // header
                    Lands[i] = new LandData((TileFlag)r.ReadInt64(), r.ReadInt16(), r.ReadASCII(20, ASCIIFormat.ZeroTerminated));
                }
            else
                for (var i = 0; i < Lands.Length; ++i)
                {
                    if ((i & 0x1F) == 0)
                        r.ReadInt32(); // header
                    Lands[i] = new LandData((TileFlag)r.ReadInt32(), r.ReadInt16(), r.ReadASCII(20, ASCIIFormat.ZeroTerminated));
                }
            for (var i = 0; i < Items.Length; ++i)
            {
                if ((i & 0x1F) == 0)
                    r.ReadInt32(); // header
                var flags = (TileFlag)(ver == 7090 ? r.ReadInt64() : r.ReadInt32());
                var weight = r.ReadByte();
                var quality = r.ReadByte();
                r.ReadInt16();
                r.ReadByte();
                var quantity = r.ReadByte();
                r.ReadInt32();
                r.ReadByte();
                var value = r.ReadByte();
                var height = r.ReadByte();
                Items[i] = new ItemData(flags, weight, quality, quantity, value, height, r.ReadASCII(20, ASCIIFormat.ZeroTerminated));
            }
            MaxLand = Lands.Length - 1;
            MaxItem = Items.Length - 1;
            return Task.CompletedTask;
        }
    }
}