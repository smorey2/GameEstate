using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameEstate.Formats.AC.Entity
{
    public class ClothingBaseEffect : IGetExplorerInfo
    {
        public readonly CloObjectEffect[] CloObjectEffects;

        public ClothingBaseEffect(BinaryReader r)
        {
            CloObjectEffects = r.ReadL32Array(x => new CloObjectEffect(x));
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode("Object Effects", items: CloObjectEffects.Select(x => {
                    var items = (x as IGetExplorerInfo).GetInfoNodes();
                    var name = items[0].Name.Replace("Idx: ", "");
                    items.RemoveAt(0);
                    return new ExplorerInfoNode(name, items: items);
                })),
            };
            return nodes;
        }
    }
}
