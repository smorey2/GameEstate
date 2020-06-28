using GameEstate.Toy.Models.Animations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEstate.Toy.Models
{
    public class Model : Dictionary<string, object>
    {
        //List<Animation> CachedEmbeddedAnimations;

        //public Skeleton GetSkeleton() => new Skeleton(Data);

        //public IEnumerable<string> GetReferencedMeshNames()
        //    => Data.GetArray<string>("m_refMeshes").Where(m => m != null);

        //public IEnumerable<(string MeshName, long LoDMask)> GetReferenceMeshNamesAndLoD()
        //    => GetReferencedMeshNames().Zip(Data.GetIntegerArray("m_refLODGroupMasks"), (l, r) => (l, r));

        //public IEnumerable<(Mesh Mesh, long LoDMask)> GetEmbeddedMeshesAndLoD()
        //    => GetEmbeddedMeshes().Zip(Data.GetIntegerArray("m_refLODGroupMasks"), (l, r) => (l, r));

        //public IEnumerable<Mesh> GetEmbeddedMeshes()
        //{
        //    var meshes = new List<Mesh>();

        //    if (Resource.ContainsBlockType(BlockType.CTRL))
        //    {
        //        var ctrl = Resource.GetBlockByType(BlockType.CTRL) as BinaryKV3;
        //        var embeddedMeshes = ctrl.Data.GetArray("embedded_meshes");

        //        foreach (var embeddedMesh in embeddedMeshes)
        //        {
        //            var dataBlockIndex = (int)embeddedMesh.GetIntegerProperty("data_block");
        //            var vbibBlockIndex = (int)embeddedMesh.GetIntegerProperty("vbib_block");

        //            meshes.Add(new Mesh(Resource.GetBlockByIndex(dataBlockIndex) as ResourceData, Resource.GetBlockByIndex(vbibBlockIndex) as VBIB));
        //        }
        //    }

        //    return meshes;
        //}

        //public IEnumerable<string> GetReferencedAnimationGroupNames()
        //    => Data.GetArray<string>("m_refAnimGroups");

        //public IEnumerable<Animation> GetEmbeddedAnimations()
        //{
        //    if (CachedEmbeddedAnimations != null)
        //    {
        //        return CachedEmbeddedAnimations;
        //    }

        //    CachedEmbeddedAnimations = new List<Animations>();

        //    if (Resource.ContainsBlockType(BlockType.CTRL))
        //    {
        //        var ctrl = Resource.GetBlockByType(BlockType.CTRL) as BinaryKV3;
        //        var embeddedAnimation = ctrl.Data.GetSubCollection("embedded_animation");

        //        if (embeddedAnimation == null)
        //        {
        //            return CachedEmbeddedAnimations;
        //        }

        //        var groupDataBlockIndex = (int)embeddedAnimation.GetIntegerProperty("group_data_block");
        //        var animDataBlockIndex = (int)embeddedAnimation.GetIntegerProperty("anim_data_block");

        //        var animationGroup = Resource.GetBlockByIndex(groupDataBlockIndex) as KeyValuesOrNTRO;
        //        var decodeKey = animationGroup.Data.GetSubCollection("m_decodeKey");

        //        var animationDataBlock = Resource.GetBlockByIndex(animDataBlockIndex) as KeyValuesOrNTRO;

        //        CachedEmbeddedAnimations.AddRange(Animations.FromData(animationDataBlock.Data, decodeKey));
        //    }

        //    return CachedEmbeddedAnimations;
        //}

        //public IEnumerable<string> GetMeshGroups()
        //    => Data.GetArray<string>("m_meshGroups");

        //public IEnumerable<string> GetDefaultMeshGroups()
        //{
        //    var defaultGroupMask = Data.GetUnsignedIntegerProperty("m_nDefaultMeshGroupMask");

        //    return GetMeshGroups().Where((group, index) => ((ulong)(1 << index) & defaultGroupMask) != 0);
        //}

        //public IEnumerable<bool> GetActiveMeshMaskForGroup(string groupName)
        //{
        //    var groupIndex = GetMeshGroups().ToList().IndexOf(groupName);
        //    var meshGroupMasks = Data.GetIntegerArray("m_refMeshGroupMasks");
        //    if (groupIndex >= 0)
        //        return meshGroupMasks.Select(mask => (mask & 1 << groupIndex) != 0);
        //    else
        //        return meshGroupMasks.Select(_ => false);
        //}
    }
}
