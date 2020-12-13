using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameEstate.Formats.AC.Entity
{
    public class CloSubPalEffect : IGetExplorerInfo
    {
        /// <summary>
        /// Icon portal.dat 0x06000000
        /// </summary>
        public readonly uint Icon;
        public readonly CloSubPalette[] CloSubPalettes;

        public CloSubPalEffect(BinaryReader r)
        {
            Icon = r.ReadUInt32();
            CloSubPalettes = r.ReadL32Array(x => new CloSubPalette(x));
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"Icon: {Icon:X8}"),
                new ExplorerInfoNode("SubPalettes", items: CloSubPalettes.Select(x => {
                    var items = (x as IGetExplorerInfo).GetInfoNodes();
                    var name = items[1].Name.Replace("Palette Set: ", "");
                    items.RemoveAt(1);
                    return new ExplorerInfoNode(name, items: items);
                })),
            };
            return nodes;
        }
    }
}
