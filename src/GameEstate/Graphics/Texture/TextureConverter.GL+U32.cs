using SkiaSharp;
using System.IO;

namespace GameEstate.Graphics.Texture
{
    static partial class TextureConverter
    {
        public static SKBitmap U32(BinaryReader r, int w, int h, SKColorType colorType)
        {
            var res = new SKBitmap(w, h, colorType, SKAlphaType.Unpremul);

            for (var y = 0; y < h; y++)
                for (var x = 0; x < w; x++)
                    res.SetPixel(x, y, new SKColor(r.ReadUInt32()));

            return res;
        }
    }
}
