namespace GameEstate.Graphics.Texture
{
    public static partial class TextureConverter
    {
        static unsafe void ARGB32(this TextureInfo source, byte[] rawData, int w, int h)
        {
            fixed (byte* pPixels = rawData, pData = source.Data)
            {
                var rPixels = (uint*)pPixels;
                var rData = (uint*)pData;
                for (var i = 0; i < w * h; ++i)
                {
                    var d = *rData++;
                    var b = (byte)(d >> 24);
                    var g = (byte)(d >> 16);
                    var r = (byte)(d >> 8);
                    var a = (byte)d;
                    var color =
                        ((uint)(a << 24) & 0xFF000000) |
                        ((uint)(r << 16) & 0x00FF0000) |
                        ((uint)(g << 8) & 0x0000FF00) |
                        ((uint)(b << 0) & 0x000000FF);
                    *rPixels++ = color;
                }
            }
        }
    }
}
