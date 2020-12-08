using System;
using System.Runtime.CompilerServices;

namespace GameEstate.Graphics.Texture
{
    public class EtcDecoder
    {
        public void DecompressETC2(byte[] input, int width, int height, byte[] output)
        {
            var bcw = (width + 3) / 4;
            var bch = (height + 3) / 4;
            var clen_last = (width + 3) % 4 + 1;
            var d = 0;
            for (var t = 0; t < bch; t++)
                for (var s = 0; s < bcw; s++, d += 8)
                {
                    DecodeEtc2Block(input, d);
                    var clen = (s < bcw - 1 ? 4 : clen_last) * 4;
                    for (int i = 0, y = height - t * 4 - 1; i < 4 && y >= 0; i++, y--)
                        Buffer.BlockCopy(_buf, i * 4 * 4, output, y * 4 * width + s * 4 * 4, clen);
                }
        }

        public void DecompressETC2A8(byte[] input, int width, int height, byte[] output)
        {
            var bcw = (width + 3) / 4;
            var bch = (height + 3) / 4;
            var clen_last = (width + 3) % 4 + 1;
            var d = 0;
            for (var t = 0; t < bch; t++)
                for (var s = 0; s < bcw; s++, d += 16)
                {
                    DecodeEtc2Block(input, d + 8);
                    DecodeEtc2a8Block(input, d);
                    var clen = (s < bcw - 1 ? 4 : clen_last) * 4;
                    for (int i = 0, y = height - t * 4 - 1; i < 4 && y >= 0; i++, y--)
                        Buffer.BlockCopy(_buf, i * 4 * 4, output, y * 4 * width + s * 4 * 4, clen);
                }
        }

