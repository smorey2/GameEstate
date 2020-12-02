using UnityEngine;

namespace GameEstate.Graphics.MaterialBuilders
{
    /// <summary>
    /// An abstract class to describe a material.
    /// </summary>
    public abstract class AbstractMaterialBuilder
    {
        public float? NormalGeneratorIntensity = 0.75f;

        protected TextureManager _textureManager;

        public AbstractMaterialBuilder(TextureManager textureManager)
        {
            _textureManager = textureManager;
        }

        public abstract Material BuildMaterial(object key);

        protected static Texture2D GenerateNormalMap(Texture2D source, float strength)
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
