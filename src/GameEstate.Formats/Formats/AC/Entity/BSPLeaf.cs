using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using GameEstate.Formats.AC.Props;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameEstate.Formats.AC.Entity
{
    public class BSPLeaf : BSPNode, IGetExplorerInfo
    {
        public readonly int LeafIndex;
        public readonly int Solid;

        public BSPLeaf(BinaryReader r, BSPType treeType) : base()
        {
            Type = Encoding.ASCII.GetString(r.ReadBytes(4)).Reverse();
            LeafIndex = r.ReadInt32();
            if (treeType == BSPType.Physics)
            {
                Solid = r.ReadInt32();
                // Note that if Solid is equal to 0, these values will basically be null. Still read them, but they don't mean anything.
                Sphere = new Sphere(r);
                InPolys = r.ReadL32Array<ushort>(sizeof(ushort));
            }
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"Type: {Type}"),
                new ExplorerInfoNode($"LeafIndex: {LeafIndex}"),
            };
            if ((BSPType)tag == BSPType.Physics)
                nodes.AddRange(new[] {
                    new ExplorerInfoNode($"Solid: {Solid}"),
                    new ExplorerInfoNode($"Sphere: {Sphere}"),
                    new ExplorerInfoNode($"InPolys: {string.Join(", ", InPolys)}"),
                });
            return nodes;
        }
    }
}