        void DecodeEtc2Block(byte[] data, int offset)
        {
            var j = (ushort)(data[offset + 6] << 8 | data[offset + 7]);
            var k = (ushort)(data[offset + 4] << 8 | data[offset + 5]);

            if ((data[offset + 3] & 2) != 0)
            {
                var r = (byte)(data[offset + 0] & 0xf8);
                var dr = (short)((data[offset + 0] << 3 & 0x18) - (data[offset + 0] << 3 & 0x20));
                var g = (byte)(data[offset + 1] & 0xf8);
                var dg = (short)((data[offset + 1] << 3 & 0x18) - (data[offset + 1] << 3 & 0x20));
                var b = (byte)(data[offset + 2] & 0xf8);
                var db = (short)((data[offset + 2] << 3 & 0x18) - (data[offset + 2] << 3 & 0x20));
                if (r + dr < 0 || r + dr > 255)
                {
                    // T
                    unchecked
                    {
                        _c[0, 0] = (byte)(data[offset + 0] << 3 & 0xc0 | data[offset + 0] << 4 & 0x30 | data[offset + 0] >> 1 & 0xc | data[offset + 0] & 3);
                        _c[0, 1] = (byte)(data[offset + 1] & 0xf0 | data[offset + 1] >> 4);
                        _c[0, 2] = (byte)(data[offset + 1] & 0x0f | data[offset + 1] << 4);
                        _c[1, 0] = (byte)(data[offset + 2] & 0xf0 | data[offset + 2] >> 4);
                        _c[1, 1] = (byte)(data[offset + 2] & 0x0f | data[offset + 2] << 4);
                        _c[1, 2] = (byte)(data[offset + 3] & 0xf0 | data[offset + 3] >> 4);
                    }
                    var d = Etc2DistanceTable[data[offset + 3] >> 1 & 6 | data[offset + 3] & 1];
                    uint[] color_set =
                    {
                        ApplicateColorRaw(_c, 0),
                        ApplicateColor(_c, 1, d),
                        ApplicateColorRaw(_c, 1),
                        ApplicateColor(_c, 1, -d)
                    };
                    for (var i = 0; i < 16; i++, j >>= 1, k >>= 1)
                        _buf[WriteOrderTable[i]] = color_set[k << 1 & 2 | j & 1];
                }
                else if (g + dg < 0 || g + dg > 255)
                {
                    // H
                    unchecked
                    {
                        _c[0, 0] = (byte)(data[offset + 0] << 1 & 0xf0 | data[offset + 0] >> 3 & 0xf);
                        _c[0, 1] = (byte)(data[offset + 0] << 5 & 0xe0 | data[offset + 1] & 0x10);
                        _c[0, 1] |= (byte)(_c[0, 1] >> 4);
                        _c[0, 2] = (byte)(data[offset + 1] & 8 | data[offset + 1] << 1 & 6 | data[offset + 2] >> 7);
                        _c[0, 2] |= (byte)(_c[0, 2] << 4);
                        _c[1, 0] = (byte)(data[offset + 2] << 1 & 0xf0 | data[offset + 2] >> 3 & 0xf);
                        _c[1, 1] = (byte)(data[offset + 2] << 5 & 0xe0 | data[offset + 3] >> 3 & 0x10);
                        _c[1, 1] |= (byte)(_c[1, 1] >> 4);
                        _c[1, 2] = (byte)(data[offset + 3] << 1 & 0xf0 | data[offset + 3] >> 3 & 0xf);
                    }
                    var di = data[offset + 3] & 4 | data[offset + 3] << 1 & 2;
                    if (_c[0, 0] > _c[1, 0] || (_c[0, 0] == _c[1, 0] && (_c[0, 1] > _c[1, 1] || (_c[0, 1] == _c[1, 1] && _c[0, 2] >= _c[1, 2]))))
                        ++di;
                    var d = Etc2DistanceTable[di];
                    uint[] color_set =
                    {
                        ApplicateColor(_c, 0, d),
                        ApplicateColor(_c, 0, -d),
                        ApplicateColor(_c, 1, d),
                        ApplicateColor(_c, 1, -d)
                    };
                    for (var i = 0; i < 16; i++, j >>= 1, k >>= 1)
                        _buf[WriteOrderTable[i]] = color_set[k << 1 & 2 | j & 1];
                }
                else if (b + db < 0 || b + db > 255)
                {
                    // planar
                    unchecked
                    {
                        _c[0, 0] = (byte)(data[offset + 0] << 1 & 0xfc | data[offset + 0] >> 5 & 3);
                        _c[0, 1] = (byte)(data[offset + 0] << 7 & 0x80 | data[offset + 1] & 0x7e | data[offset + 0] & 1);
                        _c[0, 2] = (byte)(data[offset + 1] << 7 & 0x80 | data[offset + 2] << 2 & 0x60 | data[offset + 2] << 3 & 0x18 | data[offset + 3] >> 5 & 4);
                        _c[0, 2] |= (byte)(_c[0, 2] >> 6);
                        _c[1, 0] = (byte)(data[offset + 3] << 1 & 0xf8 | data[offset + 3] << 2 & 4 | data[offset + 3] >> 5 & 3);
                        _c[1, 1] = (byte)(data[offset + 4] & 0xfe | data[offset + 4] >> 7);
                        _c[1, 2] = (byte)(data[offset + 4] << 7 & 0x80 | data[offset + 5] >> 1 & 0x7c);
                        _c[1, 2] |= (byte)(_c[1, 2] >> 6);
                        _c[2, 0] = (byte)(data[offset + 5] << 5 & 0xe0 | data[offset + 6] >> 3 & 0x1c | data[offset + 5] >> 1 & 3);
                        _c[2, 1] = (byte)(data[offset + 6] << 3 & 0xf8 | data[offset + 7] >> 5 & 0x6 | data[offset + 6] >> 4 & 1);
                        _c[2, 2] = (byte)(data[offset + 7] << 2 | data[offset + 7] >> 4 & 3);
                    }
                    for (int y = 0, i = 0; y < 4; y++)
                        for (var x = 0; x < 4; x++, i++)
                        {
                            var ri = Clamp((x * (_c[1, 0] - _c[0, 0]) + y * (_c[2, 0] - _c[0, 0]) + 4 * _c[0, 0] + 2) >> 2);
                            var gi = Clamp((x * (_c[1, 1] - _c[0, 1]) + y * (_c[2, 1] - _c[0, 1]) + 4 * _c[0, 1] + 2) >> 2);
                            var bi = Clamp((x * (_c[1, 2] - _c[0, 2]) + y * (_c[2, 2] - _c[0, 2]) + 4 * _c[0, 2] + 2) >> 2);
                            _buf[i] = Color(ri, gi, bi, 255);
                        }
                }
                else
                {
                    // differential
                    var code = new[] { (byte)(data[offset + 3] >> 5), (byte)(data[offset + 3] >> 2 & 7) };
                    var ti = data[offset + 3] & 1;
                    unchecked
                    {
                        _c[0, 0] = (byte)(r | r >> 5);
                        _c[0, 1] = (byte)(g | g >> 5);
                        _c[0, 2] = (byte)(b | b >> 5);
                        _c[1, 0] = (byte)(r + dr);
                        _c[1, 1] = (byte)(g + dg);
                        _c[1, 2] = (byte)(b + db);
                        _c[1, 0] |= (byte)(_c[1, 0] >> 5);
                        _c[1, 1] |= (byte)(_c[1, 1] >> 5);
                        _c[1, 2] |= (byte)(_c[1, 2] >> 5);
                    }
                    for (var i = 0; i < 16; i++, j >>= 1, k >>= 1)
                    {
                        var s = Etc1SubblockTable[ti, i];
                        var index = k << 1 & 2 | j & 1;
                        var m = Etc1ModifierTable[code[s], index];
                        _buf[WriteOrderTable[i]] = ApplicateColor(_c, s, m);
                    }
                }
            }
            else
            {
                // individual
                var code = new[] { (byte)(data[offset + 3] >> 5), (byte)(data[offset + 3] >> 2 & 7) };
                var ti = data[offset + 3] & 1;
                unchecked
                {
                    _c[0, 0] = (byte)(data[offset + 0] & 0xf0 | data[offset + 0] >> 4);
                    _c[1, 0] = (byte)(data[offset + 0] & 0x0f | data[offset + 0] << 4);
                    _c[0, 1] = (byte)(data[offset + 1] & 0xf0 | data[offset + 1] >> 4);
                    _c[1, 1] = (byte)(data[offset + 1] & 0x0f | data[offset + 1] << 4);
                    _c[0, 2] = (byte)(data[offset + 2] & 0xf0 | data[offset + 2] >> 4);
                    _c[1, 2] = (byte)(data[offset + 2] & 0x0f | data[offset + 2] << 4);
                }
                for (var i = 0; i < 16; i++, j >>= 1, k >>= 1)
                {
                    var s = Etc1SubblockTable[ti, i];
                    var index = k << 1 & 2 | j & 1;
                    var m = Etc1ModifierTable[code[s], index];
                    _buf[WriteOrderTable[i]] = ApplicateColor(_c, s, m);
                }
            }
        }

