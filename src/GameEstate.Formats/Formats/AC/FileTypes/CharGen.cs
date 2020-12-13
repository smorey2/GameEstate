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
    [PakFileType(PakFileType.CharacterGenerator)]
    public class CharGen : AbstractFileType, IGetExplorerInfo
    {
        public const uint FILE_ID = 0x0E000002;

        public readonly StarterArea[] StarterAreas;
        public readonly Dictionary<uint, HeritageGroupCG> HeritageGroups;

        public CharGen(BinaryReader r)
        {
            Id = r.ReadUInt32();
            r.Skip(-4);
            StarterAreas = r.ReadC32Array(x => new StarterArea(x));
            // HERITAGE GROUPS -- 11 standard player races and 2 Olthoi.
            r.Skip(1); // Not sure what this byte 0x01 is indicating, but we'll skip it because we can.
            HeritageGroups = r.ReadC32Many<uint, HeritageGroupCG>(sizeof(uint), x => new HeritageGroupCG(x));
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"{nameof(CharGen)}: {Id:X8}", items: new List<ExplorerInfoNode> {
                    new ExplorerInfoNode("Starter Areas", items: StarterAreas.Select(x => {
                        var items = (x as IGetExplorerInfo).GetInfoNodes();
                        var name = items[0].Name.Replace("Name: ", "");
                        items.RemoveAt(0);
                        return new ExplorerInfoNode(name, items: items);
                    })),
                    new ExplorerInfoNode("Heritage Groups", items: HeritageGroups.Select(x => {
                        var items = (x.Value as IGetExplorerInfo).GetInfoNodes();
                        var name = items[0].Name.Replace("Name: ", "");
                        items.RemoveAt(0);
                        return new ExplorerInfoNode(name, items: items);
                    })),
                })
            };
            return nodes;
        }
    }
}
