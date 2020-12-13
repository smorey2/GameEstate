using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class TerrainAlphaMap : IGetExplorerInfo
    {
        public readonly uint TCode;
        public readonly uint TexGID;

        public TerrainAlphaMap(BinaryReader r)
        {
            TCode = r.ReadUInt32();
            TexGID = r.ReadUInt32();
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"TerrainCode: {TCode}"),
                new ExplorerInfoNode($"TextureGID: {TexGID:X8}"),
            };
            return nodes;
        }

        public override string ToString() => $"TerrainCode: {TCode}, TextureGID: {TexGID:X8}";
    }
}
