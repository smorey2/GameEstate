using GameEstate.Formats.Valve.Blocks.Animation;
using System.Collections.Generic;
using System.Linq;

namespace GameEstate.Formats.Valve.Blocks
{
    public class DATAModel : DATABinaryKV3OrNTRO
    {
        List<ModelAnimation> CachedEmbeddedAnimations;

        public ModelSkeleton GetSkeleton() => new ModelSkeleton(Data);

        public IEnumerable<string> GetReferencedMeshNames() => Data.Get<string[]>("m_refMeshes").Where(m => m != null);

        public IEnumerable<(string MeshName, long LoDMask)> GetReferenceMeshNamesAndLoD() => GetReferencedMeshNames().Zip(Data.GetIntArray("m_refLODGroupMasks"), (l, r) => (l, r));

        public IEnumerable<(DATAMesh Mesh, long LoDMask)> GetEmbeddedMeshesAndLoD() => GetEmbeddedMeshes().Zip(Data.GetIntArray("m_refLODGroupMasks"), (l, r) => (l, r));

        public IEnumerable<DATAMesh> GetEmbeddedMeshes()
        {
            var meshes = new List<DATAMesh>();
            if (Resource.TryGetBlockType<CTRL>(out var ctrl))
            {
                var embeddedMeshes = ctrl.Data.GetArray("embedded_meshes");
                foreach (var embeddedMesh in embeddedMeshes)
                {
                    var dataBlockIndex = (int)embeddedMesh.GetInt("data_block");
                    var vbibBlockIndex = (int)embeddedMesh.GetInt("vbib_block");
                    meshes.Add(new DATAMesh(Resource.GetBlockByIndex<DATA>(dataBlockIndex), Resource.GetBlockByIndex<VBIB_>(vbibBlockIndex)));
                }
            }
            return meshes;
        }

        public IEnumerable<string> GetReferencedAnimationGroupNames() => Data.Get<string[]>("m_refAnimGroups");

        public IEnumerable<ModelAnimation> GetEmbeddedAnimations()
        {
            if (CachedEmbeddedAnimations != null)
                return CachedEmbeddedAnimations;
            CachedEmbeddedAnimations = new List<ModelAnimation>();
            if (Resource.TryGetBlockType<CTRL>(out var ctrl))
            {
                var embeddedAnimation = ctrl.Data.GetSub("embedded_animation");
                if (embeddedAnimation == null)
                    return CachedEmbeddedAnimations;
                var groupDataBlockIndex = (int)embeddedAnimation.GetInt("group_data_block");
                var animDataBlockIndex = (int)embeddedAnimation.GetInt("anim_data_block");
                var animationGroup = Resource.GetBlockByIndex<DATABinaryKV3OrNTRO>(groupDataBlockIndex);
                var decodeKey = animationGroup.Data.GetSub("m_decodeKey");
                var animationDataBlock = Resource.GetBlockByIndex<DATABinaryKV3OrNTRO>(animDataBlockIndex);
                CachedEmbeddedAnimations.AddRange(ModelAnimation.FromData(animationDataBlock.Data, decodeKey));
            }
            return CachedEmbeddedAnimations;
        }

        public IEnumerable<string> GetMeshGroups() => Data.Get<string[]>("m_meshGroups");

        public IEnumerable<string> GetDefaultMeshGroups()
        {
            var defaultGroupMask = Data.GetUInt("m_nDefaultMeshGroupMask");
            return GetMeshGroups().Where((group, index) => ((ulong)(1 << index) & defaultGroupMask) != 0);
        }

        public IEnumerable<bool> GetActiveMeshMaskForGroup(string groupName)
        {
            var groupIndex = GetMeshGroups().ToList().IndexOf(groupName);
            var meshGroupMasks = Data.GetIntArray("m_refMeshGroupMasks");
            return groupIndex >= 0 ? meshGroupMasks.Select(mask => (mask & 1 << groupIndex) != 0) : meshGroupMasks.Select(_ => false);
        }
    }
}
