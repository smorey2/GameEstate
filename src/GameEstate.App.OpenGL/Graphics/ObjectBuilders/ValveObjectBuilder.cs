using GameEstate.Graphics.OpenGL;
using System;

namespace GameEstate.Graphics.ObjectBuilders
{
    public class ValveObjectBuilder : AbstractObjectBuilder<object, Material, int>
    {
        public ValveObjectBuilder() : base() { }

        public override void EnsurePrefabContainerExists() { }

        public override object CreateObject(object prefab) => throw new NotImplementedException();

        public override object BuildObject(object source, MaterialManager<Material, int> materialManager) => throw new NotImplementedException();
    }
}