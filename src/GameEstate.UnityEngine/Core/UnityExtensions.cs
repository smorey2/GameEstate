using GameEstate.Graphics;
using GameEstate.Graphics.DirectX;

namespace GameEstate.Core
{
    public static class UnityExtensions
    {
        public static UnityEngine.Experimental.Rendering.GraphicsFormat ToUnity(this DXGI_FORMAT source) => (UnityEngine.Experimental.Rendering.GraphicsFormat)source;
        public static UnityEngine.TextureFormat ToUnity(this TextureUnityFormat source) => (UnityEngine.TextureFormat)source;

        /// <summary>
        /// Creates a Unity Texture2D from this Texture2DInfo.
        /// </summary>
        public static UnityEngine.Texture2D ToOpenTK(this TextureInfo source)
        {
            var tex = new UnityEngine.Texture2D(source.Width, source.Height, (UnityEngine.TextureFormat)source.UnityFormat, source.Mipmaps > 0);
            if (source.Data != null)
            {
                tex.LoadRawTextureData(source.Data);
                tex.Apply();
                tex.Compress(true);
            }
            return tex;
        }
    }
}