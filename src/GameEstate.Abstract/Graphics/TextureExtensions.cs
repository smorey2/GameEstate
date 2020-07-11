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
        const int DDS_HEADER_SIZE = 128;

        static partial class Literal
        {
            public static readonly byte[] IMG_ = Encoding.ASCII.GetBytes("IMG ");
        }

        public static int GetBlockSize(this TextureGLFormat source) => source switch
        {
            TextureGLFormat.DXT1 => 8,
            TextureGLFormat.DXT5 => 16,
            TextureGLFormat.RGBA8888 => 4,
            TextureGLFormat.R16 => 2,
            TextureGLFormat.RG1616 => 4,
            TextureGLFormat.RGBA16161616 => 8,
            TextureGLFormat.R16F => 2,
            TextureGLFormat.RG1616F => 4,
            TextureGLFormat.RGBA16161616F => 8,
            TextureGLFormat.R32F => 4,
            TextureGLFormat.RG3232F => 8,
            TextureGLFormat.RGB323232F => 12,
            TextureGLFormat.RGBA32323232F => 16,
            TextureGLFormat.BC6H => 16,
            TextureGLFormat.BC7 => 16,
            TextureGLFormat.IA88 => 2,
            TextureGLFormat.ETC2 => 8,
            TextureGLFormat.ETC2_EAC => 16,
            TextureGLFormat.BGRA8888 => 4,
            TextureGLFormat.ATI1N => 8,
            _ => 1,
        };

        #region CustomImage

        public static void LoadDxt(this TextureInfo source, TextureUnityFormat format, byte[] data)
        {
            if (format != TextureUnityFormat.DXT1 && format != TextureUnityFormat.DXT5)
                throw new ArgumentOutOfRangeException(nameof(format), "Invalid TextureFormat. Only DXT1 and DXT5 formats are supported by this method.");
            source.UnityFormat = format;
            var ddsSizeCheck = data[4];
            if (ddsSizeCheck != 124)
                throw new ArgumentOutOfRangeException(nameof(data), "Invalid DDS DXTn texture. Unable to read"); // this header byte should be 124 for DDS image files
            source.Height = (data[13] << 8) | data[12];
            source.Width = (data[17] << 8) | data[16];
            var dxtBytes = new byte[data.Length - DDS_HEADER_SIZE];
            Buffer.BlockCopy(dxtBytes, DDS_HEADER_SIZE, dxtBytes, 0, data.Length - DDS_HEADER_SIZE);
            source.Data = dxtBytes;
        }

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

        #region Bitmap


        #endregion
    }
}