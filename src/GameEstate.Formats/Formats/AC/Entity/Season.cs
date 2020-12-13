using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameEstate.Formats.AC.Entity
{
    public class Season : IGetExplorerInfo
    {
        public readonly uint StartDate;
        public readonly string Name;

        public Season(BinaryReader r)
        {
            StartDate = r.ReadUInt32();
            Name = r.ReadL16ANSI(Encoding.Default);
            r.AlignBoundary();
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"StartDate: {StartDate}"),
                new ExplorerInfoNode($"Name: {Name}"),
            };
            return nodes;
        }
    }
}
