using GameEstate.Graphics.DirectX;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace GameEstate.Graphics
{
    /// <summary>
    /// TextureExtensions
    /// </summary>
    public static partial class TextureExtensions
    {
        static partial class Literal
        {
            public static readonly byte[] IMG_ = Encoding.ASCII.GetBytes("IMG ");
        }

        public static int GetBlockSize(this TextureGLFormat source)
        {
            switch (source)
            {
                case TextureGLFormat.DXT1: return 8;
                case TextureGLFormat.DXT5: return 6;
                case TextureGLFormat.RGBA8888: return 4;
                case TextureGLFormat.R16: return 2;
                case TextureGLFormat.RG1616: return 4;
                case TextureGLFormat.RGBA16161616: return 8;
                case TextureGLFormat.R16F: return 2;
                case TextureGLFormat.RG1616F: return 4;
                case TextureGLFormat.RGBA16161616F: return 8;
                case TextureGLFormat.R32F: return 4;
                case TextureGLFormat.RG3232F: return 8;
                case TextureGLFormat.RGB323232F: return 12;
                case TextureGLFormat.RGBA32323232F: return 16;
                case TextureGLFormat.BC6H: return 16;
                case TextureGLFormat.BC7: return 16;
                case TextureGLFormat.IA88: return 2;
                case TextureGLFormat.ETC2: return 8;
                case TextureGLFormat.ETC2_EAC: return 16;
                case TextureGLFormat.BGRA8888: return 4;
                case TextureGLFormat.ATI1N: return 8;
                default: return 1;
            }
        }

        #region CustomImage

        public static void LoadImage(this TextureInfo source, byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var r = new BinaryReader(ms))
            {
                var magicString = r.ReadBytes(4);
                if (!Literal.IMG_.SequenceEqual(magicString))
                    throw new FileFormatException($"Invalid IMG file magic string: \"{Encoding.ASCII.GetString(magicString)}\".");
                source.Mipmaps = r.ReadByte();
                source.UnityFormat = (TextureUnityFormat)r.ReadInt16();
                source.GLFormat = (TextureGLFormat)r.ReadInt16();
                source.Width = r.ReadInt32();
                source.Height = r.ReadInt32();
                source.Data = r.ReadBytes(r.ReadInt32());
            }
        }

        public static byte[] GetImage(this TextureInfo source)
        {
            using (var ms = new MemoryStream())
            using (var r = new BinaryWriter(ms))
            {
                r.Write(Literal.IMG_);
                r.Write(source.Mipmaps);
                r.Write((short)source.UnityFormat);
                r.Write((short)source.GLFormat);
                r.Write(source.Width);
                r.Write(source.Height);
                r.Write(source.Data.Length); r.Write(source.Data);
                ms.Position = 0;
                return ms.ToArray();
            }
        }

        #endregion

        #region Color Shift

        // TODO: Move? Unity?
        public static unsafe TextureInfo FromABGR555(this TextureInfo source)
        {
            var W = source.Width; var H = source.Height;
            var pixels = new byte[W * H * 4];
            fixed (byte* pPixels = pixels, pData = source.Data)
            {
                var rPixels = (uint*)pPixels;
                var rData = (ushort*)pData;
                for (var i = 0; i < W * H; ++i)
                {
                    var d555 = *rData++;
                    //var a = 0;// (byte)Math.Min(((d555 & 0x8000) >> 15) * 0x1F, byte.MaxValue);
                    //var r = (byte)Math.Min(((d555 & 0x7C00) >> 10) * 8, byte.MaxValue);
                    //var g = (byte)Math.Min(((d555 & 0x03E0) >> 5) * 8, byte.MaxValue);
                    //var b = (byte)Math.Min(((d555 & 0x001F) >> 0) * 8, byte.MaxValue);

                    var r = (byte)Math.Min(((d555 & 0xF800) >> 11) * 8, byte.MaxValue);     // 1111 1000 0000 0000 = F800
                    var g = (byte)Math.Min(((d555 & 0x07C0) >> 6) * 8, byte.MaxValue);      // 0000 0111 1100 0000 = 07C0
                    var b = (byte)Math.Min(((d555 & 0x003E) >> 1) * 8, byte.MaxValue);      // 0000 0000 0011 1110 = 003E
                    var a = (byte)Math.Min((d555 & 0x0001) * 0x1F, byte.MaxValue);          // 0000 0000 0000 0001 = 0001
                    uint color;
                    if (source.UnityFormat == TextureUnityFormat.RGBA32)
                        color =
                            ((uint)(a << 24) & 0xFF000000) |
                            ((uint)(b << 16) & 0x00FF0000) |
                            ((uint)(g << 8) & 0x0000FF00) |
                            ((uint)(r << 0) & 0x000000FF);
                    else if (source.UnityFormat == TextureUnityFormat.ARGB32)
                        color =
                            ((uint)(b << 24) & 0xFF000000) |
                            ((uint)(g << 16) & 0x00FF0000) |
                            ((uint)(r << 8) & 0x0000FF00) |
                            ((uint)(a << 0) & 0x000000FF);
                    else throw new ArgumentOutOfRangeException(nameof(source.UnityFormat), source.UnityFormat.ToString());
                    *rPixels++ = color;
                }
            }
            source.Data = pixels;
            return source;
        }

        // TODO: Move? Unity?
        public static TextureInfo From8BitPallet(this TextureInfo source, byte[][] pallet, TextureUnityFormat palletFormat)
        {
            if (source.UnityFormat != palletFormat)
                throw new InvalidOperationException();
            var b = new MemoryStream();
            var data = source.Data;
            for (var y = 0; y < data.Length; y++)
                b.Write(pallet[data[y]], 0, 4);
            source.Data = b.ToArray();
            return source;
        }

        #endregion
    }
}