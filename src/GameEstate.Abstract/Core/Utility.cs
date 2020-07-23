using System.Globalization;

namespace GameEstate.Core
{
    public static class Utility
    {
        public static void Swap<T>(ref T a, ref T b)
        {
            var tmp = a;
            a = b;
            b = tmp;
        }

        public static bool TryParseInt32(string s, out int result) => !s.StartsWith("0x") ? int.TryParse(s, out result) : int.TryParse(s.Substring(2), NumberStyles.HexNumber, null, out result);

        public static short Reverse(short value) => (short)(
                ((value & 0xFF00) >> 8) << 0 |
                ((value & 0x00FF) >> 0) << 8);
        public static ushort Reverse(ushort value) => (ushort)(
                ((value & 0xFF00) >> 8) << 0 |
                ((value & 0x00FF) >> 0) << 8);
        public static int Reverse(int value) => (int)(
                (((uint)value & 0xFF000000) >> 24) << 0 |
                (((uint)value & 0x00FF0000) >> 16) << 8 |
                (((uint)value & 0x0000FF00) >> 8) << 16 |
                (((uint)value & 0x000000FF) >> 0) << 24);
        public static uint Reverse(uint value) => (uint)(
                ((value & 0xFF000000) >> 24) << 0 |
                ((value & 0x00FF0000) >> 16) << 8 |
                ((value & 0x0000FF00) >> 8) << 16 |
                ((value & 0x000000FF) >> 0) << 24);
        public static long Reverse(long value) => (long)(
                (((ulong)value & 0xFF00000000000000UL) >> 56) << 0 |
                (((ulong)value & 0x00FF000000000000UL) >> 48) << 8 |
                (((ulong)value & 0x0000FF0000000000UL) >> 40) << 16 |
                (((ulong)value & 0x000000FF00000000UL) >> 32) << 24 |
                (((ulong)value & 0x00000000FF000000UL) >> 24) << 32 |
                (((ulong)value & 0x0000000000FF0000UL) >> 16) << 40 |
                (((ulong)value & 0x000000000000FF00UL) >> 8) << 48 |
                (((ulong)value & 0x00000000000000FFUL) >> 0) << 56);
        public static ulong Reverse(ulong value) => (ulong)(
                ((value & 0xFF00000000000000UL) >> 56) << 0 |
                ((value & 0x00FF000000000000UL) >> 48) << 8 |
                ((value & 0x0000FF0000000000UL) >> 40) << 16 |
                ((value & 0x000000FF00000000UL) >> 32) << 24 |
                ((value & 0x00000000FF000000UL) >> 24) << 32 |
                ((value & 0x0000000000FF0000UL) >> 16) << 40 |
                ((value & 0x000000000000FF00UL) >> 8) << 48 |
                ((value & 0x00000000000000FFUL) >> 0) << 56);
    }
}