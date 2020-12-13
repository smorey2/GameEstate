using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameEstate.Formats.AC.Entity
{
    public class SceneDesc : IGetExplorerInfo
    {
        public readonly SceneType[] SceneTypes;

        public SceneDesc(BinaryReader r)
        {
            SceneTypes = r.ReadL32Array(x => new SceneType(x));
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode("SceneTypes", items: SceneTypes.Select((x, i) => new ExplorerInfoNode($"{i}", items: (x as IGetExplorerInfo).GetInfoNodes()))),
            };
            return nodes;
        }
    }
}
