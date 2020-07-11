using SkiaSharp;
using System.IO;

namespace GameEstate.Graphics.Texture
{
    static partial class TextureConverter
    {
        public static SKBitmap RG1616(BinaryReader r, int w, int h)
        {
            var res = new SKBitmap(w, h, SKColorType.Bgra8888, SKAlphaType.Unpremul);

            for (var y = 0; y < h; y++)
                for (var x = 0; x < w; x++)
                {
                    var hr = (byte)(r.ReadUInt16() / 256);
                    var hg = (byte)(r.ReadUInt16() / 256);

                    res.SetPixel(x, y, new SKColor(hr, hg, 0, 255));
                }
            return res;
        }
    }
}
