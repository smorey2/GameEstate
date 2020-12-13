using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using GameEstate.Formats.AC.Props;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class BSPTree : IGetExplorerInfo
    {
        public readonly BSPNode RootNode;

        public BSPTree(BinaryReader r, BSPType treeType)
        {
            RootNode = BSPNode.Factory(r, treeType);
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"Root", items: (RootNode as IGetExplorerInfo).GetInfoNodes(tag: tag)),
            };
            return nodes;
        }
    }
}
