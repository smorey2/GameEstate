using System;

namespace GameEstate
{
    public interface ITestGraphic : IEstateGraphic { }

    public class TestGraphic : ITestGraphic
    {
        readonly EstatePakFile _source;

        public TestGraphic(EstatePakFile source)
        {
            _source = source;
        }

        public EstatePakFile Source => _source;
        public void PreloadTexture(string texturePath) => throw new NotSupportedException();
        public void PreloadObject(string filePath) => throw new NotSupportedException();
    }
}