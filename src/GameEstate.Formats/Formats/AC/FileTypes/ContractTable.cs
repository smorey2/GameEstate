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
    /// This is the client_portal.dat file 0x0E00001D
    /// </summary>
    [PakFileType(PakFileType.ContractTable)]
    public class ContractTable : AbstractFileType, IGetExplorerInfo
    {
        public const uint FILE_ID = 0x0E00001D;

        public readonly Dictionary<uint, Contract> Contracts;

        public ContractTable(BinaryReader r)
        {
            Id = r.ReadUInt32();
            Contracts = r.ReadL16Many<uint, Contract>(sizeof(uint), x => new Contract(x), offset: 2);
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"{nameof(ContractTable)}: {Id:X8}", items: new List<ExplorerInfoNode> {
                })
            };
            return nodes;
        }
    }
}
