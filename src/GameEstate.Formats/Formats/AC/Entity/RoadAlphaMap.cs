using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class RoadAlphaMap : IGetExplorerInfo
    {
        public readonly uint RCode;
        public readonly uint RoadTexGID;

        public RoadAlphaMap(BinaryReader r)
        {
            RCode = r.ReadUInt32();
            RoadTexGID = r.ReadUInt32();
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"RoadCode: {RCode}"),
                new ExplorerInfoNode($"RoadTexGID: {RoadTexGID:X8}"),
            };
            return nodes;
        }

        public override string ToString() => $"RoadCode: {RCode}, RoadTexGID: {RoadTexGID:X8}";
    }
}
