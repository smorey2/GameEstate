using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class ScriptAndModData : IGetExplorerInfo
    {
        public readonly float Mod;
        public readonly uint ScriptId;

        public ScriptAndModData(BinaryReader r)
        {
            Mod = r.ReadSingle();
            ScriptId = r.ReadUInt32();
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"{Mod}"),
                new ExplorerInfoNode($"{ScriptId:X8}"),
            };
            return nodes;
        }

        public override string ToString() => $"Mod: {Mod}, Script: {ScriptId:X8}";
    }
}
