using OpenTK.Graphics.OpenGL;
using System;

namespace GameEstate.Graphics.TextureBuilders
{
    public class OpenGLTextureBuilder : AbstractTextureBuilder<int>
    {
        public static int MaxTextureMaxAnisotropy { get; set; }

        public void Release()
        {
            if (_defaultTexture != 0)
            {
                GL.DeleteTexture(_defaultTexture);
                _defaultTexture = 0;
            }
        }

        int _defaultTexture;
        public override int DefaultTexture => _defaultTexture != 0 ? _defaultTexture : _defaultTexture = BuildAutoTexture();

        int BuildAutoTexture() => BuildSolidTexture(4, 4, new[]
        {
            0.9f, 0.2f, 0.8f, 1f,
            0f, 0.9f, 0f, 1f,
            0.9f, 0.2f, 0.8f, 1f,
            0f, 0.9f, 0f, 1f,

            0f, 0.9f, 0f, 1f,
            0.9f, 0.2f, 0.8f, 1f,
            0f, 0.9f, 0f, 1f,
            0.9f, 0.2f, 0.8f, 1f,

            0.9f, 0.2f, 0.8f, 1f,
            0f, 0.9f, 0f, 1f,
            0.9f, 0.2f, 0.8f, 1f,
            0f, 0.9f, 0f, 1f,

            0f, 0.9f, 0f, 1f,
            0.9f, 0.2f, 0.8f, 1f,
            0f, 0.9f, 0f, 1f,
            0.9f, 0.2f, 0.8f, 1f,
        });
 
        public override int BuildTexture(ITextureInfo info)
        {
            var id = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, id);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, info.NumMipMaps - 1);

            InternalFormat format;
            switch (info.GLFormat)
            {
                case TextureGLFormat.DXT1: format = InternalFormat.CompressedRgbaS3tcDxt1Ext; break;
                case TextureGLFormat.DXT5: format = InternalFormat.CompressedRgbaS3tcDxt5Ext; break;
                case TextureGLFormat.ETC2: format = InternalFormat.CompressedRgb8Etc2; break;
                case TextureGLFormat.ETC2_EAC: format = InternalFormat.CompressedRgba8Etc2Eac; break;
                case TextureGLFormat.ATI1N: format = InternalFormat.CompressedRedRgtc1; break;
                case TextureGLFormat.ATI2N: format = InternalFormat.CompressedRgRgtc2; break;
                case TextureGLFormat.BC6H: format = InternalFormat.CompressedRgbBptcUnsignedFloat; break;
                case TextureGLFormat.BC7: format = InternalFormat.CompressedRgbaBptcUnorm; break;
                case TextureGLFormat.RGBA8888: format = InternalFormat.Rgba8; break;
                case TextureGLFormat.RGBA16161616F: format = InternalFormat.Rgba16f; break;
                case TextureGLFormat.I8: format = InternalFormat.Intensity8; break;
                default: Console.Error.WriteLine($"Don't support {info.GLFormat} but don't want to crash either. Using error texture!"); return DefaultTexture;
            }

            info.MoveToData();
            for (var i = info.NumMipMaps - 1; i >= 0; i--)
            {
                var width = info.Width >> i;
                var height = info.Height >> i;
                var bytes = info[i];

                GL.CompressedTexImage2D(TextureTarget.Texture2D, i, format, width, height, 0, bytes.Length, bytes);
            }

            if (info is IDisposable disposable)
                disposable.Dispose();

            if (MaxTextureMaxAnisotropy >= 4)
            {
                GL.TexParameter(TextureTarget.Texture2D, (TextureParameterName)ExtTextureFilterAnisotropic.TextureMaxAnisotropyExt, MaxTextureMaxAnisotropy);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            }
            else
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            }

            var clampModeS = info.Flags.HasFlag(TextureFlags.SUGGEST_CLAMPS)
                ? TextureWrapMode.Clamp
                : TextureWrapMode.Repeat;
            var clampModeT = info.Flags.HasFlag(TextureFlags.SUGGEST_CLAMPT)
                ? TextureWrapMode.Clamp
                : TextureWrapMode.Repeat;

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)clampModeS);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)clampModeT);

            GL.BindTexture(TextureTarget.Texture2D, 0);

            return id;
        }

        public override int BuildSolidTexture(int width, int height, float[] rgba)
        {
            var texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba32f, width, height, 0, PixelFormat.Rgba, PixelType.Float, rgba);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, 0);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.BindTexture(TextureTarget.Texture2D, 0);
            return texture;
        }

        public override int BuildNormalMap(int source, float strength) => throw new NotImplementedException();
    }
}