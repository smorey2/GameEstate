using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class GfxObjInfo : IGetExplorerInfo
    {
        public readonly uint Id;
        public readonly uint DegradeMode;
        public readonly float MinDist;
        public readonly float IdealDist;
        public readonly float MaxDist;

        public GfxObjInfo(BinaryReader r)
        {
            Id = r.ReadUInt32();
            DegradeMode = r.ReadUInt32();
            MinDist = r.ReadSingle();
            IdealDist = r.ReadSingle();
            MaxDist = r.ReadSingle();
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"Id: {Id:X8}"),
                new ExplorerInfoNode($"DegradeMode: {DegradeMode}"),
                new ExplorerInfoNode($"MinDist: {MinDist}"),
                new ExplorerInfoNode($"IdealDist: {IdealDist}"),
                new ExplorerInfoNode($"MaxDist: {MaxDist}"),
            };
            return nodes;
        }
    }
}
