using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameEstate.Formats.AC.Entity
{
    public class StarterArea : IGetExplorerInfo
    {
        public readonly string Name;
        public readonly Position[] Locations;

        public StarterArea(BinaryReader r)
        {
            Name = r.ReadString();
            Locations = r.ReadC32Array(x => new Position(x));
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"Name: {Name}"),
                new ExplorerInfoNode("Locations", items: Locations.Select(x => {
                    var items = (x as IGetExplorerInfo).GetInfoNodes();
                    var name = items[0].Name.Replace("ObjCellID: ", "");
                    items.RemoveAt(0);
                    return new ExplorerInfoNode(name, items: items);
                })),
            };
            return nodes;
        }
    }
}
