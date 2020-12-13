using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameEstate.Formats.AC.Entity
{
    public class TerrainType : IGetExplorerInfo
    {
        public readonly string TerrainName;
        public readonly uint TerrainColor;
        public readonly uint[] SceneTypes;

        public TerrainType(BinaryReader r)
        {
            TerrainName = r.ReadL16ANSI(Encoding.Default);
            r.AlignBoundary();
            TerrainColor = r.ReadUInt32();
            SceneTypes = r.ReadL32Array<uint>(sizeof(uint));
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"TerrainName: {TerrainName}"),
                new ExplorerInfoNode($"TerrainColor: {TerrainColor:X8}"),
                new ExplorerInfoNode("SceneTypes", items: SceneTypes.Select((x, i) => new ExplorerInfoNode($"{i}: {x}"))),
            };
            return nodes;
        }
    }
}
