using GameEstate.Toy.Core;
using GameEstate.Toy.Renderer;

namespace GameEstate.Toy
{
    public class GuiContext
    {
        public string FileName { get; }

        //public Package CurrentPackage { get; }

        //public Package ParentPackage { get; }

        public MaterialLoader MaterialLoader { get; }

        public ShaderLoader ShaderLoader { get; }
        public GPUMeshBufferCache MeshBufferCache { get; }

        readonly FileLoader FileLoader;

        QuadIndexBuffer _quadIndices;
        public QuadIndexBuffer QuadIndices
        {
            get
            {
                if (_quadIndices == null)
                    _quadIndices = new QuadIndexBuffer(65532);
                return _quadIndices;
            }
        }

        public GuiContext(string fileName, object package)
        {
            FileName = fileName;
            //CurrentPackage = package?.Package;
            //ParentPackage = package?.ParentPackage;
            MaterialLoader = new MaterialLoader(this);
            ShaderLoader = new ShaderLoader();
            FileLoader = new FileLoader();
            MeshBufferCache = new GPUMeshBufferCache();
        }

        public object LoadFileByAnyMeansNecessary(string file) => FileLoader.LoadFileByAnyMeansNecessary(file, this);

        public void ClearCache() => FileLoader.ClearCache();
    }
}
