using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using GameEstate.Formats.AC.Entity;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameEstate.Formats.AC.FileTypes
{
    /// <summary>
    /// These are client_portal.dat files starting with 0x12. 
    /// </summary>
    [PakFileType(PakFileType.Scene)]
    public class Scene : AbstractFileType, IGetExplorerInfo
    {
        public readonly ObjectDesc[] Objects;

        public Scene(BinaryReader r)
        {
            Id = r.ReadUInt32();
            Objects = r.ReadL32Array(x => new ObjectDesc(x));
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"{nameof(Scene)}: {Id:X8}", items: new List<ExplorerInfoNode> {
                    new ExplorerInfoNode("Objects", items: Objects.Select(x => {
                        var items = (x as IGetExplorerInfo).GetInfoNodes();
                        var name = items[0].Name.Replace("Object ID: ", "");
                        items.RemoveAt(0);
                        return new ExplorerInfoNode(name, items: items);
                    })),
                })
            };
            return nodes;
        }
    }
}
