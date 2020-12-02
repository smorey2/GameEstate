using GameEstate.Graphics.OpenGL;

namespace GameEstate.Graphics.MaterialBuilders
{
    public abstract class AbstractMaterialBuilder
    {
        protected TextureManager _textureManager;

        public AbstractMaterialBuilder(TextureManager textureManager)
        {
            _textureManager = textureManager;
        }

        public abstract Material BuildMaterial(object key);
    }
}
