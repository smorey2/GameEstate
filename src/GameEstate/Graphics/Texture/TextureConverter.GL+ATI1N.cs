using System;
using System.IO;

namespace GameEstate.Graphics.Texture
{
    static partial class TextureConverter
    {
        public static void ATI1N(BinaryReader r, Span<byte> data, int w, int h)
        {
            var blockCountX = (w + 3) / 4;
            var blockCountY = (h + 3) / 4;

            for (var j = 0; j < blockCountY; j++)
                for (var i = 0; i < blockCountX; i++)
                {
                    var block1 = r.ReadUInt64();
                    var ofs = ((i * 4) + (j * 4 * w)) * 4;

                    _8BitBlock(i * 4, w, ofs, block1, data, w * 4);

                    for (var y = 0; y < 4; y++)
                        for (var x = 0; x < 4; x++)
                        {
                            var dataIndex = ofs + ((x + (y * w)) * 4);
                            if (data.Length < dataIndex + 3)
                                break;

                            data[dataIndex + 1] = data[dataIndex];
                            data[dataIndex + 2] = data[dataIndex];
                            data[dataIndex + 3] = byte.MaxValue;
                        }
                }
        }
    }
}
