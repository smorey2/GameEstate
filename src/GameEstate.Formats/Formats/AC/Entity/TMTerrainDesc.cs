using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class TMTerrainDesc : IGetExplorerInfo
    {
        public readonly uint TerrainType;
        public readonly TerrainTex TerrainTex;

        public TMTerrainDesc(BinaryReader r)
        {
            TerrainType = r.ReadUInt32();
            TerrainTex = new TerrainTex(r);
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"TerrainType: {TerrainType}"),
                new ExplorerInfoNode("TerrainTexture", items: (TerrainTex as IGetExplorerInfo).GetInfoNodes()),
            };
            return nodes;
        }
    }
}
