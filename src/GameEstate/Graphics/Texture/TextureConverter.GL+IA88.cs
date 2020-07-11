using SkiaSharp;
using System.IO;

namespace GameEstate.Graphics.Texture
{
    static partial class TextureConverter
    {
        public static SKBitmap IA88(BinaryReader r, int w, int h)
        {
            var res = new SKBitmap(w, h, SKColorType.Bgra8888, SKAlphaType.Unpremul);

            for (var y = 0; y < h; y++)
                for (var x = 0; x < w; x++)
                {
                    var color = r.ReadByte();
                    var alpha = r.ReadByte();

                    res.SetPixel(x, y, new SKColor(color, color, color, alpha));
                }
            return res;
        }
    }
}
