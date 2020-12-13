using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameEstate.Formats.AC.Entity
{
    public class PhysicsScriptTableData : IGetExplorerInfo
    {
        public readonly ScriptAndModData[] Scripts;

        public PhysicsScriptTableData(BinaryReader r)
        {
            Scripts = r.ReadL32Array(x => new ScriptAndModData(r));
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode("ScriptMods", items: Scripts.Select(x=>new ExplorerInfoNode($"{x}"))),
            };
            return nodes;
        }
    }
}
