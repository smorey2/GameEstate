using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameEstate.Formats.AC.Entity
{
    public class CloObjectEffect : IGetExplorerInfo
    {
        public readonly uint Index;
        public readonly uint ModelId;
        public readonly CloTextureEffect[] CloTextureEffects;

        public CloObjectEffect(BinaryReader r)
        {
            Index = r.ReadUInt32();
            ModelId = r.ReadUInt32();
            CloTextureEffects = r.ReadL32Array(x => new CloTextureEffect(x));
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"Index: {Index}"),
                new ExplorerInfoNode($"ModelId: {ModelId:X8}"),
                new ExplorerInfoNode($"Texture Effects", items: CloTextureEffects.Select(x=> new ExplorerInfoNode($"{x}"))),
            };
            return nodes;
        }
    }
}
