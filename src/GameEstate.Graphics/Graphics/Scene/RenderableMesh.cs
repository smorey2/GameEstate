using System.Collections.Generic;

namespace GameEstate.Graphics.Scene
{
    public interface IRenderableMeshCollection
    {
        IEnumerable<RenderableMesh> RenderableMeshes { get; }
    }

    public class RenderableMesh
    {
        //public AABB BoundingBox { get; }

        public List<object> DrawCallsOpaque { get; } = new List<object>();
        public List<object> DrawCallsBlended { get; } = new List<object>();
    }
}
