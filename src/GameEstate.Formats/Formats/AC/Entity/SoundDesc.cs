using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameEstate.Formats.AC.Entity
{
    public class SoundDesc : IGetExplorerInfo
    {
        public readonly AmbientSTBDesc[] STBDesc;

        public SoundDesc(BinaryReader r)
        {
            STBDesc = r.ReadL32Array(x => new AmbientSTBDesc(x));
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode("SoundTable", items: STBDesc.Select((x, i) => {
                    var items = (x as IGetExplorerInfo).GetInfoNodes();
                    var name = items[0].Name.Replace("Ambient Sound Table ID: ", "");
                    items.RemoveAt(0);
                    return new ExplorerInfoNode($"{i}: {name}", items: items);
                })),
            };
            return nodes;
        }
    }
}
