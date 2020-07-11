using SkiaSharp;
using System;
using System.IO;

namespace GameEstate.Graphics.Texture
{
    static partial class TextureConverter
    {
        public static void DXT5(SKImageInfo imageInfo, BinaryReader r, Span<byte> data, int w, int h, bool yCoCg, bool normalize, bool invert, bool hemiOct)
        {
            var blockCountX = (w + 3) / 4;
            var blockCountY = (h + 3) / 4;

            for (var j = 0; j < blockCountY; j++)
                for (var i = 0; i < blockCountX; i++)
                {
                    var blockAlpha = r.ReadUInt64();
                    var blockStorage = r.ReadBytes(8);
                    var ofs = (i * 16) + (j * 4 * imageInfo.RowBytes);
                    DXT1Block(i * 4, j * 4, imageInfo.Width, blockStorage, data, imageInfo.RowBytes);
                    _8BitBlock(i * 4, imageInfo.Width, ofs + 3, blockAlpha, data, imageInfo.RowBytes);

                    for (var y = 0; y < 4; y++)
                        for (var x = 0; x < 4; x++)
                        {
                            var dataIndex = ofs + ((x * 4) + (y * imageInfo.RowBytes));
                            if ((i * 4) + x >= imageInfo.Width || data.Length < dataIndex + 3)
                                break;

                            if (yCoCg)
                            {
                                var s = (data[dataIndex + 0] >> 3) + 1;
                                var co = (data[dataIndex + 2] - 128) / s;
                                var cg = (data[dataIndex + 1] - 128) / s;

                                data[dataIndex + 2] = ClampColor(data[dataIndex + 3] + co - cg);
                                data[dataIndex + 1] = ClampColor(data[dataIndex + 3] + cg);
                                data[dataIndex + 0] = ClampColor(data[dataIndex + 3] - co - cg);
                                data[dataIndex + 3] = 255; // TODO: yCoCg should have an alpha too?
                            }

                            if (normalize)
                            {
                                if (hemiOct)
                                {
                                    var nx = ((data[dataIndex + 3] + data[dataIndex + 1]) / 255.0f) - 1.003922f;
                                    var ny = (data[dataIndex + 3] - data[dataIndex + 1]) / 255.0f;
                                    var nz = 1 - Math.Abs(nx) - Math.Abs(ny);

                                    var l = (float)Math.Sqrt((nx * nx) + (ny * ny) + (nz * nz));
                                    data[dataIndex + 3] = data[dataIndex + 2]; //r to alpha
                                    data[dataIndex + 2] = (byte)(((nx / l * 0.5f) + 0.5f) * 255);
                                    data[dataIndex + 1] = (byte)(((ny / l * 0.5f) + 0.5f) * 255);
                                    data[dataIndex + 0] = (byte)(((nz / l * 0.5f) + 0.5f) * 255);
                                }
                                else
                                {
                                    var swizzleA = (data[dataIndex + 3] * 2) - 255;     // premul A
                                    var swizzleG = (data[dataIndex + 1] * 2) - 255;         // premul G
                                    var deriveB = (int)System.Math.Sqrt((255 * 255) - (swizzleA * swizzleA) - (swizzleG * swizzleG));
                                    data[dataIndex + 2] = ClampColor((swizzleA / 2) + 128); // unpremul A and normalize (128 = forward, or facing viewer)
                                    data[dataIndex + 1] = ClampColor((swizzleG / 2) + 128); // unpremul G and normalize
                                    data[dataIndex + 0] = ClampColor((deriveB / 2) + 128);  // unpremul B and normalize
                                    data[dataIndex + 3] = 255;
                                }
                            }

                            if (invert)
                                data[dataIndex + 1] = (byte)(~data[dataIndex + 1]);  // LegacySource1InvertNormals
                        }
                }
        }
    }
}
