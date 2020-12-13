using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameEstate.Formats.AC.Entity
{
    public class TimeOfDay : IGetExplorerInfo
    {
        public readonly float Start;
        public readonly bool IsNight;
        public readonly string Name;

        public TimeOfDay(BinaryReader r)
        {
            Start = r.ReadSingle();
            IsNight = r.ReadUInt32() == 1;
            Name = r.ReadL16ANSI(Encoding.Default);
            r.AlignBoundary();
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"Start: {Start}"),
                new ExplorerInfoNode($"IsNight: {IsNight}"),
                new ExplorerInfoNode($"Name: {Name}"),
            };
            return nodes;
        }
    }
}
