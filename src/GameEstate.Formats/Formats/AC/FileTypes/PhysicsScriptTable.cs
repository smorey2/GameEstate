using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using GameEstate.Formats.AC.Entity;

namespace GameEstate.Formats.AC.FileTypes
{
    /// <summary>
    /// These are client_portal.dat files starting with 0x34. 
    /// </summary>
    [PakFileType(PakFileType.PhysicsScriptTable)]
    public class PhysicsScriptTable : AbstractFileType, IGetExplorerInfo
    {
        public readonly Dictionary<uint, PhysicsScriptTableData> ScriptTable;

        public PhysicsScriptTable(BinaryReader r)
        {
            Id = r.ReadUInt32();
            ScriptTable = r.ReadL32Many<uint, PhysicsScriptTableData>(sizeof(uint), x => new PhysicsScriptTableData(x));
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"{nameof(PhysicsScriptTable)}: {Id:X8}", items: new List<ExplorerInfoNode> {
                    new ExplorerInfoNode("ScriptTable", items: ScriptTable.Select(x => new ExplorerInfoNode($"{x.Key}", items: (x.Value as IGetExplorerInfo).GetInfoNodes()))),
                })
            };
            return nodes;
        }
    }
}
