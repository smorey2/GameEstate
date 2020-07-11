using System;
using System.Collections.Generic;

namespace GameEstate.Graphics.Scene
{
    public interface ISceneContext
    {
        Action<List<MeshBatchRequest>, Scene.RenderContext> MeshBatchRenderer { get; }
    }
}
