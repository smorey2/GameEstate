using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameEstate.Formats.AC.Entity
{
    public class SceneType : IGetExplorerInfo
    {
        public uint StbIndex;
        public uint[] Scenes;

        public SceneType(BinaryReader r)
        {
            StbIndex = r.ReadUInt32();
            Scenes = r.ReadL32Array<uint>(sizeof(uint));
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"SceneTableIdx: {StbIndex}"),
                new ExplorerInfoNode("Scenes", items: Scenes.Select(x => new ExplorerInfoNode($"{x:X8}"))),
            };
            return nodes;
        }
    }
}
