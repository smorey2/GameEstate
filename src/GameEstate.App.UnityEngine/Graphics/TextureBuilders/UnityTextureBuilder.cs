using System;
using UnityEngine;

namespace GameEstate.Graphics.TextureBuilders
{
    public class UnityTextureBuilder : AbstractTextureBuilder<Texture2D>
    {
        static readonly Texture2D _errorTexture = new Texture2D(1, 1);

        public UnityTextureBuilder() : base() { }

        public override Texture2D BuildTexture(TextureInfo info)
        {
            var tex = new Texture2D(info.Width, info.Height, (TextureFormat)info.UnityFormat, info.Mipmaps > 0);
            if (info.Data != null)
            {
                tex.LoadRawTextureData(info.Data);
                tex.Apply();
                tex.Compress(true);
            }
            return tex;
        }

        public override Texture2D ErrorTexture => _errorTexture;

        public override Texture2D BuildSolidTexture(float[] rgba) => throw new NotImplementedException();

        public override Texture2D BuildNormalMap(Texture2D source, float strength)
        {
            strength = Mathf.Clamp(strength, 0.0F, 1.0F);
            float xLeft, xRight, yUp, yDown, yDelta, xDelta;
            var normalTexture = new Texture2D(source.width, source.height, TextureFormat.ARGB32, true);
            for (var y = 0; y < normalTexture.height; y++)
                for (var x = 0; x < normalTexture.width; x++)
                {
                    xLeft = source.GetPixel(x - 1, y).grayscale * strength;
                    xRight = source.GetPixel(x + 1, y).grayscale * strength;
                    yUp = source.GetPixel(x, y - 1).grayscale * strength;
                    yDown = source.GetPixel(x, y + 1).grayscale * strength;
                    xDelta = (xLeft - xRight + 1) * 0.5f;
                    yDelta = (yUp - yDown + 1) * 0.5f;
                    normalTexture.SetPixel(x, y, new UnityEngine.Color(xDelta, yDelta, 1.0f, yDelta));
                }
            normalTexture.Apply();
            return normalTexture;
        }
    }
}