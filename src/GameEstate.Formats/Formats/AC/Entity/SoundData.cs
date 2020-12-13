using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameEstate.Formats.AC.Entity
{
    public class SoundData : IGetExplorerInfo
    {
        public readonly SoundTableData[] Data;
        public readonly uint Unknown;

        public SoundData(BinaryReader r)
        {
            Data = r.ReadL32Array(x => new SoundTableData(x));
            Unknown = r.ReadUInt32();
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode("SoundTable", items: Data.Select(x => {
                    var items = (x as IGetExplorerInfo).GetInfoNodes();
                    var name = items[0].Name.Replace("Sound ID: ", "");
                    items.RemoveAt(0);
                    return new ExplorerInfoNode(name, items: items);
                })),
                new ExplorerInfoNode($"Unknown: {Unknown}"),
            };
            return nodes;
        }
    }
}
