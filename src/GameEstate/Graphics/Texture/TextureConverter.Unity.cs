using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace GameEstate.Graphics.Texture
{
    public static partial class TextureConverter
    {
        public static unsafe void SaveBitmap(this TextureInfo source, string filePath)
        {
            var width = source.Width; var height = source.Height;
            var rawData = new byte[width * height * 4];
            switch (source.UnityFormat)
            {
                case TextureUnityFormat.ARGB32: ARGB32(source, rawData, width, height); break;
                case TextureUnityFormat.BGRA32: BGRA32(source, rawData); break;
                case TextureUnityFormat.RGBA32: RGBA32(source, rawData, width, height); break;
                default: throw new ArgumentOutOfRangeException(nameof(source.UnityFormat), source.UnityFormat.ToString());
            }
            using (var bmp = new Bitmap(width, height, width * 4, PixelFormat.Format32bppRgb, Marshal.UnsafeAddrOfPinnedArrayElement(rawData, 0)))
                bmp.Save(filePath);
        }
    }
}
