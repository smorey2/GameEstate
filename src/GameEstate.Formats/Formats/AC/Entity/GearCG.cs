using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class GearCG : IGetExplorerInfo
    {
        public readonly string Name;
        public readonly uint ClothingTable;
        public readonly uint WeenieDefault;

        public GearCG(BinaryReader r)
        {
            Name = r.ReadString();
            ClothingTable = r.ReadUInt32();
            WeenieDefault = r.ReadUInt32();
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"Name: {Name}"),
                new ExplorerInfoNode($"Clothing Table: {ClothingTable:X8}"),
                new ExplorerInfoNode($"Weenie Default: {WeenieDefault:X8}"),
            };
            return nodes;
        }
    }
}
