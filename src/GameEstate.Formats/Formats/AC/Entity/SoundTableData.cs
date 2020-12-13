using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class SoundTableData : IGetExplorerInfo
    {
        public readonly uint SoundId; // Corresponds to the DatFileType.Wave
        public readonly float Priority;
        public readonly float Probability;
        public readonly float Volume;

        public SoundTableData(BinaryReader r)
        {
            SoundId = r.ReadUInt32();
            Priority = r.ReadSingle();
            Probability = r.ReadSingle();
            Volume = r.ReadSingle();
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"Sound ID: {SoundId:X8}"),
                new ExplorerInfoNode($"Priority: {Priority}"),
                new ExplorerInfoNode($"Probability: {Probability}"),
                new ExplorerInfoNode($"Volume: {Volume}"),
            };
            return nodes;
        }
    }
}
