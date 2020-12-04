using GameEstate.Formats.Valve;
using GameEstate.Formats.Valve.Blocks;
using OpenTK.Graphics.OpenGL;
using System;

namespace GameEstate.Graphics.TextureBuilders
{
    public class ValveTextureBuilder : AbstractTextureBuilder<int>
    {
        public static int MaxTextureMaxAnisotropy { get; set; }
        int _errorTexture;

        public ValveTextureBuilder() : base() { }

        public override int BuildTexture(TextureInfo textureInfo)
        {
            throw new NotImplementedException();
        }

        public int LoadTexture(BinaryPak textureResource)
        {
            var tex = (DATATexture)textureResource.DATA;

            var id = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, id);

            var textureReader = textureResource.Reader;
            textureReader.BaseStream.Position = tex.Offset + tex.Size;

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, tex.NumMips - 1);

            InternalFormat format;
            switch (tex.Format)
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
                default: Console.Error.WriteLine($"Don't support {tex.Format} but don't want to crash either. Using error texture!"); return ErrorTexture;
            }

            for (var i = tex.NumMips - 1; i >= 0; i--)
            {
                var width = tex.Width >> i;
                var height = tex.Height >> i;
                var bytes = new byte[0]; // tex.GetDecompressedTextureAtMipLevel(i);

                GL.CompressedTexImage2D(TextureTarget.Texture2D, i, format, width, height, 0, bytes.Length, bytes);
            }

            // Dispose texture otherwise we run out of memory
            // TODO: This might conflict when opening multiple files due to shit caching
            textureResource.Dispose();

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

            var clampModeS = tex.Flags.HasFlag(DATATexture.VTexFlags.SUGGEST_CLAMPS)
                ? TextureWrapMode.Clamp
                : TextureWrapMode.Repeat;
            var clampModeT = tex.Flags.HasFlag(DATATexture.VTexFlags.SUGGEST_CLAMPT)
                ? TextureWrapMode.Clamp
                : TextureWrapMode.Repeat;

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)clampModeS);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)clampModeT);

            GL.BindTexture(TextureTarget.Texture2D, 0);

            return id;
        }

        public override int ErrorTexture
        {
            get
            {
                if (_errorTexture == 0)
                    _errorTexture = GenerateColorTexture(4, 4, new[]
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
                return _errorTexture;
            }
        }

        public override int BuildSolidTexture(float[] rgba) => GenerateColorTexture(1, 1, rgba);

        static int GenerateColorTexture(int width, int height, float[] pixels)
        {
            var texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba32f, width, height, 0, PixelFormat.Rgba, PixelType.Float, pixels);
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