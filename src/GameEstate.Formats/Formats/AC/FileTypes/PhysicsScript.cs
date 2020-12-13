using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using GameEstate.Formats.AC.Entity;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.FileTypes
{
    /// <summary>
    /// These are client_portal.dat files starting with 0x33. 
    /// </summary>
    [PakFileType(PakFileType.PhysicsScript)]
    public class PhysicsScript : AbstractFileType, IGetExplorerInfo
    {
        public readonly PhysicsScriptData[] ScriptData;

        public PhysicsScript(BinaryReader r)
        {
            Id = r.ReadUInt32();
            ScriptData = r.ReadL32Array(x => new PhysicsScriptData(x));
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"{nameof(PhysicsScript)}: {Id:X8}", items: new List<ExplorerInfoNode> {
                    //new ExplorerInfoNode($"Type: {Type}"),
                })
            };
            return nodes;
        }
    }
}