        void DecodeEtc2a8Block(byte[] data, int offset)
        {
            if ((data[offset + 1] & 0xf0) != 0)
            {
                var mult = unchecked((byte)(data[offset + 1] >> 4));
                var ti = data[offset + 1] & 0xf;
                var l =
                    data[offset + 7] | (uint)data[offset + 6] << 8 |
                    (uint)data[offset + 5] << 16 | (uint)data[offset + 4] << 24 |
                    (ulong)data[offset + 3] << 32 | (ulong)data[offset + 2] << 40;
                for (var i = 0; i < 16; i++, l >>= 3)
                {
                    var c = _buf[WriteOrderTableRev[i]];
                    c &= 0x00FFFFFF;
                    c |= unchecked((uint)(Clamp(data[offset + 0] + mult * Etc2AlphaModTable[ti, l & 7]) << 24));
                    _buf[WriteOrderTableRev[i]] = c;
                }
            }
            else
            {
                for (var i = 0; i < 16; i++)
                {
                    var c = _buf[WriteOrderTableRev[i]];
                    c &= 0x00FFFFFF;
                    c |= unchecked((uint)(data[offset + 0] << 24));
                    _buf[WriteOrderTableRev[i]] = c;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static uint Color(int r, int g, int b, int a) => unchecked((uint)(r << 16 | g << 8 | b | a << 24));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int Clamp(int n) => n < 0 ? 0 : n > 255 ? 255 : n;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static uint ApplicateColor(byte[,] c, int o, int m) => Color(Clamp(c[o, 0] + m), Clamp(c[o, 1] + m), Clamp(c[o, 2] + m), 255);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static uint ApplicateColorRaw(byte[,] c, int o) => Color(c[o, 0], c[o, 1], c[o, 2], 255);

        static readonly byte[] WriteOrderTable = { 0, 4, 8, 12, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15 };
        static readonly byte[] WriteOrderTableRev = { 15, 11, 7, 3, 14, 10, 6, 2, 13, 9, 5, 1, 12, 8, 4, 0 };
        static readonly int[,] Etc1ModifierTable =
        {
            { 2, 8, -2, -8, },
            { 5, 17, -5, -17, },
            { 9, 29, -9, -29,},
            { 13, 42, -13, -42, },
            { 18, 60, -18, -60, },
            { 24, 80, -24, -80, },
            { 33, 106, -33, -106, },
            { 47, 183, -47, -183, }
        };
        static readonly byte[,] Etc1SubblockTable =
       {
            {0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1},
            {0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1}
        };
        static readonly byte[] Etc2DistanceTable = { 3, 6, 11, 16, 23, 32, 41, 64 };
        static readonly sbyte[,] Etc2AlphaModTable =
        {
            {-3, -6,  -9, -15, 2, 5, 8, 14},
            {-3, -7, -10, -13, 2, 6, 9, 12},
            {-2, -5,  -8, -13, 1, 4, 7, 12},
            {-2, -4,  -6, -13, 1, 3, 5, 12},
            {-3, -6,  -8, -12, 2, 5, 7, 11},
            {-3, -7,  -9, -11, 2, 6, 8, 10},
            {-4, -7,  -8, -11, 3, 6, 7, 10},
            {-3, -5,  -8, -11, 2, 4, 7, 10},
            {-2, -6,  -8, -10, 1, 5, 7,  9},
            {-2, -5,  -8, -10, 1, 4, 7,  9},
            {-2, -4,  -8, -10, 1, 3, 7,  9},
            {-2, -5,  -7, -10, 1, 4, 6,  9},
            {-3, -4,  -7, -10, 2, 3, 6,  9},
            {-1, -2,  -3, -10, 0, 1, 2,  9},
            {-4, -6,  -8,  -9, 3, 5, 7,  8},
            {-3, -5,  -7,  -9, 2, 4, 6,  8}
        };

        readonly uint[] _buf = new uint[16];
        byte[,] _c = new byte[3, 3];
    }
}
