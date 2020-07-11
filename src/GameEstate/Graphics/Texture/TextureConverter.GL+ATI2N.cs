using System;
using System.IO;

namespace GameEstate.Graphics.Texture
{
    static partial class TextureConverter
    {
        public static void ATI2N(BinaryReader r, Span<byte> data, int w, int h, bool normalize)
        {
            var blockCountX = (w + 3) / 4;
            var blockCountY = (h + 3) / 4;

            for (var j = 0; j < blockCountY; j++)
                for (var i = 0; i < blockCountX; i++)
                {
                    var block1 = r.ReadUInt64();
                    var block2 = r.ReadUInt64();
                    var ofs = ((i * 4) + (j * 4 * w)) * 4;
                    _8BitBlock(i * 4, w, ofs + 2, block1, data, w * 4); //r
                    _8BitBlock(i * 4, w, ofs + 1, block2, data, w * 4); //g

                    for (var y = 0; y < 4; y++)
                        for (var x = 0; x < 4; x++)
                        {
                            var dataIndex = ofs + ((x + (y * w)) * 4);
                            if (data.Length < dataIndex + 3)
                                break;

                            data[dataIndex + 0] = 0; //b
                            data[dataIndex + 3] = byte.MaxValue;
                            if (normalize)
                            {
                                var swizzleR = (data[dataIndex + 2] * 2) - 255;     // premul R
                                var swizzleG = (data[dataIndex + 1] * 2) - 255;     // premul G
                                var deriveB = (int)System.Math.Sqrt((255 * 255) - (swizzleR * swizzleR) - (swizzleG * swizzleG));
                                data[dataIndex + 2] = ClampColor((swizzleR / 2) + 128); // unpremul R and normalize (128 = forward, or facing viewer)
                                data[dataIndex + 1] = ClampColor((swizzleG / 2) + 128); // unpremul G and normalize
                                data[dataIndex + 0] = ClampColor((deriveB / 2) + 128);  // unpremul B and normalize
                            }
                        }
                }
        }
    }
}
