using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class PhysicsScriptData : IGetExplorerInfo
    {
        public readonly double StartTime;
        public readonly AnimationHook Hook;

        public PhysicsScriptData(BinaryReader r)
        {
            StartTime = r.ReadDouble();
            Hook = AnimationHook.Factory(r);
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"StartTime: {StartTime}"),
                new ExplorerInfoNode($"{Hook}"),
            };
            return nodes;
        }
    }
}
