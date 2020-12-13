using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using GameEstate.Formats.AC.Props;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class AmbientSoundDesc : IGetExplorerInfo
    {
        public readonly Sound SType;
        public readonly float Volume;
        public readonly float BaseChance;
        public readonly float MinRate;
        public readonly float MaxRate;

        public AmbientSoundDesc(BinaryReader r)
        {
            SType = (Sound)r.ReadUInt32();
            Volume = r.ReadSingle();
            BaseChance = r.ReadSingle();
            MinRate = r.ReadSingle();
            MaxRate = r.ReadSingle();
        }

        public bool IsContinuous => BaseChance == 0;

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"SoundType: {SType}"),
                new ExplorerInfoNode($"Volume: {Volume}"),
                new ExplorerInfoNode($"BaseChance: {BaseChance}"),
                new ExplorerInfoNode($"MinRate: {MinRate}"),
                new ExplorerInfoNode($"MaxRate: {MaxRate}"),
            };
            return nodes;
        }
    }
}
