using GameEstate.Graphics.Algorithms;
using SkiaSharp;
using System.IO;

namespace GameEstate.Graphics.Texture
{
    static partial class TextureConverter
    {
        public static SKBitmap R16F(BinaryReader r, int w, int h)
        {
            var res = new SKBitmap(w, h, SKColorType.Bgra8888, SKAlphaType.Unpremul);

            for (var y = 0; y < h; y++)
                for (var x = 0; x < w; x++)
                {
                    var hr = (byte)(HalfPrecConverter.ToSingle(r.ReadUInt16()) * 255);

                    res.SetPixel(x, y, new SKColor(hr, 0, 0, 255));
                }
            return res;
        }
    }
}
