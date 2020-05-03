using GameEstate.Core;
using System;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.Binary.UltimaUO.Records
{
    public class UOPIndex
    {
        class UOPEntry : IComparable<UOPEntry>
        {
            public UOPEntry(int offset, int length)
            {
                Offset = offset;
                Length = length;
                Order = 0;
            }

            public int Offset;
            public int Length;
            public int Order;

            public int CompareTo(UOPEntry other) => Order.CompareTo(other.Order);
        }

        class OffsetComparer : IComparer<UOPEntry>
        {
            public static readonly IComparer<UOPEntry> Instance = new OffsetComparer();
            public int Compare(UOPEntry x, UOPEntry y) => x.Offset.CompareTo(y.Offset);
        }

        readonly int _length;
        readonly UOPEntry[] _entries;

        public UOPIndex(BinaryReader r)
        {
            _length = (int)r.BaseStream.Length;
            if (r.ReadInt32() != 0x50594D)
                throw new ArgumentException("Invalid UOP file.");
            Version = r.ReadInt32();
            r.ReadInt32();

            var nextTable = r.ReadInt32();
            var entries = new List<UOPEntry>();
            do
            {
                r.Position(nextTable);
                var count = r.ReadInt32();
                nextTable = r.ReadInt32();
                r.ReadInt32();
                for (var i = 0; i < count; ++i)
                {
                    var offset = r.ReadInt32();
                    if (offset == 0)
                    {
                        r.Skip(30);
                        continue;
                    }
                    r.ReadInt64();
                    var length = r.ReadInt32();
                    entries.Add(new UOPEntry(offset, length));
                    r.Skip(18);
                }
            }
            while (nextTable != 0 && nextTable < _length);

            entries.Sort(OffsetComparer.Instance);

            for (var i = 0; i < entries.Count; ++i)
            {
                r.Position(entries[i].Offset + 2);
                var dataOffset = r.ReadInt16();
                entries[i].Offset += 4 + dataOffset;
                r.Skip(dataOffset);
                entries[i].Order = r.ReadInt32();
            }

            entries.Sort();
            _entries = entries.ToArray();
        }

        public int Version;

        public int Lookup(int offset)
        {
            var total = 0;
            for (var i = 0; i < _entries.Length; ++i)
            {
                var newTotal = total + _entries[i].Length;
                if (offset < newTotal)
                    return _entries[i].Offset + (offset - total);
                total = newTotal;
            }
            return _length;
        }
    }
}