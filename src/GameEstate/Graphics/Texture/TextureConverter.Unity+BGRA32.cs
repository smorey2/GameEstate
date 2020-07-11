using System;

namespace GameEstate.Graphics.Texture
{
    public static partial class TextureConverter
    {
        static unsafe void BGRA32(this TextureInfo source, byte[] rawData)
        {
            Buffer.BlockCopy(source.Data, 0, rawData, 0, rawData.Length);
        }
    }
}
