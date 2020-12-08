using GameEstate.Formats.Valve.Blocks.Animation;
using GameEstate.Graphics;
using System.Collections.Generic;

namespace GameEstate.Formats.Valve
{
    /// <summary>
    /// IValveModelInfo
    /// </summary>
    public interface IValveModelInfo : IModelInfo
    {
        ModelSkeleton GetSkeleton();

        IEnumerable<(string MeshName, long LoDMask)> GetReferenceMeshNamesAndLoD();

        IEnumerable<(IMeshInfo Mesh, long LoDMask)> GetEmbeddedMeshesAndLoD();

        IEnumerable<string> GetReferencedAnimationGroupNames();

        IEnumerable<ModelAnimation> GetEmbeddedAnimations();

        IEnumerable<string> GetMeshGroups();

        IEnumerable<string> GetDefaultMeshGroups();

        IEnumerable<bool> GetActiveMeshMaskForGroup(string groupName);
    }
}