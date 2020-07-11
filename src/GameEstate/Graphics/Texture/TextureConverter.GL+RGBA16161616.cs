using SkiaSharp;
using System;
using System.IO;

namespace GameEstate.Graphics.Texture
{
    static partial class TextureConverter
    {
        public static void RGBA16161616(SKImageInfo imageInfo, BinaryReader r, Span<byte> data)
        {
            var bytes = r.ReadBytes(imageInfo.Width * imageInfo.Height * 8);
            var log = 0d;

            for (int i = 0, j = 0; i < bytes.Length; i += 8, j += 4)
            {
                var hr = BitConverter.ToUInt16(bytes, i + 0) / 256f;
                var hg = BitConverter.ToUInt16(bytes, i + 2) / 256f;
                var hb = BitConverter.ToUInt16(bytes, i + 4) / 256f;
                var lum = (hr * 0.299f) + (hg * 0.587f) + (hb * 0.114f);
                log += Math.Log(0.0000000001d + lum);
            }

            log = Math.Exp(log / (imageInfo.Width * imageInfo.Height));

            for (int i = 0, j = 0; i < bytes.Length; i += 8, j += 4)
            {
                var hr = BitConverter.ToUInt16(bytes, i + 0) / 256f;
                var hg = BitConverter.ToUInt16(bytes, i + 2) / 256f;
                var hb = BitConverter.ToUInt16(bytes, i + 4) / 256f;
                var ha = BitConverter.ToUInt16(bytes, i + 6) / 256f;

                var y = (hr * 0.299f) + (hg * 0.587f) + (hb * 0.114f);
                var u = (hb - y) * 0.565f;
                var v = (hr - y) * 0.713f;

                var mul = 4.0f * y / log;
                mul /= (1.0f + mul);
                mul /= y;

                hr = (float)Math.Pow((y + (1.403f * v)) * mul, 2.25f);
                hg = (float)Math.Pow((y - (0.344f * u) - (0.714f * v)) * mul, 2.25f);
                hb = (float)Math.Pow((y + (1.770f * u)) * mul, 2.25f);

                if (hr < 0) hr = 0;
                if (hr > 1) hr = 1;
                if (hg < 0) hg = 0;
                if (hg > 1) hg = 1;
                if (hb < 0) hb = 0;
                if (hb > 1) hb = 1;

                data[j + 0] = (byte)(hr * 255); // r
                data[j + 1] = (byte)(hg * 255); // g
                data[j + 2] = (byte)(hb * 255); // b
                data[j + 3] = (byte)(ha * 255); // a
            }
        }
    }
}
